using UnityEngine;
using Player;
using DebugMenu;
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
    public abstract class BaseStateMachineBlackBoard<T> : MonoBehaviour where T : BaseStateMachine<T>
    {
        // =============================== general component var ===============================
        protected T _stateMachine;
        protected CharacterController _characterController;
        protected Animator _animator;
        protected BaseDebugList<T> _debugList;
        [SerializeField] protected CollisionCreater _CollisionCreater;

        // ============================== setter and getter ==============================
        // component getter
        public T StateMachine { get { return _stateMachine; } }
        public CharacterController CharacterController { get { return _characterController; } }
        public Animator Animator { get { return _animator; } }

        public CollisionCreater CollisionCreater { get { return _CollisionCreater; } }
        public BaseDebugList<T> DebugList { get { return _debugList; } }

        protected virtual void Awake()
        {
            _stateMachine = GetComponent<T>();
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _debugList = GetComponent<BaseDebugList<T>>();
        }
    }

}
