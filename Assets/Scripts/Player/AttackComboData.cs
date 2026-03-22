using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1.to keep AttackSO
/// 2.to apply new AnimatorOverrideController to existing Animator during runtime
/// 3.to calucalate attack timer
/// </summary>
public class AttackComboData : MonoBehaviour
{
    [SerializeField] private List<AttackSO> _attackSO;
    private Animator _animator;

    private int _comboNumber;
    private float _lastClickedTime;
    private bool _isAttackFinish;
    private float _animationFinish = 0.9f;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _comboNumber = 0;
        _lastClickedTime = Time.time;
        _isAttackFinish = true;
    }

    void Update()
    {
        UpdateAttackTimer();
    }

    // logic to allow the current attack to chain to next attack
    public void Attack()
    {
        if (_comboNumber >= _attackSO.Count) return;

        // calculate attack buffer
        bool isAttackBuffer;
        if (_comboNumber == 0) isAttackBuffer = true;
        else isAttackBuffer = (Time.time - _lastClickedTime >= _attackSO[_comboNumber-1].chainAttack);

        // play attack animation
        if (isAttackBuffer)
        {
            // in case the invoke is fired, cancel invoke called if the player continue attack
            CancelInvoke("AttackEnd");

            _animator.runtimeAnimatorController = _attackSO[_comboNumber].animOV;
            _animator.CrossFade("Attack", 0.1f, 0, 0f);

            _comboNumber++;
            _lastClickedTime = Time.time;
            _isAttackFinish = false;
        }
    }

    // reset var when attack end
    private void AttackEnd()
    {
        _comboNumber = 0;
        _isAttackFinish = true;
    }

    // timer to count if the attack should be finish
    private void UpdateAttackTimer()
    {
        bool isAttackAnimationEnd = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= _animationFinish;
        bool isInAttack = _animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");

        // call AttackEnd() 1 sec after the attack animation is finished
        if (isAttackAnimationEnd && isInAttack) Invoke("AttackEnd", 0.6f);
    }

    public bool IsAttackFinish() => _isAttackFinish;
}

