using Machine;

namespace SlotMachineStates
{
    public class IdleState : ISlotMachineState
    {
        public void EnterState(SlotMachine slotMachine)
        {
            slotMachine.SetInteractableUI(true);
        }

        public void UpdateState(SlotMachine slotMachine) { }

        public void ExitState(SlotMachine slotMachine)
        {
            slotMachine.SetInteractableUI(false);
        }
    }
}