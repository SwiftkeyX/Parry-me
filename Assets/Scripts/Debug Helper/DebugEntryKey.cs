/// <summary>
/// use with DebugMenu.cs and DebugEntry.cs
/// adjust the key as you need
/// </summary>
public enum DebugEntryKEY
{
    // movement debug key
    MovementDir,
    MovementMultiplierY,
    Movement,

    // jump debug state
    IsCCGrounded,
    PreviousYAndNewY,
    GravityForceApply,
    
    // state debug key
    SwitchState,
    
    // collision debug key
    HitboxTiming,

    // ...
}