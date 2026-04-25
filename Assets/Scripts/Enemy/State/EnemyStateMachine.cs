using Entity;
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
    public class EnemyStateMachine : StateMachine<EnemyStateMachine>
    {
        // =========================================== necessary var ===========================================
        // state instance
        public enum STATE { IDLE, CHASE, OBSERVE, PATROL, RETREAT, ATTACK }
        private State<EnemyStateMachine> _idle;
        private State<EnemyStateMachine> _chase;
        private State<EnemyStateMachine> _observe;
        private State<EnemyStateMachine> _patrol;
        private State<EnemyStateMachine> _retreat;
        private State<EnemyStateMachine> _attack;

        // ================================== movement setting ==================================
        [Header("Movement State")]
        [SerializeField] private float _walkSpeed = 3f;
        [SerializeField] private float _runSpeed = 6f;

        // ================================== state var ==================================
        private bool _attackStrategy;
        private bool _isAggressive;
        private bool _isGuard;
        private bool _isTargetFound;

        // =========================================== setter and getter ===========================================
        public float WalkSpeed { get { return _walkSpeed; } }
        public float RunSpeed { get { return _runSpeed; } }
        public bool AttackStrategy { get { return _attackStrategy; } }
        public bool IsAggressive { get { return _isAggressive; } }
        public bool IsGuard { get { return _isGuard; } }
        public bool IsTargetFound { get { return _isTargetFound; } }

        /// <summary>
        /// Initialize the dependency 
        /// </summary>
        protected override void Awake()
        {
            // ...

            base.Awake();
        }

        /// <summary>
        /// Initialize state instance
        /// </summary>
        protected override void Start()
        {
            _idle = new Idle(_bb);
            _chase = new Chase(_bb, _walkSpeed);
            // _observe = new Observe(_bb, _walkSpeed);
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

        /// <summary>
        /// Update current state, movement logic, apply gravity
        /// </summary>
        protected override void Update()
        {
            base.Update();
        }


        #region StateManagement
        public override State<EnemyStateMachine> GetCurrentState()
        {
            return base.GetCurrentState();
        }

        public void ChangeCurrentState(STATE s)
        {
            State<EnemyStateMachine> targetState = s switch
            {
                STATE.IDLE => _idle,
                STATE.CHASE => _chase,
                STATE.OBSERVE => _observe,
                STATE.RETREAT => _retreat,
                STATE.PATROL => _patrol,
                STATE.ATTACK => _attack,
                _ => _idle,
            };

            base.ChangeCurrentState(targetState);
        }
        #endregion

    }

}
