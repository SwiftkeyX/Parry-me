using UnityEngine;
using Entity;
using Player;
/// <summary>
/// Not Reusable
/// 
/// Due to each State (idle/ walk/ run/ etc...) maybe need a lot more dependency from PlayerStateMachine in the future
/// Instead of me have to manually add new Parameter everytime
/// I will create BlackBoard that will keep all the dependency need in the future. and send this object to each State
/// 
/// Now StateMachineBB will be use with general StateMachine base class.
/// StateMachine's children include (PlayerStateMachine, EnemyStateMachine).
/// 
/// this script role is:
/// 1.To be a glue between StateMachine and Concrete State. 
/// 2.Keep dependency for each State => So we can send the dependency to each state easily.
/// 3.this also act as a temporaliry glue between script
///     3.1 glue between "InputController" and "PlatformerInputProcessor" (now, not anymore)
///     3.2 glue betwen "Attack State" and "PlayerAttackManager" (now, not anymore)
///     3.3 glue between "PlayerStateMachine" and "Gravity" (now, not anymore)
///     3.4 glue between "PlayerStateMachine" and "Concrete State"
/// </summary>
/// 
namespace Entity
{
    public class StateMachineBlackBoard<T> : MonoBehaviour where T : StateMachine<T>
    {
        // =============================== general component var ===============================
        private T _stateMachine;
        private CharacterController _characterController;
        private Animator _animator;
        private Gravity<T> _gravity;
        private DebugList<T> _debugList;
        [SerializeField] private CollisionCreater _CollisionCreater;

        // =============================== PlayerStateMachine component var ===============================
        private InputController _inputController;
        private PlayerAttackManager _playerAttackManager;
        private AttackBuffer _attackBuffer;

        // ============================== setter and getter ==============================
        // component getter
        public T StateMachine { get { return _stateMachine; } }
        public CharacterController CharacterController { get { return _characterController; } }
        public Animator Animator { get { return _animator; } }
        public InputController InputController { get { return _inputController; } }
        public PlayerAttackManager PlayerAttackManager { get { return _playerAttackManager; } }
        public AttackBuffer AttackBuffer { get { return _attackBuffer; } }
        public Gravity<T> Gravity { get { return _gravity; } }
        public CollisionCreater CollisionCreater { get { return _CollisionCreater; } }
        public DebugList<T> DebugList { get { return _debugList; } }

        void Awake()
        {
            _stateMachine = GetComponent<T>();
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _gravity = GetComponent<Gravity<T>>();
            _debugList = GetComponent<DebugList<T>>();

            if (_stateMachine is PlayerStateMachine a)
            {
                _inputController = GetComponent<InputController>();
                _playerAttackManager = GetComponent<PlayerAttackManager>();
                _attackBuffer = GetComponent<AttackBuffer>();
            }
        }
    }

}
