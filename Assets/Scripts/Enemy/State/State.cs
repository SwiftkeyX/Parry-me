using UnityEngine;

namespace Enemy
{
    public abstract class State
    {
        protected Animator _animator;
        protected StateMachine _stateMachine;
        // protected PlayerDebug _playerDebug;

        public State(StateMachineBlackBoard bb)
        {
            _animator = bb.Animator;
            _stateMachine = bb.StateMachine;
            // _playerDebug = bb.PlayerDebug;
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

            State newState = _stateMachine.GetCurrentState();

            if (_playerDebug.DebugState.IsDebugEnabled(DebugEntryKEY.SwitchState))
                Debug.Log("Switch State to: " + newState);

            newState.OnEnter();
        }
    }
}