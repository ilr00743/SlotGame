using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RNG.Strategies
{
    public class WeightedRandom : IRandomNumberGenerator
    {
        private readonly List<int> _weights;
        private readonly int _totalWeight;

        public WeightedRandom(List<int> weights)
        {
            _weights = weights;
            _totalWeight = weights.Sum();
        }
        
        public int GetRandomNumber()
        {
            int randomValue = Random.Range(0, _totalWeight);
            int currentWeight = 0;

            for (int i = 0; i < _weights.Count; i++)
            {
                currentWeight += _weights[i];

                if (randomValue < currentWeight)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}