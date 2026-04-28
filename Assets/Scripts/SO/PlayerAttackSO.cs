using UnityEngine;

[CreateAssetMenu(menuName = "Attack/PlayerAttackSO")]
public class PlayerAttackData : AttackData
{
    public float chainAttack;                       // time: use in logic that allow player to chain the attack (a1 => a2 => a3) 
    public float attackTimer;                       // time: use in same logic as "chainAttack"
}
