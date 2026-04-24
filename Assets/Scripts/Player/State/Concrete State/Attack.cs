using UnityEngine;
using Entity;
namespace Player
{
    /// <summary>
    /// how do I make Attack combo ?
    /// </summary>
    public class Attack : State<PlayerStateMachine>
    {
        private PlayerAttackManager _PlayerAttackManager;
        private CollisionCreater _CollisionCreater;

        public Attack(StateMachineBlackBoard<PlayerStateMachine> bb) : base(bb)
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

            // if (_stateMachine.AttackInput) _PlayerAttackManager.Attack();
            _PlayerAttackManager.Attack(_stateMachine.AttackInput);

            // player don't move when attacking
            _stateMachine.MovementMultiplierX = 0f;
        }

        protected override void OnExit()
        {
        }

        protected override void CheckSwitchState()
        {
            if (!_PlayerAttackManager.AttackBuffer.IsAttackFinish) return;

            if (_stateMachine.MovementDirection.x == 0f)
            {
                _stateMachine.ChangeCurrentState(PlayerStateMachine.STATE.IDLE);
                base.SwitchState();
            }

            else if (_stateMachine.MovementDirection.x != 0f && !_stateMachine.RunInput)
            {
                _stateMachine.ChangeCurrentState(PlayerStateMachine.STATE.WALK);
                base.SwitchState();
            }

            else if (_stateMachine.MovementDirection.x != 0f && _stateMachine.RunInput)
            {
                _stateMachine.ChangeCurrentState(PlayerStateMachine.STATE.RUN);
                base.SwitchState();
            }
        }
    }
}