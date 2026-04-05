using UnityEngine;

/// <summary>
/// how do I make Attack combo ?
/// </summary>
public class Attack : State
{
    private PlayerAttackManager _PlayerAttackManager;
    private CollisionCreater _CollisionCreater;

    public Attack(StateMachineBlackBoard bb) : base(bb)
    {
        _PlayerAttackManager = bb.PlayerAttackManager;
        _CollisionCreater = bb.CollisionCreater;
    }

    protected override void OnEnter()
    {
        base.OnEnter();

        _animator.SetFloat("MoveSpeed", 0f);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        // if (_playerStateMachine.AttackInput) _PlayerAttackManager.Attack();
        _PlayerAttackManager.Attack(_playerStateMachine.AttackInput);

        // player don't move when attacking
        _playerStateMachine.MovementMultiplierX = 0f;
    }

    protected override void OnExit()
    {
    }

    protected override void CheckSwitchState()
    {
        if (!_PlayerAttackManager.AttackBuffer.IsAttackFinish) return;

        if (_playerStateMachine.MovementDirection.x == 0f)
        {
            _playerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.IDLE);
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