namespace RNG.Strategies
{
    public class GuaranteedWinRandom : IRandomNumberGenerator
    {
        private readonly int _symbolIndex;
        
        public GuaranteedWinRandom(int symbolIndex)
        {
            _symbolIndex = symbolIndex;
        }
        
        public int GetRandomNumber()
        {
            return _symbolIndex < 0 ? 0 : _symbolIndex;
        }
    }
}