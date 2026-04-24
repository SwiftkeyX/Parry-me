using UnityEngine;

namespace Entity
{
    public abstract class StateMachine<T> : MonoBehaviour where T : StateMachine<T>
    {
        // ============================== dependency ==============================
        public StateMachineBlackBoard<T> _bb;
        protected Gravity _gravity;

        // ============================== state var ==============================
        protected State<T> _currentState;

        // ============================== movement var ==============================
        protected Vector3 _movement;
        protected Vector3 _movementMultiplier;
        protected Vector3 _movementDirection;

        // ============================== other var ==============================
        protected bool _isGrounded;
        protected bool _isFalling;

        // ============================== setter and getter ==============================
        public Vector3 Movement { get { return _movement; } }
        public float MovementMultiplierX { get { return _movementMultiplier.x; } set { _movementMultiplier.x = value; } }
        public float MovementMultiplierY { get { return _movementMultiplier.y; } set { _movementMultiplier.y = value; } }
        public Vector3 MovementDirection { get { return _movementDirection; } set { _movementDirection = value; } }
        public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
        public bool IsFalling { get { return _isFalling; } set { _isFalling = value; } }

        protected virtual void Awake()
        {
            _bb = GetComponent<StateMachineBlackBoard<T>>();
            _gravity = GetComponent<Gravity>();
        }

        protected virtual void Start() { }

        protected virtual void Update()
        {
            // update State
            _currentState.OnUpdate();

            // movement
            _movement = new Vector3(
                _movementDirection.x * _movementMultiplier.x,
                _movementDirection.y * _movementMultiplier.y,
                0
            );

            // the only .Move() exist in entire system, should be here (prevent unnecessary .Move())
            _bb.CharacterController.Move(_movement * Time.deltaTime);

            // apply gravity after .Move()
            _gravity.ApplyGravity();
        }

        public virtual State<T> GetCurrentState()
        {
            return _currentState;
        }
        public virtual void ChangeCurrentState(State<T> newState)
        {
            _currentState = newState;
        }
    }
}
