using Machine;
using Services;
using UnityEngine;

namespace SlotMachineStates
{
    public class SpinningState : ISlotMachineState
    {
        private readonly PaylineService _paylineService;
        private readonly PlayerFinanceService _playerFinanceService;
        private readonly float _spinDuration;
        private float _elapsedTime;
        
        public SpinningState(float spinDuration, PaylineService paylineService, PlayerFinanceService playerFinanceService)
        {
            _spinDuration = spinDuration;
            _paylineService = paylineService;
            _playerFinanceService = playerFinanceService;
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
                slotMachine.ChangeState(new StoppingState(slotMachine.RollConfig.DelayBetweenRollsStop, _playerFinanceService, _paylineService));
            }
        }

        public void ExitState(SlotMachine slotMachine) { }
    }
}