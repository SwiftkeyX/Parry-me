using UnityEngine;

/// <summary>
/// Reusable
/// 
/// wat? 
/// I want to create State for controlling the enemy's behavior
/// which should include Idle/ Chase/ Patrol/ Attack/ Retreat/ Grounded/ Airborne/ etc..
/// 
/// </summary>

/// <summary>
/// class for create State's instance and give them necessary variable/dependency
/// </summary>
namespace Enemy
{
    public class EnemyStateMachine : MonoBehaviour, StateMachine
    {
        // dependency
        public StateMachineBlackBoard _bb;
        private Gravity _gravity;

        // =========================================== necessary var ===========================================
        // state instance
        public enum STATE { IDLE, CHASE, OBSERVE, PATROL, RETREAT, ATTACK }
        private State _idle;
        private State _chase;
        private State _observe;
        private State _patrol;
        private State _retreat;
        private State _attack;
        private State _currentState;

        // movement var
        [Header("Movement State")]
        private Vector3 _movement;
        private Vector3 _movementMultiplier;
        private Vector3 _movementDirection;
        [SerializeField] private float _walkSpeed = 3f;
        [SerializeField] private float _runSpeed = 6f;

        // state var
        private bool _attackStrategy;
        private bool _isAggressive;
        private bool _isGuard;
        private bool _isTargetFound;

        // =========================================== setter and getter ===========================================
        public Vector3 Movement { get { return _movement; } }
        public float MovementMultiplierX { get { return _movementMultiplier.x; } set { _movementMultiplier.x = value; } }
        public float MovementMultiplierY { get { return _movementMultiplier.y; } set { _movementMultiplier.y = value; } }
        public Vector3 MovementDirection { get { return _movementDirection; } set { _movementDirection = value; } }
        // state variable 
        public float WalkSpeed { get { return _walkSpeed; } }
        public float RunSpeed { get { return _runSpeed; } }
        public bool AttackStrategy { get { return _attackStrategy; } }
        public bool IsAggressive { get { return _isAggressive; } }
        public bool IsGuard { get { return _isGuard; } }
        public bool IsTargetFound { get { return _isTargetFound; } }

        void Awake()
        {
            _bb = GetComponent<StateMachineBlackBoard>();
            _gravity = GetComponent<Gravity>();
        }

        void Start()
        {
            _idle = new Idle(_bb);
            _chase = new Chase(_bb);
            // _observe = new Observe();
            // _patrol = new Patrol();
            // _retreat = new Retreat();
            // _attack = new Attack(_bb);
            _currentState = _idle;

            // temporarily debug
            _attackStrategy = false;
            _isAggressive = true;
            _isGuard = false;
            _isTargetFound = true;
        }

        void Update()
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

        public State GetCurrentState()
        {
            return _currentState;
        }

        public void ChangeCurrentState(STATE s)
        {
            if (s == STATE.IDLE) _currentState = _idle;

            else if (s == STATE.CHASE) _currentState = _chase;

            else if (s == STATE.OBSERVE) _currentState = _observe;

            else if (s == STATE.PATROL) _currentState = _patrol;

            else if (s == STATE.RETREAT) _currentState = _retreat;

            else if (s == STATE.ATTACK) _currentState = _attack;
        }

    }

}
