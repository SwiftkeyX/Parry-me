using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Approach Card", menuName = "Enemy/Cards/Approach")]
    public class ApproachCard : BaseCard
    {
        protected float damage;        // damage: damage of the hitbox (If it have one).
        public ApproachCard(AnimationClip clip = null, string name = null, float damage = 0f) : base(clip, name)
        {
            this.damage = damage;
        }
    }
}
