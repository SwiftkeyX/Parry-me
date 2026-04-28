using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Attack Card", menuName = "Enemy/Cards/Attack")]
    public class AttackCard : BaseCard
    {

        public AttackCard(string name, EnemyAttackData attackSO) : base(attackSO.clip, name)
        {
            // this.damage = attackSO.dmg;
            // this.minRange = attackSO.minRange;
            // this.maxRange = attackSO.maxRange;
            // this.enableHitboxTime = attackSO.enableHitboxTime;
            // this.disableHitboxTime = attackSO.disableHitboxTime;
        }
    }
}
