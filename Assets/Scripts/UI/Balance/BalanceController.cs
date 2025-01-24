using System;

namespace UI.Balance
{
    public class BalanceController
    {
        private readonly BalanceModel _balanceModel;
        private readonly BalanceView _balanceView;

        public BalanceController(BalanceModel balanceModel, BalanceView balanceView)
        {
            _balanceModel = balanceModel;
            _balanceView = balanceView;
        }

        public float Balance => _balanceModel.CurrentBalance;

        public void UpdateBalanceView()
        {
            _balanceView.UpdateBalance(_balanceModel.CurrentBalance);
        }

        public void IncreaseBalance(float value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Value cannot be negative.");
            }
            
            _balanceModel.CurrentBalance += value;
        }

        public void DecreaseBalance(float value)
        {
            if (_balanceModel.CurrentBalance < value)
                return;
            
            _balanceModel.CurrentBalance -= value;
        }
    }
}