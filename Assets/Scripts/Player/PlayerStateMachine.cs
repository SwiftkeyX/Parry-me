using UnityEngine;

/// <summary>
/// wat? 
/// I want to create State for controlling the player's behavior
/// which should include Idle/ Walk/ Run/ Attack/ Grounded/ Airborne/ etc..
/// 
/// how to connect those state together?
/// At first, I used to connect those State using Hierarchical FSM
/// but when I finally finish that. I don't like it. so this time I want to try something new.
/// I want to connect this State using Behavior Tree (Specifically Behavior Graph from Behavior Package)
/// 
/// why Behavior Graph?
/// because the UI look great and it's seem to be really flexible. I guess?
/// also I am planning to use Behavior Graph as a Glue System => to connect Reusable Module
/// let's see how it does
/// </summary>

/// <summary>
/// class for create State's instance and give them necessary variable/dependency
/// </summary>
[RequireComponent(typeof(StateMachineBlackBoard))]
public class PlayerStateMachine : MonoBehaviour
{
    private StateMachineBlackBoard _bb;
    public enum STATE { IDLE, WALK, RUN }

    // state instance
    private State _idle;
    private State _walk;
    private State _run;
    private State _currentState;

    void Awake()
    {
        _bb = GetComponent<StateMachineBlackBoard>();
    }

    void Start()
    {
        _idle = new Idle(_bb);
        _walk = new Walk(_bb);
        _run = new Run(_bb);
        _currentState = _idle;
    }

    void Update()
    {
        // update State
        _currentState.OnUpdate();
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
    }
}

/// <summary>
/// abstract class for being the base class for State
/// </summary>
public abstract class State
{
    protected StateMachineBlackBoard _bb;
    protected Animator _animator;
    protected CharacterController _characterController;

    public State(StateMachineBlackBoard bb)
    {
        _bb = bb;
        _animator = bb.Animator;
        _characterController = bb.CharacterController;
    }

    protected virtual void OnEnter() { Debug.Log("Enter to: " + this); }

    public virtual void OnUpdate() { CheckSwitchState(); }

    protected virtual void OnExit() { }

    protected virtual void CheckSwitchState()
    {
        this.OnExit();

        State newState = _bb.PlayerStateMachine.GetCurrentState();

        Debug.Log("Switch State to: " + newState);

        newState.OnEnter();
    }
}

/// <summary>
/// all the concrete state that inherit from base state
/// which include Idle/ Walk/ Run/ Attack/ Grounded/ Airborne/ etc..
/// </summary>
public class Idle : State
{
    private float _idleSpeed;

    public Idle(StateMachineBlackBoard bb, float moveSpeed = 0f) : base(bb)
    {
        _idleSpeed = moveSpeed;
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        _animator.SetFloat("MoveSpeed", _idleSpeed, 0.1f, Time.deltaTime);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }

    protected override void CheckSwitchState()
    {
        Debug.Log("MoveDirection from Idle: " + _bb.InputProcessor.MoveDirection);

        if (_bb.InputProcessor.MoveDirection.sqrMagnitude > 0f)
        {
            _bb.PlayerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.WALK);
        }

        base.CheckSwitchState();
    }
}

public class Walk : State
{
    private float _walkSpeed;
    private float _rotationSpeed;

    public Walk(StateMachineBlackBoard bb, float moveSpeed = 3f, float rotationSpeed = 20f) : base(bb)
    {
        _walkSpeed = moveSpeed;
        _rotationSpeed = rotationSpeed;
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        _animator.SetFloat("MoveSpeed", _walkSpeed, 0.1f, Time.deltaTime);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        _bb.CharacterController.Move(_bb.InputProcessor.MoveDirection * _walkSpeed * Time.deltaTime);

        RotateTowardMoveDirection();
    }

    protected override void CheckSwitchState()
    {
        if (_bb.InputProcessor.MoveDirection.sqrMagnitude == 0f)
        {
            _bb.PlayerStateMachine.ChangeCurrentState(PlayerStateMachine.STATE.IDLE);
        }

        base.CheckSwitchState();
    }

    private void RotateTowardMoveDirection()
    {
        // shutup Unity's console log
        if (_bb.InputProcessor.MoveDirection.sqrMagnitude < 0.001f) return;

        // Get target rotation
        Quaternion targetRotation = Quaternion.LookRotation(_bb.InputProcessor.MoveDirection);

        // Smoothly rotate
        _bb.transform.rotation = Quaternion.Slerp(
            _bb.transform.rotation,
            targetRotation,
            _rotationSpeed * Time.deltaTime
        );
    }
}

public class Run : State
{
    private float _runSpeed;

    public Run(StateMachineBlackBoard bb, float moveSpeed = 6f) : base(bb)
    {
        _runSpeed = moveSpeed;
    }


    protected override void OnEnter()
    {
        base.OnEnter();
        _animator.SetFloat("MoveSpeed", _runSpeed, 0.1f, Time.deltaTime);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }

    protected override void CheckSwitchState()
    {

        base.CheckSwitchState();
    }
}
