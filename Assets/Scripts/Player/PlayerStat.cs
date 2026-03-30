using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _baseAttack = 10f;
    [SerializeField] private Healthbar _healthbar;

    void Start()
    {
        _healthbar.InitialHealthbar(_maxHealth);
    }

    private void GetHit(float dmg)
    {
        _health -= dmg;
        _healthbar.SetCurrentHealth(_health);
    }
}