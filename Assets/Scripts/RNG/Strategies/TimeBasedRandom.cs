using System;

namespace RNG.Strategies
{
    public class TimeBasedRandom : IRandomNumberGenerator
    {
        private readonly int _minValue;
        private readonly int _maxValue;

        public TimeBasedRandom(int minValue, int maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }
        
        public int GetRandomNumber()
        {
            long seed = DateTime.Now.Ticks;
            Random random = new Random((int)(seed & 0xFFFFFFFF));
            return random.Next(_minValue, _maxValue);
        }
    }
}