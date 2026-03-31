using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Not Reusable 
/// 
/// Role?
/// 1.to keep AttackSO
/// 2.to be glue for attack system
/// 
/// </summary>
public class AttackComboData : MonoBehaviour
{
    [SerializeField] private List<AttackSO> _attackSO;
    [SerializeField] private CollisionController _collisionController;
    private Animator _animator;
    private AttackBuffer _attackBuffer;
    private PlayerDebug _playerDebug;

    // var
    int currentAttack = -1;

    // setter and getter
    public AttackBuffer AttackBuffer { get { return _attackBuffer; } }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _attackBuffer = GetComponent<AttackBuffer>();
        _playerDebug = GetComponent<PlayerDebug>();
    }

    void Start()
    {
        // injection
        for (int i = 0; i < _attackSO.Count; i++)
        {
            AttackBufferData a = new AttackBufferData(_attackSO[i].animOV, _attackSO[i].chainAttack, _attackSO[i].attackTimer);
            _attackBuffer.Data.Add(a);
        }
    }

    public void Attack(bool attackInput)
    {
        // ======================= logic for buffer the attack =======================
        // if successfully attack, mean character is using "currentAttack"
        bool chainAttackSuccess = false;
        if (attackInput) chainAttackSuccess = _attackBuffer.Attack();
        if (chainAttackSuccess)
        {
            currentAttack = _attackBuffer.ComboNumber - 1;

            // when change attack early, disable hitbox immediately
            _collisionController.DisableHitbox();

            if (_playerDebug.DebugCollision.IsDebugEnabled(DebugEntryKEY.HitboxTiming))
                Debug.Log("[Collision] current attack: " + currentAttack + " Disable the hitbox Early");
        }


        // ======================= logic for enable, disable hitbox =======================
        // guard
        if (currentAttack < 0 || currentAttack >= _attackSO.Count) return;

        float animator_time = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

        if (_playerDebug.DebugCollision.IsDebugEnabled(DebugEntryKEY.HitboxTiming))
            Debug.Log("[Collision] current attact: " + currentAttack + " animator time: " + animator_time +
            " enable time: " + _attackSO[currentAttack].enableHitboxTime +
            " disable time: " + _attackSO[currentAttack].disableHitboxTime);

        if (animator_time > _attackSO[currentAttack].enableHitboxTime && animator_time <= _attackSO[currentAttack].disableHitboxTime)
        {
            _collisionController.EnableHitbox();

            if (_playerDebug.DebugCollision.IsDebugEnabled(DebugEntryKEY.HitboxTiming))
                Debug.Log("[Collision] current attack: " + currentAttack + " Enable the hitbox");
        }

        else if (animator_time > _attackSO[currentAttack].disableHitboxTime)
        {
            _collisionController.DisableHitbox();

            if (_playerDebug.DebugCollision.IsDebugEnabled(DebugEntryKEY.HitboxTiming))
                Debug.Log("[Collision] current attack: " + currentAttack + " Disable the hitbox");
        }

    }


}

