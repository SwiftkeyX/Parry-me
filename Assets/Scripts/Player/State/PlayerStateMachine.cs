using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Reusable
/// yes, this initially a script for Platformer but It should require no change at all 
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
public class PlayerStateMachine : MonoBehaviour
{
    // dependency
    public StateMachineBlackBoard _bb;
    private Gravity _gravity;

    // =========================================== necessary var ===========================================
    // state instance
    public enum STATE { IDLE, WALK, RUN, JUMP, ATTACK }
    private State _idle;
    private State _walk;
    private State _run;
    private State _jump;
    private State _attack;
    private State _currentState;

    // input var
    private Vector3 _movement;
    private Vector3 _movementMultiplier;
    private Vector3 _movementDirection;
    private bool _runInput;
    private bool _jumpInput;
    private bool _rollInput;
    private bool _attackInput;

    [Header("Movement State")]
    [SerializeField] private float _walkSpeed = 3f;
    [SerializeField] private float _runSpeed = 6f;

    [Header("Jump State")]
    [SerializeField] private float _maxJumpHeight = 4f;
    [SerializeField] private float _maxJumpTime = 0.5f;
    private float _initialJumpVelocity;
    private bool _isGrounded;
    private bool _isFalling;

    // =========================================== setter and getter ===========================================
    // input 
    public Vector3 Movement { get { return _movement; } }
    public float MovementMultiplierX { get { return _movementMultiplier.x; } set { _movementMultiplier.x = value; } }
    public float MovementMultiplierY { get { return _movementMultiplier.y; } set { _movementMultiplier.y = value; } }
    public Vector3 MovementDirection { get { return _movementDirection; } set { _movementDirection = value; } }
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
    public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    public bool IsFalling { get { return _isFalling; } set { _isFalling = value; } }

    void Awake()
    {
        _bb = GetComponent<StateMachineBlackBoard>();
        _gravity = GetComponent<Gravity>();
    }

    void Start()
    {
        _idle = new Idle(_bb);
        _walk = new Walk(_bb, _walkSpeed);
        _run = new Run(_bb, _runSpeed);
        _jump = new Jump(_bb, _initialJumpVelocity);
        _attack = new Attack(_bb);
        _currentState = _idle;
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

        else if (s == STATE.WALK) _currentState = _walk;

        else if (s == STATE.RUN) _currentState = _run;

        else if (s == STATE.JUMP) _currentState = _jump;

        else if (s == STATE.ATTACK) _currentState = _attack;
    }

}