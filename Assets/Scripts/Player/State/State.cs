using UnityEngine;

/// <summary>
/// Reusable
/// 
/// abstract class for being the base class for each State
/// </summary>
public abstract class State
{
    protected Animator _animator;
    protected PlayerStateMachine _playerStateMachine;
    protected PlayerDebug _playerDebug;

    public State(StateMachineBlackBoard bb)
    {
        _animator = bb.Animator;
        _playerStateMachine = bb.PlayerStateMachine;
        _playerDebug = bb.PlayerDebug;
    }

    protected virtual void OnEnter() {  }

    public virtual void OnUpdate() { CheckSwitchState(); }

    protected virtual void OnExit() { }

    protected virtual void CheckSwitchState()
    {

    }

    protected void SwitchState()
    {
        this.OnExit();

        State newState = _playerStateMachine.GetCurrentState();

        if (_playerDebug.DebugState.IsDebugEnabled(DebugEntryKEY.SwitchState)) 
            Debug.Log("Switch State to: " + newState);

        newState.OnEnter();
    }
}