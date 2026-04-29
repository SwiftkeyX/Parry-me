using Entity;

namespace Enemy
{
    public abstract class EnemyBaseState : BaseState<EnemyStateMachine>
    {
        protected EnemyAttackAIForRealTimeCombat _attackAI;

        public EnemyBaseState(EnemyBlackBoard bb) : base(bb)
        {
            _attackAI = bb.AttackAI;
        }
    }
}