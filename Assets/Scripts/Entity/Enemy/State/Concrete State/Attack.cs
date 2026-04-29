using Entity;
using UnityEngine;

namespace Enemy
{
    public class Attack : EnemyBaseState
    {
        private EnemyDetection _detection;

        public Attack(EnemyBlackBoard bb) : base(bb)
        {
            _detection = bb.EnemyDetection;
        }

        protected override void OnEnter()
        {
            base.OnEnter();

            // _animator.CrossFade("Attack", 0.1f, 0, 0f);
            _animator.SetTrigger("AttackTrigger");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

        }

        protected override void CheckSwitchState()
        {
            // Ask AI what should I do (attack, approach, retreat).
            EnemyAttackAI.STRATEGY strategy = _attackAI.ShouldAttack();
            bool isAttack = (strategy == EnemyAttackAI.STRATEGY.ATTACK);
            bool isApproach = (strategy == EnemyAttackAI.STRATEGY.APPROACH);
            bool isRetreat = (strategy == EnemyAttackAI.STRATEGY.RETREAT);

            if (_detection.CanDetect && isAttack)
            {
                _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.ATTACK);
                base.SwitchState();
            }

            else if (_detection.CanDetect && isApproach)
            {
                _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.CHASE);
                base.SwitchState();
            }

            else if (!_detection.CanDetect)
            {
                _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.IDLE);
                base.SwitchState();
            }
        }


    }
}