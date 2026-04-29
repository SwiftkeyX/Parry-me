
using Entity;

namespace Enemy
{
    public class EnemyBlackBoard : BaseStateMachineBlackBoard<EnemyStateMachine>
    {
        // ========================== Dependency ==========================
        private EnemyDetection _enemyDetection;
        private EnemyAttackAIForRealTimeCombat _attackAI;

        // ========================== setter and getter ==========================
        public EnemyDetection EnemyDetection { get { return _enemyDetection; } }
        public EnemyAttackAIForRealTimeCombat AttackAI { get { return _attackAI; } }

        protected override void Awake()
        {
            base.Awake();

            _enemyDetection = GetComponent<EnemyDetection>();
            _attackAI = GetComponent<EnemyAttackAIForRealTimeCombat>();
        }
    }
}
