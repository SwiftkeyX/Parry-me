using UnityEngine;

/// <summary>
/// how do I make Attack combo ?
/// </summary>
public class Attack : State
{
    public Attack(StateMachineBlackBoard bb) : base(bb)
    {
    }

    protected override void OnEnter()
    {
        base.OnEnter();

        _bb.Animator.SetFloat("MoveSpeed", 0f);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_bb.InputProcessor.Attack_input) _bb.AttackComboData.Attack();
    }

    protected override void OnExit()
    {
    }

    protected override void CheckSwitchState()
    {
        if (!_bb.AttackComboData.IsAttackFinish()) return;

        if (_bb.InputProcessor.MoveDirection.sqrMagnitude == 0f)
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