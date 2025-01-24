using Machine;

namespace SlotMachineStates
{
    public class PayoutState : ISlotMachineState
    {
        public void EnterState(SlotMachine slotMachine)
        {
            float totalPayout = slotMachine.CalculatePayout();
            
            slotMachine.UpdateBalance(totalPayout);
            
            slotMachine.ShowPayoutResult(totalPayout);
            
            slotMachine.ChangeState(new IdleState());
        }

        public void UpdateState(SlotMachine slotMachine) { }

        public void ExitState(SlotMachine slotMachine) { }
    }
}