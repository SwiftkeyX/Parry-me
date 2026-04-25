/// <summary>
/// use with DebugMenu.cs and DebugEntry.cs
/// adjust the key as you need
/// </summary>
namespace DebugMenu
{
    public enum DebugEntryKEY
    {
        // movement debug key
        MovementDir,
        MovementMultiplierY,
        Movement,

        // jump debug state
        IsCCGrounded,               // debug if the CharacterController.isGrounded() is working properly
        PreviousYAndNewY,           // debug the calculate part of the jump.
        GravityForceApply,          // debug what kind of gravity force is apply on the entity. (airbornGravityForce, groundedGravityForce)

        // state debug key
        SwitchState,                // debug the current state of the entity.

        // collision debug key
        HitboxTiming,

        // ...
    }
}
