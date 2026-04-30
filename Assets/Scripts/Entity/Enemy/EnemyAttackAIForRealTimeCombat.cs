using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Reusable
    /// 
    /// Role?
    /// ...
    /// 
    /// 
    /// How?
    /// ...
    /// </summary>
    public class EnemyAttackAIForRealTimeCombat : MonoBehaviour
    {
        // =============================== dependency ===============================
        private Animator _animator;

        // =============================== necessary var ===============================
        public enum STRATEGY { ATTACK, APPROACH, RETREAT, WAIT }
        [SerializeField] private List<EnemyAttackData> _attack;
        private List<EnemyAttackData> _availableAttack;
        private readonly System.Random _random = new();
        private STRATEGY _currentStrategy;
        private EnemyAttackData _currentAttack;
        private AnimatorOverrideController _animOverride;
        private float _distanceToPlayer;

        // =============================== debug var ===============================
        [Header("DebugMode")]
        [SerializeField] private bool _debugMode;
        [SerializeField] private TextMeshProUGUI _debugTextUI;

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
            TurnOnDebugUI();
        }

        void LateUpdate()
        {
            UpdateDebugTextVisuallyInTheScene();
        }

        #region Main Logic
        /// <summary>
        /// AI think if I should attack
        /// </summary>
        /// <returns>STRATEGY</returns> for the statemachine. Statemachine use STRATEGY for state's transition
        public STRATEGY ShouldAttack()
        {
            if (CanISkipCurrentAnimation() == false) return STRATEGY.WAIT;

            // tempo
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            _distanceToPlayer = Vector3.Distance(this.transform.position, player.position);

            // 1. Choose next attack
            _currentAttack = ChooseNextAttack(_distanceToPlayer);

            // 2. If attack not available, approach or retreat from target.
            if (_currentAttack == null)
            {
                // If no melee available, retreat from target to create more distanceToPlayer
                if (_distanceToPlayer <= _meleeRange) _currentStrategy = STRATEGY.RETREAT;

                // If no range available, approach target to reduce distanceToPlayer
                else if (_distanceToPlayer > _meleeRange) _currentStrategy = STRATEGY.APPROACH;

                // I want the case enemy stand still too. Like he was waiting for his cooldown.
                // ... 
            }

            // 3. If attack available, return and said "I will attack".
            _currentStrategy = STRATEGY.ATTACK;

            // 4. Return
            return _currentStrategy;
        }

        /// <summary>
        /// Override the animator (so later we can play correct animation), Delete the selected attack from the available attack list
        /// </summary>
        public void Attack()
        {
            // Remove the chosen attack from the _availableAttack list.
            _availableAttack.Remove(_currentAttack);

            // Play animation
            _animOverride["DefaultAttack"] = _currentAttack.clip;
        }

        /// <summary>
        /// Choose next attack based on the available attack list and distanceToPlayer.
        /// </summary>
        /// <param name="distanceToPlayer"></param>
        /// <returns></returns>
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

        /// <summary>
        /// After enemy attack for a while, the available attack list should be delete each time to empty. Refill the list.
        /// </summary>
        private void RefillAttackList()
        {
            _availableAttack = new List<EnemyAttackData>(_attack);
        }

        private bool CanISkipCurrentAnimation()
        {
            AnimatorStateInfo _ = _animator.GetCurrentAnimatorStateInfo(0);
            bool isInAttack = _.IsTag("Attack");
            bool isNotInTransition = !_animator.IsInTransition(0);
            bool isAttackFinish = _.normalizedTime >= 1f;

            // 1. not in attack state, can skip animation => return true.
            if (!isInAttack && isNotInTransition) return true;

            // 2. in attack state, can't skip attack animation => wait until attack is finished => return true.
            if (isInAttack && isAttackFinish && isNotInTransition) return true;

            return false;
        }
        #endregion

        /// <summary>
        /// distance check, ...
        /// </summary>
        /// <param name="card"></param>
        /// <param name="distanceToPlayer"></param>
        /// <returns></returns>
        #region Other Logic (Should be move to other script later)
        private bool AttackDistanceCheck(EnemyAttackData move, float distanceToPlayer)
        {
            return (move.maxRange >= distanceToPlayer && move.minRange <= distanceToPlayer);
        }
        #endregion

        /// <summary>
        /// Debug current strategy, current attack visually in the scene.
        /// </summary>
        #region Debug
        private void UpdateDebugTextVisuallyInTheScene()
        {
            if (!_debugMode || _debugTextUI == null) return;

            string attackName = _currentAttack != null ? _currentAttack.name : "None";

            _debugTextUI.text = $"<color=yellow>Strategy:</color> {_currentStrategy}\n" +
                               $"<color=cyan>Distance To Player:</color> {_distanceToPlayer:F2}\n" +
                               $"<color=red>Queued Attack:</color> {attackName}\n" +
                               $"<color=white>Pool Count:</color> {_availableAttack.Count}";
        }

        /// <summary>
        /// turn on/off TMP (text mesh pro) depend on script's debug mode
        /// </summary>
        private void TurnOnDebugUI()
        {
            _debugTextUI.enabled = _debugMode;
        }


        #endregion
    }

}
