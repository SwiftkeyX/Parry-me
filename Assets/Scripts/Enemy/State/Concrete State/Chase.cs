using UnityEngine;

namespace Enemy
{
    public class Chase : State
    {
        private float _moveSpeed;
        public Chase(StateMachineBlackBoard bb, float moveSpeed = 0f) : base(bb)
        {
            _moveSpeed = moveSpeed;
        }

        protected override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            _animator.SetFloat("MoveSpeed", _moveSpeed, 0.1f, Time.deltaTime);

            _stateMachine.MovementMultiplierX = _moveSpeed;
        }

        protected override void CheckSwitchState()
        {
            if (_stateMachine.IsTargetFound && _stateMachine.AttackStrategy)
            {
                _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.ATTACK);
                base.SwitchState();
            }

            else if (_stateMachine.IsTargetFound && !_stateMachine.IsAggressive)
            {
                _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.OBSERVE);
                base.SwitchState();
            }

            else if (!_stateMachine.IsTargetFound && _stateMachine.IsGuard)
            {
                _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.PATROL);
                base.SwitchState();
            }

            else if (!_stateMachine.IsTargetFound && !_stateMachine.IsGuard)
            {
                _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.IDLE);
                base.SwitchState();
            }
        }
    }
}