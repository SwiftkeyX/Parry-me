using TMPro;
using UnityEngine;

/// <summary>
/// not Reusable
/// 
/// 1.to debug AttackCombaoData of the player (using TMP)
/// </summary>
[RequireComponent(typeof(AttackComboData))]
public class VisualDebugger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debug_textUI;
    private AttackComboData _attackComboData;

    void Awake()
    {
        _attackComboData = GetComponent<AttackComboData>();
    }

    void Start()
    {
        // turn on/off TMP (text mesh pro) depend on script's debug mode
        if (!_attackComboData.debugMode) debug_textUI.enabled = false;
    }

    void Update()
    {
        if (_attackComboData.debugMode)
        {
            debug_textUI.text
            = "timer: " + _attackComboData.Timer.ToString("F3") + "\n" +
            "ComboNumber: " + _attackComboData.ComboNumber + "\n" +
            "IsAttackFinish: " + _attackComboData.IsAttackFinish;
        }
    }
}