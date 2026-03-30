using UnityEngine;

/// <summary>
/// Reusable
/// 
/// Setup?
/// 1.attach this script to the attack (ex. player's weapon. enemy's attack)
/// 2.this attack must have collider with trigger on.
/// 3.both, player and enemy must have collider and characterController.
/// </summary>
public class Collision: MonoBehaviour
{
    [SerializeField] private TEAM _team;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("I am "+gameObject);
        GameObject target = other.gameObject;
        IDamageble targetStat = target.GetComponent<IDamageble>();

        // can't hit the same team
        if (targetStat.GetTeam() == _team) return;

        Debug.Log("target is hit " + target);
        targetStat.GetHit(10f);   
    }
}

public enum TEAM {PLAYER, ENEMY}