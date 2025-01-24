using Machine;

namespace SlotMachineStates
{
    public interface ISlotMachineState
    {
        void EnterState(SlotMachine slotMachine);
        void UpdateState(SlotMachine slotMachine);
        void ExitState(SlotMachine slotMachine);
    }
}