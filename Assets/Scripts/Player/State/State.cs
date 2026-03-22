using UnityEngine;

/// <summary>
/// abstract class for being the base class for each State
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

    protected virtual void OnEnter() {  }

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