using UI.Balance;
using UI.Bets;
using UI.TotalPayout;

namespace Services
{
    public class PlayerFinanceService : IPlayerFinanceService
    {
        private readonly BalanceController _balanceController;
        private readonly BetPresenter _betPresenter;
        private readonly TotalPayoutController _totalPayoutController;

        public PlayerFinanceService(
            BalanceController balanceController,
            BetPresenter betPresenter,
            TotalPayoutController totalPayoutController)
        {
            _balanceController = balanceController;
            _betPresenter = betPresenter;
            _totalPayoutController = totalPayoutController;
        }

        public float Balance => _balanceController.Balance;
        public int ActivePaylinesCount => _betPresenter.GetActivePaylinesCount();

        public int CurrentBet => _betPresenter.GetBet();
        public bool IsNotEnoughMoney => Balance < CurrentBet;

        public void PlaceBet()
        {
            float bet = _betPresenter.GetBet();
            _balanceController.DecreaseBalance(bet);
            UpdateBalanceOnly();
        }
        
        public void AddWin(float amount)
        {
            _balanceController.IncreaseBalance(amount);
            _totalPayoutController.SetTotalPayout(amount);
            UpdateFinanceDisplays();
        }
        
        public void UpdateFinanceDisplays()
        {
            _balanceController.UpdateBalanceView();
            _totalPayoutController.UpdateView();
        }
        
        public void UpdateBalanceOnly()
        {
            _balanceController.UpdateBalanceView();
        }
    }
}