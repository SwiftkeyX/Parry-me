using UnityEngine;
using Entity;
namespace Player
{
    public class Run : State<PlayerStateMachine>
    {
        private float _runSpeed;
        private float _rotationSpeed;

        public Run(PlayerBlackBoard bb, float moveSpeed = 6f, float rotationSpeed = 180f) : base(bb)
        {
            _runSpeed = moveSpeed;
            _rotationSpeed = rotationSpeed;
        }


        protected override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            _animator.SetFloat("MoveSpeed", _runSpeed, 0.1f, Time.deltaTime);

            _stateMachine.MovementMultiplierX = _runSpeed;

            RotateTowardMoveDirection();
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


            else if (_stateMachine.MovementDirection.x == 0f)
            {
                _stateMachine.ChangeCurrentState(PlayerStateMachine.STATE.IDLE);
                base.SwitchState();
            }

            else if (_stateMachine.MovementDirection.x != 0f && !_stateMachine.RunInput)
            {
                _stateMachine.ChangeCurrentState(PlayerStateMachine.STATE.WALK);
                base.SwitchState();
            }

        }

        private void RotateTowardMoveDirection()
        {
            // prevent character from rotate the wrong way
            if (_stateMachine.MovementDirection.x == 0f) return;

            // Get target rotation
            Vector3 dir = new Vector3(_stateMachine.MovementDirection.x, 0, 0);
            Quaternion targetRotation = Quaternion.LookRotation(dir);

            // Smoothly rotate
            _stateMachine.transform.rotation = Quaternion.Slerp(
                _stateMachine.transform.rotation,
                targetRotation,
                _rotationSpeed * Time.deltaTime
            );
        }
    }
}