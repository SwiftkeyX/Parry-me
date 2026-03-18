using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private InputController _inputController;
    private Animator _animator;

    void Awake()
    {
        _inputController = GetComponent<InputController>(); 
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        // get input 
        bool attackInput = _inputController.IsAttackPressed;
        bool parryInput = _inputController.IsParryPresed;

        Debug.Log(attackInput.ToString());

        // play attack animation
        if (attackInput) _animator.SetTrigger("Attack");
    }
}