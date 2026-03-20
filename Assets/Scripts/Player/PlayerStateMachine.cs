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
public class PlayerStateMachine : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {

    }


}

/// <summary>
/// abstract class for being the base class for State
/// </summary>
public abstract class State
{
    protected Animator _animator;
    protected CharacterController _characterController;

    public State(Animator animator, CharacterController characterController) 
    {
        _animator = animator;
        _characterController = characterController;
    }

    public virtual void OnEnter() { }

    public virtual void OnUpdate() { }

    public virtual void OnExit() { }
}

/// <summary>
/// all the concrete state that inherit from base state
/// which include Idle/ Walk/ Run/ Attack/ Grounded/ Airborne/ etc..
/// </summary>
public class Idle : State
{
    private float _idleSpeed = 0f;

    public Idle(Animator animator, CharacterController characterController): base(animator, characterController)
    {
        
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _animator.SetFloat("MoveSpeed", _idleSpeed, 0.1f, Time.deltaTime);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }
}

public class Walk : State
{
    public override void OnEnter()
    {
        base.OnEnter();
        _animator.SetFloat("MoveSpeed", _moveSpeed, 0.1f, Time.deltaTime);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }
}

public class Run : State
{
    public override void OnEnter()
    {
        base.OnEnter();
        _animator.SetFloat("MoveSpeed", _moveSpeed, 0.1f, Time.deltaTime);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }
}
