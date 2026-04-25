using UnityEngine;

/// <summary>
/// I don't remember what is this for.
/// </summary>
public class FrameRateLock : MonoBehaviour
{
    [SerializeField] private int targetFPS = 60;

    void Awake()
    {
        QualitySettings.vSyncCount = 0; // Disable VSync
        Application.targetFrameRate = targetFPS;
    }
}