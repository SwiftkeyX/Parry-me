using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private Coroutine _attackTimer;
    private float _timer;

    // debug
    public bool debugMode;

    // getter and setter
    public int ComboNumber => _comboNumber;
    public bool IsAttackFinish => _isAttackFinish;
    public Coroutine GetAttackTimer => _attackTimer;
    public float Timer => _timer;

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

    }

    // logic to allow the current attack to chain to next attack
    public void Attack()
    {
        if (_comboNumber >= _attackSO.Count) return;

        // calculate attack buffer
        bool isAttackBuffer;
        if (_comboNumber == 0) isAttackBuffer = true;
        else isAttackBuffer = (Time.time - _lastClickedTime >= _attackSO[_comboNumber - 1].chainAttack);

        // play attack animation
        if (isAttackBuffer)
        {
            // start the attack timer, if timer is run out => call AttackEnd()
            // reset the attack timer, if player continue attack => AttackEnd() won't be called  
            if (_attackTimer != null) StopCoroutine(_attackTimer);
            _attackTimer = StartCoroutine(AttackTimer(_attackSO[_comboNumber].attackTimer));

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

    // attack timer => to allow/not allow player from exiting attack state
    private IEnumerator AttackTimer(float t)
    {
        _timer = t;

        while (_timer > 0f)
        {
            _timer -= Time.deltaTime;

            yield return null;
        }

        AttackEnd();
    }

}

