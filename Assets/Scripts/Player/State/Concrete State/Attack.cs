using UnityEngine;

public class Attack: State
{
    public Attack(StateMachineBlackBoard bb): base(bb)
    {
        
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        _bb.Animator.SetTrigger("Attack");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    protected override void CheckSwitchState()
    {
        if (_bb.InputProcessor.MoveDirection.sqrMagnitude == 0f)
        {
            _bb.PlayerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.IDLE);
        }

        else if (_bb.InputProcessor.MoveDirection.sqrMagnitude > 0f && !_bb.InputProcessor.Run_input)
        {
            _bb.PlayerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.WALK);
        }

        else if (_bb.InputProcessor.MoveDirection.sqrMagnitude > 0f && _bb.InputProcessor.Run_input)
        {
            _bb.PlayerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.RUN);
        }

        base.CheckSwitchState();
    }
}