using UnityEngine;
using Entity;
/// <summary>
/// all the concrete state that inherit from base state
/// which include Idle/ Walk/ Run/ Attack/ Grounded/ Airborne/ etc..
/// </summary>

namespace Player
{
    public class Idle : BaseState<PlayerStateMachine>
    {
        private float _idleSpeed;

        public Idle(PlayerBlackBoard bb, float moveSpeed = 0f) : base(bb)
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
            if (_stateMachine.AttackInput)
            {
                _stateMachine.ChangeCurrentState(PlayerStateMachine.STATE.ATTACK);
                base.SwitchState();
            }

            else if (_stateMachine.IsGrounded && _stateMachine.JumpInput)
            {
                _stateMachine.ChangeCurrentState(PlayerStateMachine.STATE.JUMP);
                base.SwitchState();
            }

            else if (_stateMachine.MovementDirection.x != 0f && !_stateMachine.RunInput)
            {
                _stateMachine.ChangeCurrentState(PlayerStateMachine.STATE.WALK);
                base.SwitchState();
            }

            else if (_stateMachine.MovementDirection.x != 0f && _stateMachine.RunInput)
            {
                _stateMachine.ChangeCurrentState(PlayerStateMachine.STATE.RUN);
                base.SwitchState();
            }
        }
    }
}