using Machine;
using UnityEngine;

namespace SlotMachineStates
{
    public class SpinningState : ISlotMachineState
    {
        private float _spinDuration;
        private float _elapsedTime;

        public SpinningState(float spinDuration)
        {
            _spinDuration = spinDuration;
        }

        public void EnterState(SlotMachine slotMachine)
        {
            _elapsedTime = 0;
            slotMachine.StartRollsSpin();
        }

        public void UpdateState(SlotMachine slotMachine)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _spinDuration)
            {
                slotMachine.ChangeState(new StoppingState(2f));
            }
        }

        public void ExitState(SlotMachine slotMachine) { }
    }
}