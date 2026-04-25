using Entity;

namespace Player
{
    public class PlayerBlackBoard : StateMachineBlackBoard<PlayerStateMachine>
    {
        // =============================== PlayerStateMachine component var ===============================
        private InputController _inputController;
        private PlayerAttackManager _playerAttackManager;
        private AttackBuffer _attackBuffer;

        // ============================== setter and getter ==============================
        // component getter
        public InputController InputController { get { return _inputController; } }
        public PlayerAttackManager PlayerAttackManager { get { return _playerAttackManager; } }
        public AttackBuffer AttackBuffer { get { return _attackBuffer; } }

        protected override void Awake()
        {
            base.Awake();

            _inputController = GetComponent<InputController>();
            _playerAttackManager = GetComponent<PlayerAttackManager>();
            _attackBuffer = GetComponent<AttackBuffer>();
        }

    }
}
