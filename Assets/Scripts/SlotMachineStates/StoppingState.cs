using System.Collections;
using Machine;
using Services;
using UnityEngine;

namespace SlotMachineStates
{
    public class StoppingState : ISlotMachineState 
    {
        private readonly IPaylineService _paylineService;
        private readonly IPlayerFinanceService _playerFinanceService;
        private readonly float _delayBetweenReels;
        private Coroutine _stoppingCoroutine;

        public StoppingState(float delayBetweenReels, IPlayerFinanceService playerFinanceService, IPaylineService paylineService)
        {
            _delayBetweenReels = delayBetweenReels;
            _playerFinanceService = playerFinanceService;
            _paylineService = paylineService;
        }

        public void EnterState(SlotMachine slotMachine)
        {
            _stoppingCoroutine = slotMachine.StartCoroutine(StopReelsSequentially(slotMachine));
            slotMachine.StopReel(0);
        }

        public void UpdateState(SlotMachine slotMachine) { }

        private IEnumerator StopReelsSequentially(SlotMachine slotMachine)
        {
            var delayBetweenReels = new WaitForSeconds(_delayBetweenReels);
            for (int i = 0; i < slotMachine.RollsCount; i++)
            {
                slotMachine.StopReel(i);
                
                yield return slotMachine.WaitForReelToAlign(i);
                
                yield return delayBetweenReels;
            }

            //need to prevent not adjusted paylines
            yield return new WaitForSeconds(0.5f);
            
            slotMachine.ChangeState(new PayoutState(_paylineService, _playerFinanceService, slotMachine.GetVisibleSymbols()));
        }

        public void ExitState(SlotMachine slotMachine)
        {
            if (_stoppingCoroutine != null)
            {
                slotMachine.StopCoroutine(_stoppingCoroutine);
            }
        }
    }
}