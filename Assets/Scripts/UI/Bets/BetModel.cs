namespace UI.Bets
{
    public class BetModel
    {
        public int Bet { get; set; }

        public int ActivePaylinesCount { get; set; }

        public int BetStep { get; }
        
        public int MinBet { get; }
        
        public int MaxBet { get; }
        
        public int MaxActivePaylinesCount { get; set; }

        public BetModel(int betStep, int minBet, int maxBet, int maxActivePaylinesCount)
        {
            Bet = minBet;
            ActivePaylinesCount = 1;
            BetStep = betStep;
            MinBet = minBet;
            MaxBet = maxBet;
            MaxActivePaylinesCount = maxActivePaylinesCount;
        }
    }
}