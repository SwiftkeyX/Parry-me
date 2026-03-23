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

        _bb.CharacterController.Move(_bb.InputProcessor.MoveDirection * _runSpeed * Time.deltaTime);

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

        else if (_bb.InputProcessor.MoveDirection.sqrMagnitude > 0f && !_bb.InputProcessor.Run_input)
        {
            _bb.PlayerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.WALK);
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