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
    public enum STATE { IDLE, WALK, RUN, JUMP, ATTACK }

    // state instance
    private State _idle;
    private State _walk;
    private State _run;
    private State _jump;
    private State _attack;
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
        _jump = new Jump(_bb);
        _attack = new Attack(_bb);
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

        else if (s == STATE.JUMP) _currentState = _jump;
        
        else if (s == STATE.ATTACK) _currentState = _attack;


    }
}

