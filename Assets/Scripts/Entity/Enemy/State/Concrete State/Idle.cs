using UnityEngine;
using Entity;
/// <summary>
/// all the concrete state that inherit from base state
/// which include Idle/ Walk/ Run/ Attack/ Grounded/ Airborne/ etc..
/// </summary>

namespace Enemy
{
    public class Idle : BaseState<EnemyStateMachine>
    {
        private float _idleSpeed;

        public Idle(BaseStateMachineBlackBoard<EnemyStateMachine> bb, float moveSpeed = 0f) : base(bb)
        {
            _idleSpeed = moveSpeed;
        }

        protected override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            _animator.SetFloat("MoveSpeed", _idleSpeed, 0.1f, Time.deltaTime);

            _stateMachine.MovementMultiplierX = _idleSpeed;
        }

        protected override void CheckSwitchState()
        {
            // if (_stateMachine.IsTargetFound && _stateMachine.AttackStrategy)
            // {
            //     _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.ATTACK);
            //     base.SwitchState();
            // }

            // else if (_stateMachine.IsTargetFound && _stateMachine.IsAggressive)
            // {
            //     _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.CHASE);
            //     base.SwitchState();
            // }

            // else if (_stateMachine.IsTargetFound && !_stateMachine.IsAggressive)
            // {
            //     _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.OBSERVE);
            //     base.SwitchState();
            // }

            // else if (!_stateMachine.IsTargetFound && _stateMachine.IsGuard)
            // {
            //     _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.PATROL);
            //     base.SwitchState();
            // }

            // // else if (!_stateMachine.IsTargetFound && !_stateMachine.IsGuard)
            // // {
            // //     _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.IDLE);
            // //     base.SwitchState();
            // // }

            if (_stateMachine.IsTargetFound && _stateMachine.IsAggressive)
            {
                _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.CHASE);
                base.SwitchState();
            }
        }
    }
}