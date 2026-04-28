using System.Collections.Generic;

namespace Enemy
{
    [System.Serializable]
    public class Deck
    {
        public List<AttackCard> attackDeck;
        public List<RetreatCard> retreatDeck;
        public List<ApproachCard> approachDeck;
    }
}