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

        _bb.CharacterController.Move(_bb.InputProcessor.MoveDirection * _walkSpeed * Time.deltaTime);

        RotateTowardMoveDirection();
    }

    protected override void CheckSwitchState()
    {
        if (_bb.InputProcessor.Attack_input)
        {
            _bb.PlayerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.ATTACK);
            base.SwitchState();
        }

        else if (_bb.InputProcessor.MoveDirection.sqrMagnitude == 0f)
        {
            _bb.PlayerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.IDLE);
            base.SwitchState();
        }

        else if (_bb.InputProcessor.MoveDirection.sqrMagnitude > 0f && _bb.InputProcessor.Run_input)
        {
            _bb.PlayerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.RUN);
            base.SwitchState();
        }

    }

    private void RotateTowardMoveDirection()
    {
        // shutup Unity's console log
        if (_bb.InputProcessor.MoveDirection.sqrMagnitude < 0.001f) return;

        // Get target rotation
        Quaternion targetRotation = Quaternion.LookRotation(_bb.InputProcessor.MoveDirection);

        // Smoothly rotate
        _bb.transform.rotation = Quaternion.Slerp(
            _bb.transform.rotation,
            targetRotation,
            _rotationSpeed * Time.deltaTime
        );
    }
}