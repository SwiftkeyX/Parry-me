using UnityEngine;

/// <summary>
/// What?
/// AttackData is a SO that contain all the data that this attack need.
/// Which include animation, damage, hitbox, etc...
/// 
/// Thought?
/// I could this script got scale in the near future.
/// Like instead of AttackData => it could be AnimationData instead and include other move beside attack (ex. buff animation, dead animation, etc...)
/// That mean it could need a lot of change.
/// </summary>
public class AttackData : ScriptableObject
{
    public AnimationClip clip;                      // animation of the attack
    public float dmg;                               // damage attack for calculation
    public float enableHitboxTime;                  // animator normalized time: to enable hitbox   
    public float disableHitboxTime;                 // animator normalized time: to disable hitbox
}
