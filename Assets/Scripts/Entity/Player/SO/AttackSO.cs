using UnityEngine;

[CreateAssetMenu(menuName = "Attack/AttackSO")]
public class AttackSO : ScriptableObject
{
    // over write previous attack animation
    public AnimatorOverrideController animOV;
    
    // damage attack for calculation
    public float dmg;

    // time: use in logic that allow player to chain the attack (a1 => a2 => a3) 
    public float chainAttack;
    
    // time: use in same logic as "chainAttack"
    public float attackTimer;

    // animator normalized time: to enable hitbox
    public float enableHitboxTime;

    // animator normalized time: to disable hitbox
    public float disableHitboxTime;
}