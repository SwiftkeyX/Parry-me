using Entity;
using UnityEngine;

namespace Enemy
{
    public class Attack : BaseState<EnemyStateMachine>
    {
        private EnemyDetection _detection;

        public Attack(EnemyBlackBoard bb) : base(bb)
        {
            _detection = bb.EnemyDetection;
        }

        protected override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

        }

        protected override void CheckSwitchState()
        {
            if (_detection.CanDetect && _stateMachine.IsAggressive)
            {
                _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.CHASE);
                base.SwitchState();
            }

            else if (!_detection.CanDetect)
            {
                _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.IDLE);
                base.SwitchState();
            }
        }


    }
}