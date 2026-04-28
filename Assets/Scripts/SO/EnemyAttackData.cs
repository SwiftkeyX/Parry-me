using UnityEngine;

[CreateAssetMenu(menuName = "Attack/EnemyAttackSO")]
public class EnemyAttackData : AttackData
{
    public float minRange;      // minRange: if distance to target is less than minRange, don't use this attack.
    public float maxRange;      // maxRange: if distance to target is more than maxRange, don't use this attack.
}

