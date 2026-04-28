using UnityEngine;

namespace Enemy
{
    [System.Serializable]
    public abstract class BaseCard : ScriptableObject
    {
        protected AnimationClip clip;                   // over write previous attack animation
        protected string cardName;                      // name: name of the card.
        public BaseCard(AnimationClip clip, string name)
        {
            this.clip = clip;
            cardName = name;
        }

        public void PlayCard()
        {
            PlayAnimation();
        }

        protected void PlayAnimation()
        {

        }
    }

}