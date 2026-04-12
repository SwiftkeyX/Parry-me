using UnityEngine;

public class EnemyStat : MonoBehaviour, IDamageble
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _baseAttack = 10f;
    [SerializeField] private Healthbar _healthbar;
    [SerializeField] private TEAM _team;

    void Start()
    {
        _healthbar.InitialHealthbar(_maxHealth);
    }

    public void GetHit(float dmg)
    {
        _health -= dmg;
        _healthbar.SetCurrentHealth(_health);
    }

    public TEAM GetTeam()
    {
        return _team;
    }
}