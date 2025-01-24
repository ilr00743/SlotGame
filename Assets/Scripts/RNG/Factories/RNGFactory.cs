using System.Collections.Generic;
using RNG.Strategies;

namespace RNG.Factories
{
    public static class RNGFactory
    {
        public static IRandomNumberGenerator CreateWeightedRNG(List<int> weights)
        {
            return new WeightedRandom(weights);
        }

        public static IRandomNumberGenerator CreateBufferedCryptoRNG(int minValue, int maxValue)
        {
            return new BufferedCryptoRandom(minValue, maxValue);
        }

        public static IRandomNumberGenerator CreateTimeBasedRNG(int minValue, int maxValue)
        {
            return new TimeBasedRandom(minValue, maxValue);
        }

        public static IRandomNumberGenerator CreateGuaranteedWinRNG(int symbolIndex)
        {
            return new GuaranteedWinRandom(symbolIndex);
        }
    }
}