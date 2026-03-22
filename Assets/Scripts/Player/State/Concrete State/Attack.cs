using UnityEngine;

public class Attack : State
{
    public Attack(StateMachineBlackBoard bb) : base(bb)
    {
    }

    protected override void OnEnter()
    {
        base.OnEnter();

        _bb.Animator.SetTrigger("Attack");

        // reset var 
        // (if we reset var in OnExit(), it won't work because the var is reset too fast that animator can't read it)
        _bb.Animator.SetBool("AllowNextAttack", false);
        _bb.Animator.SetBool("AttackCancel", false);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();


    }

    protected override void OnExit()
    {
    }

    protected override void CheckSwitchState()
    {
        if (!AllowSwitchState())
        {
            return;
        }

        if (_animator.GetBool("AllowNextAttack"))
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

    private bool AllowSwitchState()
    {
        AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
        bool animatorInAttack = info.IsTag("Attack");

        // pre condition
        if (!animatorInAttack) return false;

        // necessary var
        float animatorTime = info.normalizedTime;

        // condition
        bool continueAttack = _bb.InputProcessor.Attack_input;
        bool attackWindow = (animatorTime > 0.3f && animatorTime < 0.9f);
        bool inCancelWindow = (animatorTime > 0.7f && animatorTime < 0.9f);
        bool attackIsFinish = (animatorTime >= 0.9f);

        Debug.Log(
            $"continueAttack: {continueAttack} | " +
            $"attackWindow: {attackWindow} | " +
            $"inCancelWindow: {inCancelWindow} | " +
            $"attackIsFinish: {attackIsFinish} | " +
            $"animatorInAttack: {animatorInAttack} | " +
            $"animatorTime: {animatorTime:F2}"
        );

        if (attackWindow && continueAttack)
        {
            _bb.Animator.SetBool("AllowNextAttack", true);
            return true;
        }

        else if (inCancelWindow)
        {
            _bb.Animator.SetBool("AttackCancel", true);
            return true;
        }

        else if (attackIsFinish)
        {
            return true;
        }

        return false;
    }

}