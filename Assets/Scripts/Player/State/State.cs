using UnityEngine;

/// <summary>
/// Reusable
/// 
/// abstract class for being the base class for each State
/// </summary>
public abstract class State
{
    protected Animator _animator;
    protected CharacterController _characterController;
    protected PlayerStateMachine _playerStateMachine;

    public State(StateMachineBlackBoard bb)
    {
        _animator = bb.Animator;
        _characterController = bb.CharacterController;
        _playerStateMachine = bb.PlayerStateMachine;
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

        Debug.Log("Switch State to: " + newState);

        newState.OnEnter();
    }
}