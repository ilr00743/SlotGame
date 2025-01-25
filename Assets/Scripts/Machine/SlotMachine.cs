using System.Collections;
using System.Collections.Generic;
using Rolls;
using Services;
using SlotMachineStates;
using SymbolGenerators;
using Symbols;
using UnityEngine;

namespace Machine
{
    public class SlotMachine : MonoBehaviour
    {
        [SerializeField] private List<Roll> _rolls;
        [SerializeField] private RollConfig _rollConfig;

        private RollsManager _rollsManager;
        private ISlotMachineState _currentState;
        private IPaylineService _paylineService;
        private IPlayerFinanceService _playerFinanceService;
        private IMachineButtonsService _machineButtonsService;
        
        public int RollsCount => _rolls.Count;
        public RollConfig RollConfig => _rollConfig;
        public SymbolView[,] GetVisibleSymbols() => _rollsManager.GetVisibleSymbols();

        public void Initialize(
            IPaylineService paylineService,
            IPlayerFinanceService playerFinanceService,  
            IMachineButtonsService machineButtonsService,
            ISymbolGenerator symbolGenerator)
        {
            _paylineService = paylineService;
            _playerFinanceService = playerFinanceService;
            _machineButtonsService = machineButtonsService;
            
            InitializeRolls(symbolGenerator);

            _machineButtonsService.AddListenerOnSpin(Spin);
            _paylineService.HideAllPaylines();
            ChangeState(new IdleState());
        }

        private void InitializeRolls(ISymbolGenerator symbolGenerator)
        {
            foreach (var roll in _rolls)
            {
                roll.Initialize(symbolGenerator, _rollConfig.MinSymbols, _rollConfig.MaxSymbols);
            }

            _rollsManager = new RollsManager(_rolls);
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
            if (_playerFinanceService.IsNotEnoughMoney)
                return;
            
            _playerFinanceService.PlaceBet();

            _rollsManager.ResetVisibleSymbols();
            
            _paylineService.HideAllPaylines();
            
            ChangeState(new SpinningState(_rollConfig.SpinDuration, _paylineService, _playerFinanceService));
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

        public void SetInteractableUI(bool isInteractable)
        {
            _machineButtonsService.SetInteractable(isInteractable);
        }
    }
}