using UnityEngine;

public class Jump : State
{
    private float _jumpSpeed;

    public Jump(StateMachineBlackBoard bb, float jumpSpeed = 10f) : base(bb)
    {
        _jumpSpeed = jumpSpeed;
    }

    protected override void OnEnter()
    {
        base.OnEnter();

        // _animator.SetTrigger("Jump");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        Vector3 jumpDirection = new Vector3(_bb.InputProcessor.MoveDirection.x, 1, _bb.InputProcessor.MoveDirection.z);

        _bb.CharacterController.Move(jumpDirection * _jumpSpeed * Time.deltaTime);
    }

    protected override void CheckSwitchState()
    {
        if (!_characterController.isGrounded) return;

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

        else if (_bb.InputProcessor.MoveDirection.sqrMagnitude > 0f && _bb.InputProcessor.Run_input)
        {
            _bb.PlayerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.RUN);
            base.SwitchState();
        }

    }

}