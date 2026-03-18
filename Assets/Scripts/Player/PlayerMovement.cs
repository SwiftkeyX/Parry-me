using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputController _inputController;
    private CharacterController _characterController;

    private float _left_right_input;
    private float _up_down_input;
    private bool _run_input;
    Vector3 _moveDirection;
    float _moveSpeed;

    // readonly
    private readonly float _walkSpeed = 3f;
    private readonly float _runSpeed = 6f;

    // setter and getter
    public float Left_Right_input { get { return _left_right_input; } }
    public float Up_Down_input { get { return _up_down_input; } }
    public bool Run_input { get { return _run_input; } }

    void Awake()
    {
        _inputController = GetComponent<InputController>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // get input from InputController
        _left_right_input = _inputController.MovementDirection.x;
        _up_down_input = _inputController.MovementDirection.y;
        _run_input = _inputController.IsRunPressed;

        // calculate move direction
        _moveDirection = new Vector3(0, 0, _left_right_input);

        // adjust moveSpeed base on IsPlayerRun
        if (!_run_input) _moveSpeed = _walkSpeed;

        else if (_run_input) _moveSpeed = _runSpeed;

        // move the character forward using CharacterController component
        _characterController.Move(_moveDirection * _moveSpeed * Time.deltaTime);
    }
}