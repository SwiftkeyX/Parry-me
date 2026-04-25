using UnityEngine;
using Entity;

/// <summary>
/// Reusable
/// yes, this initially a script for Platformer genre game. But It should require no change at all for other genre.
/// if I want to reuse this in the other game's genre in the future. 
/// 
/// wat? 
/// I want to create State for controlling the player's behavior
/// which should include Idle/ Walk/ Run/ Attack/ Grounded/ Airborne/ etc..
/// 
/// </summary>

/// <summary>
/// class for create State's instance and give them necessary variable/dependency
/// </summary>

namespace Player
{
    public class PlayerStateMachine : BaseStateMachine<PlayerStateMachine>, IJumpable
    {
        // ================================== dependency =================================
        public PlayerBlackBoard _bb { get { return (PlayerBlackBoard)base._baseBB; } set { base._baseBB = value; } }
        protected PlayerGravity _gravity { get { return (PlayerGravity)base._baseGravity; } }

        // ================================== state var ==================================
        public enum STATE { IDLE, WALK, RUN, JUMP, ATTACK }
        private BaseState<PlayerStateMachine> _idle;
        private BaseState<PlayerStateMachine> _walk;
        private BaseState<PlayerStateMachine> _run;
        private BaseState<PlayerStateMachine> _jump;
        private BaseState<PlayerStateMachine> _attack;

        // ================================== input var ==================================
        private bool _runInput;
        private bool _jumpInput;
        private bool _rollInput;
        private bool _attackInput;

        // ================================== movement setting ==================================
        [Header("Movement State")]
        [SerializeField] private float _walkSpeed = 3f;
        [SerializeField] private float _runSpeed = 6f;

        [Header("Jump State")]
        [SerializeField] private float _maxJumpHeight = 4f;
        [SerializeField] private float _maxJumpTime = 0.5f;
        private float _initialJumpVelocity;

        // =========================================== setter and getter ===========================================
        // input 
        public bool RunInput { get { return _runInput; } set { _runInput = value; } }
        public bool JumpInput { get { return _jumpInput; } set { _jumpInput = value; } }
        public bool RollInput { get { return _rollInput; } set { _rollInput = value; } }
        public bool AttackInput { get { return _attackInput; } set { _attackInput = value; } }
        // state variable 
        public float WalkSpeed { get { return _walkSpeed; } }
        public float RunSpeed { get { return _runSpeed; } }
        public float MaxJumpHeight { get { return _maxJumpHeight; } }
        public float MaxJumpTime { get { return _maxJumpTime; } }
        public float InitialJumpVelocity { get { return _initialJumpVelocity; } set { _initialJumpVelocity = value; } }

        /// <summary>
        /// Initialize the dependency 
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            _bb = GetComponent<PlayerBlackBoard>();
        }

        /// <summary>
        /// Initialize state instance
        /// </summary>
        protected override void Start()
        {
            _idle = new Idle(_bb);
            _walk = new Walk(_bb, _walkSpeed);
            _run = new Run(_bb, _runSpeed);
            _jump = new Jump(_bb, _initialJumpVelocity);
            _attack = new Attack(_bb);
            _currentState = _idle;

        }

        /// <summary>
        /// Update current state, movement logic, apply gravity
        /// </summary>
        protected override void Update()
        {
            base.Update();
        }

        #region StateManagement
        public override BaseState<PlayerStateMachine> GetCurrentState()
        {
            return base.GetCurrentState();
        }

        public void ChangeCurrentState(STATE s)
        {
            BaseState<PlayerStateMachine> targetState = s switch
            {
                STATE.IDLE => _idle,
                STATE.WALK => _walk,
                STATE.RUN => _run,
                STATE.JUMP => _jump,
                STATE.ATTACK => _attack,
                _ => _idle,
            };

            base.ChangeCurrentState(targetState);
        }
        #endregion

    }
}
