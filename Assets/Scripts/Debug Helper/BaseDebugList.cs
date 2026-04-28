using System.Collections.Generic;
using UnityEngine;
using Entity;

namespace DebugHelper
{
    /// <summary>
    /// Reusable
    /// 
    /// Role?
    /// ...
    /// 
    /// How to use?
    /// Add new "DebugMenu" variable in the DebugMenu section (or use existed one if it was available in the class). 
    /// Add "List<DebugEntry>" variable in the DebugEntry section (only if you add the new "DebugMenu").
    /// Add setter and getter for that new variable in the setter and getter section.
    /// Initialize those new variable in the InitializeDebugMenu(). 
    /// Add your own "List<DebugEntry>" section in OnValidate().
    /// 
    /// What if you want to add "DebugMenu" in the children class?
    /// Just do the same in "How to use?"
    /// </summary>
    /// <typeparam name="T">The type for BaseStateMachine (Could be "PlayerStateMachine" or "EnemyStateMachine")</typeparam>
    public class BaseDebugList<T> : MonoBehaviour where T : BaseStateMachine<T>
    {
        protected BaseStateMachine<T> _stateMachine;

        [Header("Debug")]
        // ============================== DebugMenu section ==============================
        private DebugMenu _debugMovement;
        private DebugMenu _debugJump;
        private DebugMenu _debugState;
        private DebugMenu _debugCollision;

        // ============================== DebugEntry section ==============================
        [SerializeField] private List<DebugEntry> DebugMovementInfo;
        [SerializeField] private List<DebugEntry> DebugJumpInfo;
        [SerializeField] private List<DebugEntry> DebugStateInfo;
        [SerializeField] private List<DebugEntry> DebugCollisionInfo;

        // =========================================== setter and getter ===========================================
        public DebugMenu DebugMovement { get { return _debugMovement; } }
        public DebugMenu DebugJump { get { return _debugJump; } }
        public DebugMenu DebugState { get { return _debugState; } }
        public DebugMenu DebugCollision { get { return _debugCollision; } }

        protected virtual void Awake()
        {
            // Dependency
            _stateMachine = GetComponent<BaseStateMachine<T>>();

            // Initialize debug menu
            InitializeDebugMenu();
        }

        private void InitializeDebugMenu()
        {
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

        // ======================== initialize debug list in Editor time ========================
        void OnValidate()
        {
            // DebugMovementInfo section
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

            // DebugJumpInfo section
            if (DebugJumpInfo != null && DebugJumpInfo.Count == 0)
            {
                DebugJumpInfo = new List<DebugEntry>
                {
                    new DebugEntry(DebugEntryKEY.PreviousYAndNewY),
                    new DebugEntry(DebugEntryKEY.GravityForceApply),
                };
            }

            // DebugCollisionInfo section
            if (DebugCollisionInfo != null && DebugCollisionInfo.Count == 0)
            {
                DebugCollisionInfo = new List<DebugEntry>
                {
                    new DebugEntry(DebugEntryKEY.HitboxTiming),
                };
            }

            // DebugStateInfo section
            if (DebugStateInfo != null && DebugStateInfo.Count == 0)
            {
                DebugStateInfo = new List<DebugEntry>
                {
                    new DebugEntry(DebugEntryKEY.SwitchState),
                };
            }

            // ...
        }

    }
}
