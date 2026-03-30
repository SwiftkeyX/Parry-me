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

    // setter and getter
    public AttackBuffer AttackBuffer { get { return _attackBuffer; } }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _attackBuffer = GetComponent<AttackBuffer>();
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

    public void Attack()
    {
        // enable, disable hitbox
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        int comboNumber = _attackBuffer.ComboNumber;

        // guard
        if (comboNumber >= _attackSO.Count) return;

        if (stateInfo.normalizedTime > _attackSO[comboNumber].enableHitboxTime && stateInfo.normalizedTime <= _attackSO[comboNumber].disableHitboxTime)
            _collisionController.EnableHitbox();

        else if (stateInfo.normalizedTime > _attackSO[comboNumber].disableHitboxTime)
            _collisionController.DisableHitbox();


        // buffer the attack, play the attack animation, comboNumber++
        _attackBuffer.Attack();
    }

}

