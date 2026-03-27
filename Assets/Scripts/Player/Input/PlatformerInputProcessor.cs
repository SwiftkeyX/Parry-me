using UnityEngine;

/// <summary>
/// Not Reusable
/// 
/// why is it here? 
/// glue between State and InputController
/// 
/// role? 
/// get input and process in a way Platformer should
/// </summary>
public class PlatformerInputProcessor : MonoBehaviour
{
    private PlayerStateMachine _playerStateMachine;
    private InputController _inputController;

    void Awake()
    {
        _playerStateMachine = GetComponent<PlayerStateMachine>();
        _inputController = GetComponent<InputController>();
    }

    void Update()
    {
        ProcessInput();
    }

    public void ProcessInput()
    {
        // player should be able to walk only left or right
        // can jump only with jump button
        // that why z-axis was 0
        _playerStateMachine.MovementDirection = new Vector3(_inputController.MovementDirection.x, 1, 0);

        _playerStateMachine.RunInput = _inputController.IsRunPressed;

        _playerStateMachine.JumpInput = _inputController.IsJumpPressed;

        _playerStateMachine.AttackInput = _inputController.IsAttackPressed;

    }
}