using UnityEngine;

/// <summary>
/// because I can't think of where should the game process player's input are
/// so I create new class just for process player's input
/// </summary>
public class InputProcessor
{
    private InputController _inputController;

    private float _left_right_input;
    private float _up_down_input;
    private bool _run_input;
    private bool _attack_input;
    private Vector3 _moveDirection;

    // setter and getter
    public float Left_Right_input { get { return _left_right_input; } }
    public float Up_Down_input { get { return _up_down_input; } }
    public bool Run_input { get { return _run_input; } }
    public bool Attack_input { get { return _attack_input; } }
    public Vector3 MoveDirection { get { return _moveDirection; } }

    public InputProcessor(InputController inputController)
    {
        _inputController = inputController;
    }

    public void GetInput()
    {
        _left_right_input = _inputController.MovementDirection.x;
        _up_down_input = _inputController.MovementDirection.y;
        _run_input = _inputController.IsRunPressed;
        _attack_input = _inputController.IsAttackPressed;
    }

    public void ProcessInput()
    {
        // calculate move direction
        _moveDirection = new Vector3(0, 0, _left_right_input);
    }
}