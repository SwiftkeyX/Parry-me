using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Reusable
/// 
/// 
/// </summary>
public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform _targetHead;
    [SerializeField] private Vector3 _offset;

    /// <summary>
    ///  make healthbar appear above target's head
    /// </summary>
    void LateUpdate()
    {
        if (_targetHead != null) transform.position = _targetHead.position + _offset;
    }

    public void InitialHealthbar(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetCurrentHealth(float currentHealth)
    {
        slider.value = currentHealth;
    }
}
