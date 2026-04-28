using UnityEngine;
using Entity;
/// <summary>
/// all the concrete state that inherit from base state
/// which include Idle/ Walk/ Run/ Attack/ Grounded/ Airborne/ etc..
/// </summary>

namespace Enemy
{
    public class Chase : BaseState<EnemyStateMachine>
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
            // if (_detection.CanDetect && _stateMachine.AttackStrategy)
            // {
            //     _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.ATTACK);
            //     base.SwitchState();
            // }

            if (_detection.CanDetect && _stateMachine.AttackStrategy)
            {
                _stateMachine.ChangeCurrentState(EnemyStateMachine.STATE.ATTACK);
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