using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// not reusable
/// 
/// Role?
/// glue to use DebugMenu with Player
/// </summary>
public class PlayerDebug : MonoBehaviour
{
    private PlayerStateMachine _playerStateMachine;

    [Header("Debug")]
    private DebugMenu _debugMovement;
    private DebugMenu _debugJump;
    private DebugMenu _debugState;
    public List<DebugEntry> DebugMovementInfo;
    public List<DebugEntry> DebugJumpInfo;
    public List<DebugEntry> DebugStateInfo;

    // =========================================== setter and getter ===========================================
    // debug helper
    public DebugMenu DebugMovement { get { return _debugMovement; } }
    public DebugMenu DebugJump { get { return _debugJump; } }
    public DebugMenu DebugState { get { return _debugState; } }

    void Awake()
    {
        // dependency

        // debug
        _debugMovement = new DebugMenu(DebugMovementInfo);
        _debugJump = new DebugMenu(DebugJumpInfo);
        _debugState = new DebugMenu(DebugStateInfo);
    }

    private void LateUpdate()
    {
        if (_debugMovement.IsDebugEnabled(DebugEntryKEY.MovementDir)) Debug.Log("MovementDirection: " + _playerStateMachine.MovementDirection);

        if (_debugMovement.IsDebugEnabled(DebugEntryKEY.MovementMultiplierY)) Debug.Log("MovementMultiY: " + _playerStateMachine.MovementMultiplierY);

        if (_debugMovement.IsDebugEnabled(DebugEntryKEY.Movement)) Debug.Log("movement: " + _playerStateMachine.Movement);

        if (_debugMovement.IsDebugEnabled(DebugEntryKEY.IsCCGrounded)) Debug.Log("CC is grounded: " + _playerStateMachine.IsGrounded);
    }

    // ======================== run in Editor time ============================
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

        if (DebugStateInfo != null && DebugStateInfo.Count == 0)
        {
            DebugStateInfo = new List<DebugEntry>
            {
                new DebugEntry(DebugEntryKEY.SwitchState),
            };
        }

    }
}