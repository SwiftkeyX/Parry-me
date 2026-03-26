using UnityEngine;

/// <summary>
/// include Jump and Falling animation
/// </summary>
public class Jump : State
{
    private float _jumpForce;
    private bool _jumpOnce;

    public Jump(StateMachineBlackBoard bb, float jumpForce = 1f) : base(bb)
    {
        _jumpForce = jumpForce;
    }

    protected override void OnEnter()
    {
        base.OnEnter();

        _animator.CrossFade("jump", 0.1f);
        
        _animator.SetBool("Grounded", false);

        _jumpOnce = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        DoTheJump();
    }

    protected override void OnExit()
    {
        base.OnExit();

        _animator.SetBool("Grounded", true);
    }

    protected override void CheckSwitchState()
    {
        if (!_jumpOnce || !_characterController.isGrounded) return;

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

    private void DoTheJump()
    {
        if (_jumpOnce) return;

        AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
        bool inJumpAnimatorState = info.IsTag("Jump");
        bool inJumpWindow = (info.normalizedTime > 0.1);

        if (inJumpWindow && inJumpAnimatorState)
        {
            _bb.VerticalMovement = _jumpForce;
            _jumpOnce = true;
        }
    }
}