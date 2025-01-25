namespace UI.Balance
{
    public class BalanceModel
    {
        private float _currentBalance;
        
        public float CurrentBalance
        {
            get => _currentBalance;
            set => _currentBalance = value > 0 ? value : 0;
        }

        public BalanceModel(float currentBalance)
        {
            _currentBalance = currentBalance;
        }
    }
}