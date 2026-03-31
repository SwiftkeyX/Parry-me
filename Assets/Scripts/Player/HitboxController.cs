

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reusable
/// 
/// Role?
/// 1.Enable/Disable hitbox
/// 2.use with CollisionCreater
/// </summary>
public class HitboxController : MonoBehaviour
{
    private List<HitboxData> _data;
    private Animator _animator;
    [SerializeField] private CollisionCreater _CollisionCreater;

    // getter and setter
    public List<HitboxData> Data { get { return _data; } set { value = _data; } }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _data = new List<HitboxData>();
    }

    public void EnableDisableHitbox(int currentAttack)
    {
        if (currentAttack < 0 || currentAttack >= _data.Count) return;

        float animator_time = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

        if (animator_time > _data[currentAttack].enableHitboxTime && animator_time <= _data[currentAttack].disableHitboxTime)
        {
            _CollisionCreater.EnableHitbox();
        }

        else if (animator_time > _data[currentAttack].disableHitboxTime)
        {
            _CollisionCreater.DisableHitbox();
        }
    }

    public void ResetHitbox()
    {
        _CollisionCreater.DisableHitbox();
    }
}

public class HitboxData
{
    public float enableHitboxTime;
    public float disableHitboxTime;

    public HitboxData(float et, float dt)
    {
        enableHitboxTime = et;
        disableHitboxTime = dt;
    }
}