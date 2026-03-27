using UnityEngine;

public class Run : State
{
    private float _runSpeed;
    private float _rotationSpeed;

    public Run(StateMachineBlackBoard bb, float moveSpeed = 6f, float rotationSpeed = 180f) : base(bb)
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

        _playerStateMachine.MovementMultiplierX = _runSpeed;

        RotateTowardMoveDirection();
    }

    protected override void CheckSwitchState()
    {
        if (_playerStateMachine.AttackInput)
        {
            _playerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.ATTACK);
            base.SwitchState();
        }

        else if (_playerStateMachine.JumpInput)
        {
            _playerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.JUMP);
            base.SwitchState();
        }


        else if (_playerStateMachine.MovementDirection.x == 0f)
        {
            _playerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.IDLE);
            base.SwitchState();
        }

        else if (_playerStateMachine.MovementDirection.x != 0f && !_playerStateMachine.RunInput)
        {
            _playerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.WALK);
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