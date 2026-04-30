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

            _attackAI.Attack();

            _animator.SetTrigger("AttackTrigger");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

        }

        protected override void CheckSwitchState()
        {
            // Ask AI what should I do (attack, approach, retreat).
            EnemyAttackAIForRealTimeCombat.STRATEGY strategy = _attackAI.ShouldAttack();
            bool isAttack = (strategy == EnemyAttackAIForRealTimeCombat.STRATEGY.ATTACK);
            bool isApproach = (strategy == EnemyAttackAIForRealTimeCombat.STRATEGY.APPROACH);
            bool isRetreat = (strategy == EnemyAttackAIForRealTimeCombat.STRATEGY.RETREAT);
            bool isWait = (strategy == EnemyAttackAIForRealTimeCombat.STRATEGY.WAIT);

            if (isWait) return;

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

            else if (!_detection.CanDetect && isRetreat)
            {
                _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.IDLE);
                base.SwitchState();
            }
        }


    }
}