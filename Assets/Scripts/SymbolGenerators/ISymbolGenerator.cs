using Symbols;

namespace SymbolGenerators
{
    public interface ISymbolGenerator
    {
        void GenerateNewSymbol(SymbolView symbolView);
    }
}