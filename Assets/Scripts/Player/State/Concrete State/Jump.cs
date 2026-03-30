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

        _animator.CrossFade("jump", 0.05f);

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
        if (!_jumpOnce || !_playerStateMachine.IsGrounded)
        {
            return;
        }

        if (_playerStateMachine.AttackInput)
        {
            _playerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.ATTACK);
            base.SwitchState();
        }

        else if (_playerStateMachine.MovementDirection.x == 0f)
        {
            _playerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.IDLE);
            base.SwitchState();
        }

        else if (_playerStateMachine.MovementDirection.x != 0f && !_playerStateMachine.RunInput)
        {
            Debug.Log("I am here");
            _playerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.WALK);
            base.SwitchState();
        }

        else if (_playerStateMachine.MovementDirection.x != 0f && _playerStateMachine.RunInput)
        {
            _playerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.RUN);
            base.SwitchState();
        }

    }

    private void DoTheJump()
    {
        if (_jumpOnce) return;

        AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
        bool inJumpAnimatorState = info.IsTag("Jump");
        bool inJumpWindow = (info.normalizedTime > 0.4);

        if (inJumpWindow && inJumpAnimatorState)
        {
            _playerStateMachine.MovementMultiplierY = _jumpForce / 2f;
            _jumpOnce = true;
        }

    }
}