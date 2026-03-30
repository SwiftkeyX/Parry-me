using UnityEngine;

/// <summary>
/// all the concrete state that inherit from base state
/// which include Idle/ Walk/ Run/ Attack/ Grounded/ Airborne/ etc..
/// </summary>
public class Idle : State
{
    private float _idleSpeed;

    public Idle(StateMachineBlackBoard bb, float moveSpeed = 0f) : base(bb)
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

        _playerStateMachine.MovementMultiplierX = _idleSpeed;
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

        else if (_playerStateMachine.MovementDirection.x != 0f && !_playerStateMachine.RunInput)
        {
            _playerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.WALK);
            base.SwitchState();
        }

        else if (_playerStateMachine.MovementDirection.x != 0f && _playerStateMachine.RunInput)
        {
            _playerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.RUN);
            base.SwitchState();
        }
    }
}