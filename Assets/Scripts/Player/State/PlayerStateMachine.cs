using UnityEngine;
using UnityEngine.InputSystem.Controls;

/// <summary>
/// Reusable?
/// yes, this initially a script for Platformer but It should require no change at all 
/// if I want to reuse this in the other game's genre in the future. 
/// 
/// wat? 
/// I want to create State for controlling the player's behavior
/// which should include Idle/ Walk/ Run/ Attack/ Grounded/ Airborne/ etc..
/// 
/// how to connect those state together?
/// At first, I used to connect those State using Hierarchical FSM
/// but when I finally finish that. I don't like it. so this time I want to try something new.
/// I want to connect this State using Behavior Tree (Specifically Behavior Graph from Behavior Package)
/// 
/// </summary>

/// <summary>
/// class for create State's instance and give them necessary variable/dependency
/// </summary>
[RequireComponent(typeof(StateMachineBlackBoard))]
public class PlayerStateMachine : MonoBehaviour
{
    // dependency
    private StateMachineBlackBoard _bb;

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
    public enum INPUT { MOVEMUL, MOVEDIR, JUMPI, ROLLI, ATTACKI }
    private Vector3 _movementMultiplier;
    private Vector3 _movementDirection;
    private bool _runInput;
    private bool _jumpInput;
    private bool _rollInput;
    private bool _attackInput;


    [Header("Adjust Player Stat")]
    public float WalkSpeed = 3f;
    public float RunSpeed = 6f;
    public float JumpSpeed = 1f;

    [Header("Debug")]
    public bool DebugMode = false;

    // =========================================== setter and getter ===========================================
    // input getter and setter
    public float MovementMultiplierX { get { return _movementMultiplier.x; } set { _movementMultiplier.x = value; } }
    public float MovementMultiplierY { get { return _movementMultiplier.y; } set { _movementMultiplier.y = value; } }
    public Vector3 MovementDirection { get { return _movementDirection; } set { _movementDirection = value; } }
    public bool RunInput { get { return _runInput; } set { _runInput = value; } }
    public bool JumpInput { get { return _jumpInput; } set { _jumpInput = value; } }
    public bool RollInput { get { return _rollInput; } set { _rollInput = value; } }
    public bool AttackInput { get { return _attackInput; } set { _attackInput = value; } }

    void Awake()
    {
        _bb = GetComponent<StateMachineBlackBoard>();
    }

    void Start()
    {
        _idle = new Idle(_bb);
        _walk = new Walk(_bb, WalkSpeed);
        _run = new Run(_bb, RunSpeed);
        _jump = new Jump(_bb, JumpSpeed);
        _attack = new Attack(_bb);
        _currentState = _idle;
    }

    void Update()
    {
        // update State
        _currentState.OnUpdate();

        // movement
        Vector3 completeMovement = new Vector3(
            _movementDirection.x * _movementMultiplier.x,
            _movementDirection.y * _movementMultiplier.y,
            0
        );
        // the only .Move() exist in entire system, should be here (prevent unnecessary .Move())
        _bb.CharacterController.Move(completeMovement * Time.deltaTime);

        ManageDebug();
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

    private void ManageDebug()
    {
        if (!DebugMode) return;

        Debug.Log("MovementDirection: " + MovementDirection);
    }
}

