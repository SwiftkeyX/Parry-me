using UnityEngine;

/// <summary>
/// Reusable
/// 
/// abstract class for being the base class for each State
/// </summary>

namespace Entity
{
    public abstract class State<T> where T : StateMachine<T>
    {
        protected Animator _animator;
        protected T _stateMachine;
        protected PlayerDebug _playerDebug;

        public State(StateMachineBlackBoard<T> bb)
        {
            _animator = bb.Animator;
            _stateMachine = bb.StateMachine;
            _playerDebug = bb.PlayerDebug;
        }

        protected virtual void OnEnter() { }

        public virtual void OnUpdate() { CheckSwitchState(); }

        protected virtual void OnExit() { }

        protected virtual void CheckSwitchState()
        {

        }

        protected void SwitchState()
        {
            this.OnExit();

            State<T> newState = _stateMachine.GetCurrentState();

            if (_playerDebug.DebugState.IsDebugEnabled(DebugEntryKEY.SwitchState))
                Debug.Log("Switch State to: " + newState);

            newState.OnEnter();
        }
    }
}