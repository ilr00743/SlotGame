using System.Collections.Generic;
using System.Linq;
using Paylines;
using Payout;
using Symbols;

namespace Services
{
    public class PaylineService : IPaylineService
    {
        private readonly PaylineChecker _paylineChecker;
        private readonly PayoutCalculator _payoutCalculator;
        private readonly PaylineVisualizer _paylineVisualizer;
        private readonly PaylineConfig _paylineConfig;
        
        public List<Payline> GetPaylines => _paylineConfig.PayLines.ToList();

        public PaylineService(
            PaylineChecker paylineChecker,
            PayoutCalculator payoutCalculator,
            PaylineVisualizer paylineVisualizer,
            PaylineConfig paylineConfig)
        {
            _paylineChecker = paylineChecker;
            _payoutCalculator = payoutCalculator;
            _paylineVisualizer = paylineVisualizer;
            _paylineConfig = paylineConfig;
        }

        public List<(SymbolView, int)> CheckWinCombinations(SymbolView[,] visibleSymbols, int activePaylinesCount)
        {
            List<(SymbolView, int)> wins = new List<(SymbolView, int)>();

            for (int i = 0; i < activePaylinesCount; i++)
            {
                if (i >= _paylineConfig.PayLines.Count) 
                    break;

                var payline = _paylineConfig.PayLines[i];
                var matchResult = _paylineChecker.GetPaylineMatches(visibleSymbols, payline.PositionsArray);
            
                if (matchResult.Item2 > 0) 
                {
                    wins.Add(matchResult);
                }
            }

            return wins;
        }
        
        public float CalculateTotalPayout(List<(SymbolView, int)> symbolMatches, int bet)
        {
            return _payoutCalculator.CalculateTotalPayout(symbolMatches, bet);
        }
        
        public void VisualizePaylines(List<Payline> paylines, SymbolView[,] visibleSymbols, List<(SymbolView, int)> symbolsMatches, int activePaylinesCount)
        {
            _paylineVisualizer.UpdatePaylines(paylines, visibleSymbols, symbolsMatches, activePaylinesCount);
        }

        public void HideAllPaylines()
        {
            _paylineVisualizer.HideAllPaylines();
        }
    }   
}