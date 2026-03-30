using UnityEngine;

public class Walk : State
{
    private float _walkSpeed;
    private float _rotationSpeed;

    public Walk(StateMachineBlackBoard bb, float moveSpeed = 3f, float rotationSpeed = 180f) : base(bb)
    {
        _walkSpeed = moveSpeed;
        _rotationSpeed = rotationSpeed;
    }

    protected override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        _animator.SetFloat("MoveSpeed", _walkSpeed, 0.1f, Time.deltaTime);

        _playerStateMachine.MovementMultiplierX = _walkSpeed;

        RotateTowardMoveDirection();
    }

    protected override void CheckSwitchState()
    {
        if (_playerStateMachine.AttackInput)
        {
            _playerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.ATTACK);
            base.SwitchState();
        }

        else if (_playerStateMachine.IsGrounded && _playerStateMachine.JumpInput)
        {
            _playerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.JUMP);
            base.SwitchState();
        }

        else if (_playerStateMachine.MovementDirection.x == 0f)
        {
            _playerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.IDLE);
            base.SwitchState();
        }

        else if (_playerStateMachine.MovementDirection.x != 0f && _playerStateMachine.RunInput)
        {
            _playerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.RUN);
            base.SwitchState();
        }

    }

    private void RotateTowardMoveDirection()
    {
        // prevent character from rotate the wrong way
        if (_playerStateMachine.MovementDirection.x == 0f) return;

        // Get target rotation
        Vector3 dir = new Vector3(_playerStateMachine.MovementDirection.x, 0, 0);
        Quaternion targetRotation = Quaternion.LookRotation(dir);

        // Smoothly rotate
        _playerStateMachine.transform.rotation = Quaternion.Slerp(
            _playerStateMachine.transform.rotation,
            targetRotation,
            _rotationSpeed * Time.deltaTime
        );
    }
}