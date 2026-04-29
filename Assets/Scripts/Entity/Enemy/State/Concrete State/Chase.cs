using UnityEngine;
using Entity;
/// <summary>
/// all the concrete state that inherit from base state
/// which include Idle/ Walk/ Run/ Attack/ Grounded/ Airborne/ etc..
/// </summary>

namespace Enemy
{
    public class Chase : EnemyBaseState
    {
        private float _moveSpeed;
        private EnemyDetection _detection;

        public Chase(EnemyBlackBoard bb, float moveSpeed) : base(bb)
        {
            _moveSpeed = moveSpeed;
            _detection = bb.EnemyDetection;
        }

        protected override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            _animator.SetFloat("MoveSpeed", _moveSpeed, 0.1f, Time.deltaTime);

            _stateMachine.MovementMultiplierX = _moveSpeed;
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

            // temporarily
            else if (!_detection.CanDetect && isRetreat)
            {
                _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.IDLE);
                base.SwitchState();
            }
        }
    }
}