using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Retreat Card", menuName = "Enemy/Cards/Retreat")]
    public class RetreatCard : BaseCard
    {
        protected float damage;        // damage: damage of the hitbox (If it have one).
        public RetreatCard(AnimationClip clip = null, string name = null, float damage = 0f) : base(clip, name)
        {
            this.damage = damage;
        }
    }
}
