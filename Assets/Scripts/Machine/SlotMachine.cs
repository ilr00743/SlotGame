using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Paylines;
using Payout;
using Rolls;
using SlotMachineStates;
using SymbolGenerators;
using Symbols;
using UI.Balance;
using UI.Bets;
using UI.TotalPayout;
using UnityEngine;

namespace Machine
{
    public class SlotMachine : MonoBehaviour
    {
        [SerializeField] private List<Roll> _rolls;
        [SerializeField] private float _spinDuration = 3f;
        [SerializeField] private float _delayBetweenRollsStopping = 1f;
        [SerializeField] private PaylineConfig _paylineConfig;
        [SerializeField] private RollConfig _rollConfig;

        private RollsManager _rollsManager;

        private PaylineChecker _paylineChecker;
        private PaylineVisualizer _paylineVisualizer;
        
        private BetPresenter _betPresenter;
        private TotalPayoutController _totalPayoutController;
        private BalanceController _balanceController;
        private SpinButtonView _spinButton;

        private PayoutCalculator _payoutCalculator;

        private ISlotMachineState _currentState;

        public int RollsCount => _rolls.Count;

        public void Initialize(
            PaylineChecker paylineChecker,
            PayoutCalculator payoutCalculator, 
            BalanceController balanceController, 
            BetPresenter betPresenter, 
            TotalPayoutController totalPayoutController,
            SpinButtonView spinButton,
            PaylineVisualizer paylineVisualizer,
            ISymbolGenerator symbolGenerator)
        {
            _paylineChecker = paylineChecker;
            _payoutCalculator = payoutCalculator;
            _balanceController = balanceController;
            _betPresenter = betPresenter;
            _totalPayoutController = totalPayoutController;
            _paylineVisualizer = paylineVisualizer;
            _spinButton = spinButton;
            
            foreach (var roll in _rolls)
            {
                roll.Initialize(symbolGenerator, 5, 10);
            }
            
            _rollsManager = new RollsManager(_rolls);
            
            _spinButton.Clicked += Spin;
            ChangeState(new IdleState());
        }

        private void Update()
        {
            _currentState?.UpdateState(this);
        }

        public void ChangeState(ISlotMachineState newState)
        {
            _currentState?.ExitState(this);
            _currentState = newState;
            _currentState.EnterState(this);
        }
        
        public void StartRollsSpin()
        {
            _rollsManager.StartSpin();
        }

        private void Spin()
        {
            if (_balanceController.Balance <= 0)
                return;
            
            _balanceController.DecreaseBalance(_betPresenter.GetBet());
            _balanceController.UpdateBalanceView();

            _rollsManager.ResetVisibleSymbols();
            
            _paylineVisualizer.HideAllPaylines();
            
            ChangeState(new SpinningState(_rollsManager.SpinDuration));
        }

        public void StopReel(int reelIndex)
        {
            _rollsManager.StopReel(reelIndex);
        }
        
        public IEnumerator WaitForReelToAlign(int reelIndex)
        {
            Roll reel = _rolls[reelIndex];
            while (reel.IsAligning)
            {
                yield return null;
            }
        }

        public float CalculatePayout()
        {
            var symbolsMatchesList = GetSymbolsMatches();

            var totalPayout = _payoutCalculator.CalculateTotalPayout(symbolsMatchesList, _betPresenter.GetBet());

            _paylineVisualizer.UpdatePaylines(_paylineConfig.PayLines.ToList(), _rollsManager.GetVisibleSymbols(), symbolsMatchesList, _betPresenter.GetActivePaylinesCount());

            return totalPayout;
        }

        private List<(SymbolView, int)> GetSymbolsMatches()
        {
            List<(SymbolView, int)> symbolMatchesList = new List<(SymbolView, int)>();

            for (int i = 0; i < _betPresenter.GetActivePaylinesCount(); i++)
            {
                if (i >= _paylineConfig.PayLines.Count)
                    break;
                
                var symbolMatches = _paylineChecker.GetPaylineMatches(_rollsManager.GetVisibleSymbols(),
                    _paylineConfig.PayLines[i].PositionsArray);
                
                symbolMatchesList.Add(symbolMatches);
            }

            return symbolMatchesList;
        }

        public void SetBetInteractable(bool isInteractable)
        {
            _betPresenter.SetInteractable(isInteractable);
        }

        public void SetSpinButtonInteractable(bool isInteractable)
        {
            _spinButton.SetInteractable(isInteractable);
        }
        
        public void UpdateBalance(float value)
        {
            _balanceController.IncreaseBalance(value);
            _balanceController.UpdateBalanceView();
        }

        public void ShowPayoutResult(float value)
        {
            _totalPayoutController.SetTotalPayout(value);
            _totalPayoutController.UpdateView();
        }
    }
}