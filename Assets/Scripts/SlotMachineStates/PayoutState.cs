using System.Collections;
using Machine;
using Services;
using Symbols;
using UnityEngine;

namespace SlotMachineStates
{
    public class PayoutState : ISlotMachineState
    {
        private readonly IPaylineService _paylineService;
        private readonly IPlayerFinanceService _financeService;
        private readonly SymbolView[,] _visibleSymbols;
        private readonly int _currentBet;

        public PayoutState(
            IPaylineService paylineService,
            IPlayerFinanceService financeService,
            SymbolView[,] visibleSymbols
            )
        {
            _paylineService = paylineService;
            _financeService = financeService;
            _visibleSymbols = visibleSymbols;
            _currentBet = _financeService.CurrentBet;
        }

        public void EnterState(SlotMachine slotMachine)
        {
            var wins = _paylineService
                .CheckWinCombinations(
                _visibleSymbols, 
                _financeService.ActivePaylinesCount
            );
            
            float totalPayout = _paylineService.CalculateTotalPayout(wins, _currentBet);
            
            if (totalPayout > 0)
            {
                _financeService.AddWin(totalPayout);
            }

            _financeService.UpdateFinanceDisplays();
            _paylineService.VisualizePaylines(
                _paylineService.GetPaylines,
                _visibleSymbols,
                wins,
                _financeService.ActivePaylinesCount
            );
            
            slotMachine.StartCoroutine(ReturnToIdleState(slotMachine, 0.5f));
        }

        private IEnumerator ReturnToIdleState(SlotMachine slotMachine, float delay)
        {
            yield return new WaitForSeconds(delay);
            slotMachine.ChangeState(new IdleState());
        }

        public void UpdateState(SlotMachine slotMachine) { }
        public void ExitState(SlotMachine slotMachine) { }
    }
}