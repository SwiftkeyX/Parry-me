using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Not Reusable 
/// 
/// Role?
/// 1.to keep AttackSO
/// 2.to be glue for attack system (include PlayerAttackManager, HitboxController, AttackBuffer)
/// 
/// </summary>
/// 
namespace Player
{
    public class PlayerAttackManager : MonoBehaviour
    {
        [SerializeField] private List<PlayerAttackData> _attackSO;
        private PlayerAttackBuffer _attackBuffer;
        private HitboxController _hitboxController;

        // var
        int currentAttack = -1;

        // setter and getter
        public PlayerAttackBuffer AttackBuffer { get { return _attackBuffer; } }

        void Awake()
        {
            _attackBuffer = GetComponent<PlayerAttackBuffer>();
            _hitboxController = GetComponent<HitboxController>();
        }

        void Start()
        {
            // injection
            for (int i = 0; i < _attackSO.Count; i++)
            {
                AttackBufferData _ = new AttackBufferData(_attackSO[i].clip, _attackSO[i].chainAttack, _attackSO[i].attackTimer);
                _attackBuffer.Data.Add(_);
            }

            // injection 2
            for (int i = 0; i < _attackSO.Count; i++)
            {
                HitboxData _ = new HitboxData(_attackSO[i].enableHitboxTime, _attackSO[i].disableHitboxTime);
                _hitboxController.Data.Add(_);
            }
        }

        public void Attack(bool attackInput)
        {
            bool chainAttackSuccess = false;

            // Try buffer the attack
            // I see even I got confused by this function => need more refactor 
            if (attackInput) chainAttackSuccess = _attackBuffer.Attack();

            // If buffer the attack success, reset the hitbox
            if (chainAttackSuccess)
            {
                currentAttack = _attackBuffer.ComboNumber - 1;

                // when change attack early, disable hitbox immediately
                _hitboxController.ResetHitbox();
            }

            // tell hitboxController which attack we currently use
            _hitboxController.EnableDisableHitboxLogic(currentAttack);
        }


    }


}
