using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Reusable
    /// 
    /// Role?
    /// This class return the "Card" class, which card played determined the behavior of the enemy.
    /// If Deck return AttackCard => enemy enter attack state.
    /// If Deck return RetreatCard => enemy enter retreat state.
    /// If Deck return ApproachCard => enemy enter chase state.
    /// 
    /// What?
    /// "Deck" class is the combination of the "Card" class.
    /// 
    /// How?
    /// ...
    /// </summary>
    //     public class EnemyAttackAI : MonoBehaviour
    //     {
    //         [SerializeField] private Deck _masterDeck;
    //         private List<AttackCard> _availableAttackCard = new();
    //         private readonly System.Random _random = new();
    //         private float _meleeRange = 3f;

    //         void Start()
    //         {
    //             RefillDeck();
    //         }

    //         /// <summary>
    //         /// Draw next card, refill the deck when empty.
    //         /// </summary>
    //         /// <param name="distanceToPlayer"></param>
    //         /// <returns></returns>
    //         #region Main Logic
    //         public BaseCard GetNextValidAttackCard(float distanceToPlayer)
    //         {
    //             if (_availableAttackCard.Count == 0) RefillDeck();

    //             // 1. Find all attacks in the current deck that meet the distance requirement (pass minRange, maxRange).
    //             var validMoves = _availableAttackCard.FindAll(card => AttackCardDistanceCheck(card, distanceToPlayer));

    //             // 2. If no moves are valid (with current distanceToPlayer)
    //             // Reason: player was too close/far, enemy don't have melee/range attack card available.
    //             if (validMoves.Count == 0)
    //             {
    //                 // If no melee card available, retreat from target to create more distanceToPlayer
    //                 if (distanceToPlayer <= _meleeRange) return new RetreatCard();

    //                 // If no range card available, approach target to reduce distanceToPlayer
    //                 else if (distanceToPlayer > _meleeRange) return new ApproachCard();

    //                 // I want the case enemy stand still too. Like he was waiting for his cooldown.
    //                 // ... 
    //             }

    //             // 3. Pick from the VALID moves only
    //             int index = _random.Next(validMoves.Count);
    //             AttackCard selectedAttack = validMoves[index];

    //             // 4. Remove the specific instance from the main deck
    //             _availableAttackCard.Remove(selectedAttack);

    //             return selectedAttack;
    //         }

    //         private void RefillDeck()
    //         {
    //             _availableAttackCard = new List<AttackCard>(_masterDeck.attackDeck);
    //         }
    //         #endregion

    //         /// <summary>
    //         /// distance check, ...
    //         /// </summary>
    //         /// <param name="card"></param>
    //         /// <param name="distanceToPlayer"></param>
    //         /// <returns></returns>
    //         #region Other Logic (Should be move later)
    //         private bool AttackCardDistanceCheck(BaseCard card, float distanceToPlayer)
    //         {
    //             if (card is AttackCard attackCard)
    //                 return (attackCard.maxRange >= distanceToPlayer && attackCard.minRange <= distanceToPlayer);

    //             return false;
    //         }
    //         #endregion

    //     }
    public class EnemyAttackAI : MonoBehaviour
    {
        [SerializeField] private Deck _masterDeck;
        private List<AttackCard> _availableAttackCard = new();
        private readonly System.Random _random = new();
        private float _meleeRange = 3f;

        void Start()
        {
            RefillDeck();
        }

        /// <summary>
        /// Draw next card, refill the deck when empty.
        /// </summary>
        /// <param name="distanceToPlayer"></param>
        /// <returns></returns>
        #region Main Logic
        public BaseCard GetNextValidAttackCard(float distanceToPlayer)
        {
            if (_availableAttackCard.Count == 0) RefillDeck();

            // 1. Find all attacks in the current deck that meet the distance requirement (pass minRange, maxRange).
            var validMoves = _availableAttackCard.FindAll(card => AttackCardDistanceCheck(card, distanceToPlayer));

            // 2. If no moves are valid (with current distanceToPlayer)
            // Reason: player was too close/far, enemy don't have melee/range attack card available.
            if (validMoves.Count == 0)
            {
                // If no melee card available, retreat from target to create more distanceToPlayer
                if (distanceToPlayer <= _meleeRange) return new RetreatCard();

                // If no range card available, approach target to reduce distanceToPlayer
                else if (distanceToPlayer > _meleeRange) return new ApproachCard();

                // I want the case enemy stand still too. Like he was waiting for his cooldown.
                // ... 
            }

            // 3. Pick from the VALID moves only
            int index = _random.Next(validMoves.Count);
            AttackCard selectedAttack = validMoves[index];

            // 4. Remove the specific instance from the main deck
            _availableAttackCard.Remove(selectedAttack);

            return selectedAttack;
        }

        private void RefillDeck()
        {
            _availableAttackCard = new List<AttackCard>(_masterDeck.attackDeck);
        }
        #endregion

        /// <summary>
        /// distance check, ...
        /// </summary>
        /// <param name="card"></param>
        /// <param name="distanceToPlayer"></param>
        /// <returns></returns>
        #region Other Logic (Should be move later)
        private bool AttackCardDistanceCheck(BaseCard card, float distanceToPlayer)
        {
            if (card is AttackCard attackCard)
                return (attackCard.maxRange >= distanceToPlayer && attackCard.minRange <= distanceToPlayer);

            return false;
        }
        #endregion

    }

}
