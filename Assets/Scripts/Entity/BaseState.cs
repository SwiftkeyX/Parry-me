using DebugMenu;
using UnityEngine;

/// <summary>
/// Reusable
/// 
/// abstract class for being the base class for each State
/// </summary>

namespace Entity
{
    public abstract class BaseState<T> where T : BaseStateMachine<T>
    {
        protected Animator _animator;
        protected T _stateMachine;
        protected BaseDebugList<T> _debugList;

        public BaseState(BaseStateMachineBlackBoard<T> bb)
        {
            _animator = bb.Animator;
            _stateMachine = bb.StateMachine;
            _debugList = bb.DebugList;
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

            BaseState<T> newState = _stateMachine.GetCurrentState();

            if (_debugList.DebugState.IsDebugEnabled(DebugEntryKEY.SwitchState))
                Debug.Log("Switch State to: " + newState);

            newState.OnEnter();
        }
    }
}