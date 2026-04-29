using Entity;

namespace Enemy
{
    public class EnemyBaseState : BaseState<EnemyStateMachine>
    {
        protected EnemyAttackAI _attackAI;

        public EnemyBaseState(EnemyBlackBoard bb) : base(bb)
        {
            _attackAI = bb.AttackAI;    
        }
    }
}