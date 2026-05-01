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
    public class EnemyStateMachine : BaseStateMachine<EnemyStateMachine>, IMoveableForSideScroller
    {
        // ================================== dependency ==================================
        private EnemyBlackBoard _bb { get { return (EnemyBlackBoard)base._baseBB; } }
        private EnemyGravity _gravity { get { return (EnemyGravity)base._baseGravity; } }

        // ================================== state instance ==================================
        public enum STATE { IDLE, CHASE, OBSERVE, PATROL, RETREAT, ATTACK }
        private BaseState<EnemyStateMachine> _idle;
        private BaseState<EnemyStateMachine> _chase;
        private BaseState<EnemyStateMachine> _observe;
        private BaseState<EnemyStateMachine> _patrol;
        private BaseState<EnemyStateMachine> _retreat;
        private BaseState<EnemyStateMachine> _attack;

        // ================================== movement setting ==================================
        // maybe delete Movement setting later ?
        [Header("Movement State")]
        [SerializeField] private float _walkSpeed = 3f;
        [SerializeField] private float _runSpeed = 6f;

        // ================================== state var ==================================
        private bool _isGuard;

        // =========================================== setter and getter ===========================================
        public float WalkSpeed { get { return _walkSpeed; } }
        public float RunSpeed { get { return _runSpeed; } }
        public bool IsGuard { get { return _isGuard; } }

        /// <summary>
        /// Initialize the dependency 
        /// </summary>
        protected override void Awake()
        {
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
            _attack = new Attack(_bb);
            _currentState = _idle;

            // temporarily debug
            _isGuard = false;
        }

        /// <summary>
        /// Update current state, movement logic, apply gravity
        /// </summary>
        protected override void Update()
        {
            base.Update();
        }

        void LateUpdate()
        {
            ((IMoveableForSideScroller)this).KeepCharacterInZAxis(this.transform);
        }

        /// <summary>
        /// To get current state, change current state.
        /// </summary>
        /// <returns></returns>
        #region StateManagement
        public override BaseState<EnemyStateMachine> GetCurrentState()
        {
            return base.GetCurrentState();
        }

        public void ChangeCurrentState(STATE s)
        {
            BaseState<EnemyStateMachine> targetState = s switch
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
