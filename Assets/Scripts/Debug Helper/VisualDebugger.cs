using TMPro;
using UnityEngine;

/// <summary>
/// not Reusable
/// 
/// Role?
/// 1.to debug AttackCombaoData of the player visually in the scene (using TMP)
/// </summary>
namespace Player
{
    public class VisualDebugger : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI debug_textUI;
        private PlayerAttackManager _PlayerAttackManager;

        void Awake()
        {
            _PlayerAttackManager = GetComponent<PlayerAttackManager>();
        }

        // void Start()
        // {
        //     // turn on/off TMP (text mesh pro) depend on script's debug mode
        //     if (!_PlayerAttackManager.debugMode) debug_textUI.enabled = false;
        // }

        // void Update()
        // {
        //     if (_PlayerAttackManager.debugMode)
        //     {
        //         debug_textUI.text
        //         = "timer: " + _PlayerAttackManager.Timer.ToString("F3") + "\n" +
        //         "ComboNumber: " + _PlayerAttackManager.ComboNumber + "\n" +
        //         "IsAttackFinish: " + _PlayerAttackManager.IsAttackFinish;
        //     }
        // }
    }
}
