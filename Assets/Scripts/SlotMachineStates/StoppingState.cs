using System.Collections;
using Machine;
using UnityEngine;

namespace SlotMachineStates
{
    public class StoppingState : ISlotMachineState 
    {
        private int _currentReelIndex;
        private float _delayBetweenReels;
        private float _startTime;
        private Coroutine _stoppingCoroutine;

        public StoppingState(float delayBetweenReels)
        {
            _delayBetweenReels = delayBetweenReels;
        }

        public void EnterState(SlotMachine slotMachine)
        {
            _stoppingCoroutine = slotMachine.StartCoroutine(StopReelsSequentially(slotMachine));
            _startTime = 0;
            _currentReelIndex = 0;
            slotMachine.StopReel(_currentReelIndex);
        }

        public void UpdateState(SlotMachine slotMachine) { }

        private IEnumerator StopReelsSequentially(SlotMachine slotMachine)
        {
            for (int i = 0; i < slotMachine.RollsCount; i++)
            {
                slotMachine.StopReel(i);
                
                yield return slotMachine.WaitForReelToAlign(i);
                
                yield return new WaitForSeconds(_delayBetweenReels);
            }
            
            slotMachine.ChangeState(new PayoutState());
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