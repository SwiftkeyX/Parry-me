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
    }

    protected override void CheckSwitchState()
    {
        if (_bb.InputProcessor.Attack_input)
        {
            _bb.PlayerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.ATTACK);
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