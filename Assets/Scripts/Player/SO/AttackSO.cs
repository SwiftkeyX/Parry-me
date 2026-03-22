using UnityEngine;

[CreateAssetMenu(menuName = "Attack/AttackSO")]
public class AttackSO : ScriptableObject
{
    public AnimatorOverrideController animOV;
    public float dmg;
}