
using Entity;

namespace Enemy
{
    public class EnemyBlackBoard : BaseStateMachineBlackBoard<EnemyStateMachine>
    {
        // ========================== Dependency ==========================
        private EnemyDetection _enemyDetection;
        private EnemyAttackAI _deck;

        // ========================== setter and getter ==========================
        public EnemyDetection EnemyDetection { get { return _enemyDetection; } }
        public EnemyAttackAI Deck { get { return _deck; } }

        protected override void Awake()
        {
            base.Awake();

            _enemyDetection = GetComponent<EnemyDetection>();
            _deck = GetComponent<EnemyAttackAI>();
        }
    }
}
