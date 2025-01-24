using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace RNG.Strategies
{
    public class BufferedCryptoRandom : IRandomNumberGenerator
    {
        private const int BUFFER_SIZE = 1000;
        
        private readonly Queue<int> _buffer = new Queue<int>();
        private readonly int _minValue;
        private readonly int _maxValue;
        
        public BufferedCryptoRandom(int minValue, int maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }
        
        public int GetRandomNumber()
        {
            if (_buffer.Count == 0)
            {
                RefillBuffer();
            }

            return _buffer.Dequeue() % (_maxValue - _minValue) + _minValue;
        }
        
        private void RefillBuffer()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomNumbers = new byte[BUFFER_SIZE * 4];
                rng.GetBytes(randomNumbers);

                for (int i = 0; i < BUFFER_SIZE; i++)
                {
                    int value = BitConverter.ToInt32(randomNumbers, i * 4);
                    _buffer.Enqueue(Math.Abs(value));
                }
            }
        }
    }
}