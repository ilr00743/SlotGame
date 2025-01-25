namespace Services
{
    public interface IPlayerFinanceService
    {
        float Balance { get; }
        int ActivePaylinesCount { get; }
        int CurrentBet { get; }
        bool IsNotEnoughMoney { get; }

        void PlaceBet();
        void AddWin(float amount);
        void UpdateFinanceDisplays();
        void UpdateBalanceOnly();
    }
}