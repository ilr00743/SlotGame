using System.Collections.Generic;
using Symbols;

namespace Services
{
    public interface IPaylineService
    {
        List<(SymbolView, int)> CheckWinCombinations(SymbolView[,] visibleSymbols, int activePaylinesCount);
        
        float CalculateTotalPayout(List<(SymbolView, int)> symbolMatches, int bet);

        void VisualizePaylines(List<Payline> paylines, SymbolView[,] visibleSymbols,
            List<(SymbolView, int)> symbolsMatches, int activePaylinesCount);

        void HideAllPaylines();

        List<Payline> GetPaylines { get; }
    }
}