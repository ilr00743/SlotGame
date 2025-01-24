namespace UI.Balance
{
    public class BalanceModel
    {
        public float CurrentBalance { get; set; }

        public BalanceModel(float currentBalance)
        {
            CurrentBalance = currentBalance;
        }
    }
}