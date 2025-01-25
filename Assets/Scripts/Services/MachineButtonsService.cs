using System;
using UI.Bets;

namespace Services
{
    public class MachineButtonsService
    {
        private readonly BetPresenter _betPresenter;
        private readonly SpinButtonView _spinButtonView;

        public MachineButtonsService(BetPresenter betPresenter, SpinButtonView spinButtonView)
        {
            _betPresenter = betPresenter;
            _spinButtonView = spinButtonView;
        }

        public void SetInteractable(bool isInteractable)
        {
            _spinButtonView.SetInteractable(isInteractable);
            _betPresenter.SetInteractable(isInteractable);
        }

        public void AddListenerOnSpin(Action listener)
        {
            _spinButtonView.Clicked += listener;
        }
        
    }
}