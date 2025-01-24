using System;
using System.Collections.Generic;
using Symbols;

namespace Payout
{
    public class PayoutCalculator
    {
        private readonly SymbolsConfig _symbolsConfig;

        public PayoutCalculator(SymbolsConfig symbolsConfig)
        {
            _symbolsConfig = symbolsConfig;
        }
        
        public float CalculateTotalPayout(List<(SymbolView, int)> symbolMatchesList, int betPerLine)
        {
            float totalPayout = 0;

            foreach (var symbolMatches in symbolMatchesList)
            {
                var symbolView = symbolMatches.Item1;
                var matches = symbolMatches.Item2;

                if (symbolView != null && matches > 0)
                {
                    float payout = CalculateSymbolPayout(symbolView, matches, betPerLine);
                    totalPayout += payout;
                }
            }

            return totalPayout;
        }

        private float CalculateSymbolPayout(SymbolView symbolView, int matches, int betPerLine)
        {
            var symbolModel = _symbolsConfig.GetSymbolByName(symbolView.Name);
            float rate = symbolModel.GetPayoutRate(matches);
            return rate * betPerLine;
        }
    }
}