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
    public class EnemyAttackAIForRealTimeCombat : MonoBehaviour
    {
        // =============================== dependency ===============================
        private Animator _animator;

        // =============================== necessary var ===============================
        public enum STRATEGY { ATTACK, APPROACH, RETREAT }
        [SerializeField] private List<EnemyAttackData> _attack;
        private List<EnemyAttackData> _availableAttack;
        private readonly System.Random _random = new();
        private EnemyAttackData _currentAttack;
        private AnimatorOverrideController _animOverride;

        // =============================== temp var ===============================
        private float _meleeRange = 3f;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _animOverride = new AnimatorOverrideController(_animator.runtimeAnimatorController);
            _animator.runtimeAnimatorController = _animOverride;
        }

        void Start()
        {
            RefillAttackList();
        }

        #region Main Logic
        public STRATEGY ShouldAttack()
        {
            // tempo
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            float distanceToPlayer = Vector3.Distance(this.transform.position, player.position);

            // 1. Choose next attack
            _currentAttack = ChooseNextAttack(distanceToPlayer);

            // 2. If attack not available, approach or retreat from target.
            if (_currentAttack == null)
            {
                // If no melee available, retreat from target to create more distanceToPlayer
                if (distanceToPlayer <= _meleeRange) return STRATEGY.RETREAT;

                // If no range available, approach target to reduce distanceToPlayer
                else if (distanceToPlayer > _meleeRange) return STRATEGY.APPROACH;

                // I want the case enemy stand still too. Like he was waiting for his cooldown.
                // ... 
            }

            // 3. If attack available, return and said "I will attack".
            return STRATEGY.ATTACK;
        }

        public void Attack()
        {
            // Remove the chosen attack from the _availableAttack list.
            _availableAttack.Remove(_currentAttack);

            // Play animation
            _animOverride["DefaultAttack"] = _currentAttack.clip;
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
            Debug.Log("[EnemyAttackAI] move: " + move + " maxRange: " + move.maxRange + " minRange: " + move.minRange + " distanceToPlayer: " + distanceToPlayer);
            return (move.maxRange >= distanceToPlayer && move.minRange <= distanceToPlayer);
        }
        #endregion

    }

}
