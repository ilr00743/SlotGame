using System.Collections.Generic;

namespace Symbols
{
    public class VisibleSymbolsHolder
    {
        private readonly int _rows;
        private readonly int _columns;

        public SymbolView[,] VisibleSymbols { get; private set; }

        public VisibleSymbolsHolder(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            VisibleSymbols = new SymbolView[rows, columns];
        }

        public void SetSymbolsInColumn(int columnIndex, List<SymbolView> symbols)
        {
            for (int i = 0; i < _rows; i++)
            {
                VisibleSymbols[i, columnIndex] = symbols[i];
            }
        }

        public void Reset()
        {
            if (VisibleSymbols.Length == 0) return;
            
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    VisibleSymbols[i, j] = null;
                }
            }
        }
    }
}