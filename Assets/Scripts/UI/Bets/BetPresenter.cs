using System;

namespace UI.Bets
{
    public class BetPresenter : IDisposable
    {
        private readonly BetView _betView;
        private readonly BetModel _betModel;

        public BetPresenter(BetView betView, BetModel betModel)
        {
            _betView = betView;
            _betModel = betModel;

            _betView.IncreaseBetClicked += IncreaseBet;
            _betView.DecreaseBetClicked += DecreaseBet;
        }

        public int GetBet()
        {
            return _betModel.Bet;
        }

        public int GetActivePaylinesCount()
        {
            return _betModel.ActivePaylinesCount;
        }

        public void IncreaseBet()
        {
            if (_betModel.Bet == _betModel.MaxBet)
                return;
            
            _betModel.Bet += _betModel.BetStep;
            
            _betView.UpdateBetText(_betModel.Bet);

            UpdateActivePaylinesCount();
        }

        public void DecreaseBet()
        {
            if (_betModel.Bet == _betModel.MinBet)
                return;
            
            _betModel.Bet -= _betModel.BetStep;
            
            _betView.UpdateBetText(_betModel.Bet);

            UpdateActivePaylinesCount();
        }
        
        private void UpdateActivePaylinesCount()
        {
            int expectedActivePaylines = (_betModel.Bet - _betModel.MinBet) / _betModel.BetStep + 1;

            if (expectedActivePaylines > _betModel.MaxActivePaylinesCount)
            {
                expectedActivePaylines = _betModel.MaxActivePaylinesCount;
            }

            _betModel.ActivePaylinesCount = expectedActivePaylines;
        }

        public void SetInteractable(bool isInteractable)
        {
            _betView.SetInteractable(isInteractable);
        }
            
        // Maybe unnecessary 
        public void Dispose()
        {
            _betView.IncreaseBetClicked -= IncreaseBet;
            _betView.DecreaseBetClicked -= DecreaseBet;
        }
    }
}