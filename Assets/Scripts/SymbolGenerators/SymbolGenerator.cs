using RNG.Strategies;
using Symbols;

namespace SymbolGenerators
{
    public class SymbolGenerator : ISymbolGenerator
    {
        private readonly SymbolsConfig _symbolsConfig;
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        
        public SymbolGenerator(SymbolsConfig symbolsConfig, IRandomNumberGenerator randomNumberGenerator)
        {
            _symbolsConfig = symbolsConfig;
            _randomNumberGenerator = randomNumberGenerator;
        }
        
        public void GenerateNewSymbol(SymbolView symbolView)
        {
            var randomSymbol = _symbolsConfig.Symbols[_randomNumberGenerator.GetRandomNumber()];
            symbolView.Initialize(randomSymbol.Name, randomSymbol.Icon);
        }
    }
}