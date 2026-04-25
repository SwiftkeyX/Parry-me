using System.Collections.Generic;
using UnityEngine;
using Entity;

namespace DebugMenu
{
    public class BaseDebugList<T> : MonoBehaviour where T : BaseStateMachine<T>
    {
        protected BaseStateMachine<T> _stateMachine;

        [Header("Debug")]
        private DebugMenu _debugMovement;
        private DebugMenu _debugJump;
        private DebugMenu _debugState;
        private DebugMenu _debugCollision;
        public List<DebugEntry> DebugMovementInfo;
        public List<DebugEntry> DebugJumpInfo;
        public List<DebugEntry> DebugStateInfo;
        public List<DebugEntry> DebugCollisionInfo;

        // =========================================== setter and getter ===========================================
        // debug helper
        public DebugMenu DebugMovement { get { return _debugMovement; } }
        public DebugMenu DebugJump { get { return _debugJump; } }
        public DebugMenu DebugState { get { return _debugState; } }
        public DebugMenu DebugCollision { get { return _debugCollision; } }

        protected virtual void Awake()
        {
            // Dependency
            _stateMachine = GetComponent<BaseStateMachine<T>>();

            // Initialize debug menu
            _debugMovement = new DebugMenu(DebugMovementInfo);
            _debugJump = new DebugMenu(DebugJumpInfo);
            _debugState = new DebugMenu(DebugStateInfo);
            _debugCollision = new DebugMenu(DebugCollisionInfo);
        }

        protected void LateUpdate()
        {
            if (_debugMovement.IsDebugEnabled(DebugEntryKEY.MovementDir)) Debug.Log("MovementDirection: " + _stateMachine.MovementDirection);

            if (_debugMovement.IsDebugEnabled(DebugEntryKEY.MovementMultiplierY)) Debug.Log("MovementMultiY: " + _stateMachine.MovementMultiplierY);

            if (_debugMovement.IsDebugEnabled(DebugEntryKEY.Movement)) Debug.Log("movement: " + _stateMachine.Movement);

            if (_debugMovement.IsDebugEnabled(DebugEntryKEY.IsCCGrounded)) Debug.Log("CC is grounded: " + _stateMachine.IsGrounded);
        }

        // ======================== run in Editor time ============================
        // initial debug list
        void OnValidate()
        {
            if (DebugMovementInfo != null && DebugMovementInfo.Count == 0)
            {
                DebugMovementInfo = new List<DebugEntry>
            {
                new DebugEntry(DebugEntryKEY.MovementDir),
                new DebugEntry(DebugEntryKEY.MovementMultiplierY),
                new DebugEntry(DebugEntryKEY.Movement),
                new DebugEntry(DebugEntryKEY.IsCCGrounded),
            };
            }

            if (DebugJumpInfo != null && DebugJumpInfo.Count == 0)
            {
                DebugJumpInfo = new List<DebugEntry>
            {
                new DebugEntry(DebugEntryKEY.PreviousYAndNewY),
                new DebugEntry(DebugEntryKEY.GravityForceApply),
            };
            }

            if (DebugCollisionInfo != null && DebugCollisionInfo.Count == 0)
            {
                DebugCollisionInfo = new List<DebugEntry>
            {
                new DebugEntry(DebugEntryKEY.HitboxTiming),
            };
            }

            if (DebugStateInfo != null && DebugStateInfo.Count == 0)
            {
                DebugStateInfo = new List<DebugEntry>
            {
                new DebugEntry(DebugEntryKEY.SwitchState),
            };
            }

        }

    }
}
