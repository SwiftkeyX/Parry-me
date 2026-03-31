using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reusable
/// calculate "Is Attack buffer" for the player
/// 
/// How?
/// 1.use Coroutine to time when the combo should stop 
///     (if attack in this time, the combo will reset => start from first attack again)
/// 2.use "chainAttack" as a timer to prevent current attack animation end early 
///     (next attack can override current animation by accident).
/// </summary>
public class AttackBuffer : MonoBehaviour
{
    private List<AttackBufferData> _data;
    private Animator _animator;

    // attack buffer var
    private int _comboNumber;
    private float _lastClickedTime;
    private bool _isAttackFinish;
    private Coroutine _attackTimer;
    private float _timer;

    // getter and setter
    public int ComboNumber => _comboNumber;
    public bool IsAttackFinish => _isAttackFinish;
    public Coroutine GetAttackTimer => _attackTimer;
    public float Timer => _timer;

    // getter and setter
    public List<AttackBufferData> Data { get { return _data; } }   

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _data = new List<AttackBufferData>();
    }

    void Start()
    {
        _comboNumber = 0;
        _lastClickedTime = Time.time;
        _isAttackFinish = true;
    }

    // logic to allow the current attack to chain to next attack
    public bool Attack()
    {
        if (_comboNumber >= _data.Count) return false;

        // calculate attack buffer
        bool isAttackBuffer;
        if (_comboNumber == 0) isAttackBuffer = true;
        else isAttackBuffer = (Time.time - _lastClickedTime >= _data[_comboNumber - 1].chainAttack);

        // play attack animation
        if (isAttackBuffer)
        {
            // reset the attack timer, if player continue attack => AttackEnd() won't be called  
            if (_attackTimer != null) StopCoroutine(_attackTimer);
            // start the attack timer, if timer is run out => call AttackEnd()
            _attackTimer = StartCoroutine(AttackTimer(_data[_comboNumber].attackTimer));

            // play animation
            _animator.runtimeAnimatorController = _data[_comboNumber].animOV;
            _animator.CrossFade("Attack", 0.1f, 0, 0f);

            // update flag
            _comboNumber++;
            _lastClickedTime = Time.time;
            _isAttackFinish = false;
            return true;
        }

        return false;
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

// keep necessary variable to calculate attack buffer
public class AttackBufferData
{
    // over write previous attack animation
    public AnimatorOverrideController animOV;

    // time: use in logic that allow player to chain the attack (a1 => a2 => a3) 
    public float chainAttack;

    // time: use in same logic as "chainAttack"
    public float attackTimer;

    public AttackBufferData(AnimatorOverrideController anim, float chain, float timer)
    {
        animOV = anim;
        chainAttack = chain;
        attackTimer = timer;
    }
}