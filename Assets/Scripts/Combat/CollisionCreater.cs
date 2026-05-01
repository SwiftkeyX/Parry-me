using UnityEngine;

/// <summary>
/// Reusable
/// 
/// Setup?
/// 1.attach this script to the attack (ex. player's weapon. enemy's attack)
/// 2.this attack must have collider with trigger on.
/// 3.both, player and enemy must have collider and characterController.
/// 
/// How to use?
/// attacker's gameobject called either "EnableHitbox()" or "DisableHitbox()"
/// </summary>
public class CollisionCreater : MonoBehaviour
{
    [SerializeField] private TEAM _myTeam;
    private Collider _collider;

    void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    void Start()
    {
        DisableHitbox();
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        IDamageable targetStat = target.GetComponent<IDamageable>();
        ITeam targetTeam = target.GetComponent<ITeam>();

        // can't hit the same team
        if (targetTeam.GetTEAM() == _myTeam) return;

        targetStat.GetHit(10f);
    }

    public void EnableHitbox()
    {
        _collider.isTrigger = true;
    }

    public void DisableHitbox()
    {
        _collider.isTrigger = false;
    }
}

public enum TEAM { PLAYER, ENEMY }