using System.Collections.Generic;
using Symbols;

namespace Paylines
{
    public class PaylineChecker
    {
        public (SymbolView symbol, int matches) GetPaylineMatches(SymbolView[,] reels, bool[,] payline)
        {
            SymbolView firstSymbol = null;
            int matches = 0;

            int rows = payline.GetLength(0);
            int cols = payline.GetLength(1);

            // Перевіряємо колонки ЗЛІВА НАПРАВО
            for (int col = 0; col < cols; col++)
            {
                // Шукаємо активний рядок для поточної колонки в пейлайні
                for (int row = 0; row < rows; row++)
                {
                    if (payline[row, col])
                    {
                        SymbolView currentSymbol = reels[row, col];

                        if (firstSymbol == null)
                        {
                            firstSymbol = currentSymbol;
                            matches = 1;
                        }
                        else if (currentSymbol.Name == firstSymbol.Name)
                        {
                            matches++;
                        }
                        else
                        {
                            // Зупиняємося, якщо символ не збігається
                            return (firstSymbol, matches);
                        }

                        break; // Переходимо до наступної колонки
                    }
                }
            }

            return (firstSymbol, matches);
        }
    }


}