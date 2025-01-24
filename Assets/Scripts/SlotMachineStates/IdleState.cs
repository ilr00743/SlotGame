using Machine;

namespace SlotMachineStates
{
    public class IdleState : ISlotMachineState
    {
        public void EnterState(SlotMachine slotMachine)
        {
            slotMachine.SetSpinButtonInteractable(true);
            slotMachine.SetBetInteractable(true);
        }

        public void UpdateState(SlotMachine slotMachine) { }

        public void ExitState(SlotMachine slotMachine)
        {
            slotMachine.SetSpinButtonInteractable(false);
            slotMachine.SetBetInteractable(false);
        }
    }
}