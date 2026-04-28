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
    public class EnemyAttackAI : MonoBehaviour
    {
        // =============================== dependency ===============================
        private Animator _animator;

        // =============================== necessary var ===============================
        public enum STRATEGY {ATTACK, APPROACH, RETREAT}
        [SerializeField] private readonly List<EnemyAttackData> _attack;
        private List<EnemyAttackData> _availableAttack;
        private readonly System.Random _random = new();

        // =============================== temp var ===============================
        private float _meleeRange = 3f;

        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void Start()
        {
            RefillAttackList();
        }

        #region Main Logic
        public STRATEGY Attack()
        {
            // tempo
            float distanceToPlayer = -1f;

            // 1. Choose next attack
            EnemyAttackData currentAttack = ChooseNextAttack(distanceToPlayer);

            // 2. If attack not available, approach or retreat from target.
            if (currentAttack == null)
            {
                // If no melee available, retreat from target to create more distanceToPlayer
                if (distanceToPlayer <= _meleeRange) return STRATEGY.RETREAT;

                // If no range available, approach target to reduce distanceToPlayer
                else if (distanceToPlayer > _meleeRange) return STRATEGY.APPROACH;

                // I want the case enemy stand still too. Like he was waiting for his cooldown.
                // ... 
            }

            // 3. If attack available, play the attack.
            else
            {
                AnimatorOverrideController anim = new AnimatorOverrideController(_animator.runtimeAnimatorController);
                _animator.runtimeAnimatorController = anim;
                anim["DefaultAttack"] = currentAttack.clip;
                _animator.CrossFade("Attack", 0.1f, 0, 0f);
            }

            return STRATEGY.ATTACK;
        }

        private EnemyAttackData ChooseNextAttack(float distanceToPlayer)
        {
            if (_availableAttack.Count == 0) RefillAttackList();

            // 1. Find all attacks in the current list that meet the distance requirement (pass minRange, maxRange).
            var validMoves = _availableAttack.FindAll(move => AttackDistanceCheck(move, distanceToPlayer));

            // 2. If no moves are valid (with current distanceToPlayer), return and said "attack not available".
            // Reason: player was too close/far, enemy don't have melee/range attack available.
            if (validMoves.Count == 0) return null;

            // 3. Random the attack from the valid moves.
            int index = _random.Next(validMoves.Count);
            EnemyAttackData selectedAttack = validMoves[index];

            // 4. Remove the chosen attack from the _availableAttack list.
            _availableAttack.Remove(selectedAttack);

            return selectedAttack;
        }

        private void RefillAttackList()
        {
            _availableAttack = new List<EnemyAttackData>(_attack);
        }
        #endregion

        /// <summary>
        /// distance check, ...
        /// </summary>
        /// <param name="card"></param>
        /// <param name="distanceToPlayer"></param>
        /// <returns></returns>
        #region Other Logic (Should be move later)
        private bool AttackDistanceCheck(EnemyAttackData move, float distanceToPlayer)
        {
            return (move.maxRange >= distanceToPlayer && move.minRange <= distanceToPlayer);
        }
        #endregion

    }

}
