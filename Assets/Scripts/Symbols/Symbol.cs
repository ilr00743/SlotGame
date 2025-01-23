using System;
using UnityEngine;

namespace Symbols
{
    [Serializable]
    public class Symbol
    {
        private const int MIN_MATCHES = 3;
        
        [SerializeField] private string _name;

        [SerializeField] private Sprite _icon;

        [SerializeField] private int _weight;

        [SerializeField] private MatchMultiplierPair[] _multipliers;

        public string Name => _name;

        public Sprite Icon => _icon;

        public int Weight => _weight;

        public float GetPayoutRate(int matches)
        {
            if (matches < MIN_MATCHES || matches > MIN_MATCHES + _multipliers.Length - 1)
                return 0;
            return _multipliers[matches - MIN_MATCHES].Multiplier;
        }

        [Serializable]
        private class MatchMultiplierPair
        {
            public int Matches;
            public float Multiplier;
        }
    }
}