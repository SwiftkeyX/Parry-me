using UnityEngine;
using Entity;
/// <summary>
/// all the concrete state that inherit from base state
/// which include Idle/ Walk/ Run/ Attack/ Grounded/ Airborne/ etc..
/// </summary>

namespace Enemy
{
    public class Idle : EnemyBaseState
    {
        private float _idleSpeed;
        private EnemyDetection _detection;

        public Idle(EnemyBlackBoard bb, float moveSpeed = 0f) : base(bb)
        {
            _idleSpeed = moveSpeed;
            _detection = bb.EnemyDetection;
        }

        protected override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            _animator.SetFloat("MoveSpeed", _idleSpeed, 0.1f, Time.deltaTime);

            _stateMachine.MovementMultiplierX = _idleSpeed;
        }

        protected override void CheckSwitchState()
        {
            // Ask AI what should I do (attack, approach, retreat).
            EnemyAttackAIForRealTimeCombat.STRATEGY strategy = _attackAI.ShouldAttack();
            bool isAttack = (strategy == EnemyAttackAIForRealTimeCombat.STRATEGY.ATTACK);
            bool isApproach = (strategy == EnemyAttackAIForRealTimeCombat.STRATEGY.APPROACH);
            bool isRetreat = (strategy == EnemyAttackAIForRealTimeCombat.STRATEGY.RETREAT);

            Debug.Log("Detection: " + _detection.CanDetect + " Strategy: " + strategy);

            if (_detection.CanDetect && isAttack)
            {
                _attackAI.Attack();
                _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.ATTACK);
                base.SwitchState();
            }

            else if (_detection.CanDetect && isApproach)
            {
                _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.CHASE);
                base.SwitchState();
            }
        }
    }
}