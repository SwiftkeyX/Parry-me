using UnityEngine;

public class FrameRateLock : MonoBehaviour
{
    [SerializeField] private int targetFPS = 60;

    void Awake()
    {
        QualitySettings.vSyncCount = 0; // Disable VSync
        Application.targetFrameRate = targetFPS;
    }
}