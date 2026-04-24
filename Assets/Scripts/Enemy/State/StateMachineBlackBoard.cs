using UnityEngine;

namespace Enemy
{
    public class StateMachineBlackBoard : MonoBehaviour
    {
        // component var
        private StateMachine _stateMachine;
        private CharacterController _characterController;
        private Animator _animator;
        private InputController _inputController;
        private Gravity _gravity;
        [SerializeField] private CollisionCreater _CollisionCreater;

        // ============================== setter and getter ==============================
        // component getter
        public StateMachine StateMachine { get { return _stateMachine; } }
        public CharacterController CharacterController { get { return _characterController; } }
        public Animator Animator { get { return _animator; } }
        public InputController InputController { get { return _inputController; } }
        public Gravity Gravity { get { return _gravity; } }
        public CollisionCreater CollisionCreater { get { return _CollisionCreater; } }

        void Awake()
        {
            _stateMachine = GetComponent<StateMachine>();
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _inputController = GetComponent<InputController>();
            _gravity = GetComponent<Gravity>();
        }
    }
}

