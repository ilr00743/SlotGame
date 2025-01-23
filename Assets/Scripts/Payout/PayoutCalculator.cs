using System.Collections.Generic;
using Paylines;
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

        public float CalculateTotalPayout(SymbolView[,] reels, List<Payline> activePaylines, int betPerLine)
        {
            float totalPayout = 0;

            foreach (var payline in activePaylines)
            {
                PaylineChecker paylineChecker = new PaylineChecker();
                var (symbolView, matches) = paylineChecker.GetPaylineMatches(reels, payline.PositionsArray);

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