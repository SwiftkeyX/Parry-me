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
        [SerializeField] private List<AttackSO> _attackSO;
        private AttackBuffer _attackBuffer;
        private HitboxController _hitboxController;

        // var
        int currentAttack = -1;

        // setter and getter
        public AttackBuffer AttackBuffer { get { return _attackBuffer; } }

        void Awake()
        {
            _attackBuffer = GetComponent<AttackBuffer>();
            _hitboxController = GetComponent<HitboxController>();
        }

        void Start()
        {
            // injection
            for (int i = 0; i < _attackSO.Count; i++)
            {
                AttackBufferData a = new AttackBufferData(_attackSO[i].animOV, _attackSO[i].chainAttack, _attackSO[i].attackTimer);
                _attackBuffer.Data.Add(a);
            }

            // injection 2
            for (int i = 0; i < _attackSO.Count; i++)
            {
                HitboxData a = new HitboxData(_attackSO[i].enableHitboxTime, _attackSO[i].disableHitboxTime);
                _hitboxController.Data.Add(a);
            }
        }

        public void Attack(bool attackInput)
        {
            // if successfully attack, mean character is using "currentAttack"
            bool chainAttackSuccess = false;
            if (attackInput) chainAttackSuccess = _attackBuffer.Attack();
            if (chainAttackSuccess)
            {
                currentAttack = _attackBuffer.ComboNumber - 1;

                // when change attack early, disable hitbox immediately
                _hitboxController.ResetHitbox();
            }

            // tell hitboxController which attack we currently use
            _hitboxController.EnableDisableHitbox(currentAttack);
        }


    }


}
