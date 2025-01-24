namespace UI.TotalPayout
{
    public class TotalPayoutController
    {
        private readonly TotalPayoutModel _totalPayoutModel;
        private readonly TotalPayoutView _totalPayoutView;

        public TotalPayoutController(TotalPayoutModel totalPayoutModel, TotalPayoutView totalPayoutView)
        {
            _totalPayoutModel = totalPayoutModel;
            _totalPayoutView = totalPayoutView;
        }

        public void SetTotalPayout(float value)
        {
            _totalPayoutModel.TotalPayout = value;
        }

        public void UpdateView()
        {
            _totalPayoutView.UpdateText(_totalPayoutModel.TotalPayout);
        }

        public float GetTotalPayout()
        {
            return _totalPayoutModel.TotalPayout;
        }
    }
}