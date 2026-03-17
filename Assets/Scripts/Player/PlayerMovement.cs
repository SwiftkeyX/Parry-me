using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputController _inputController;
    private CharacterController _characterController;

    private float _left_right_input;
    private float _up_down_input;

    // setter and getter
    public float Left_Right_input { get { return _left_right_input; } }
    public float Up_Down_input { get { return _up_down_input; } }

    void Awake()
    {
        _inputController = GetComponent<InputController>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        _left_right_input = _inputController.MovementDirection.x;
        _up_down_input = _inputController.MovementDirection.y;

        Vector3 moveDirection = new Vector3(0, 0, _left_right_input);
        float moveSpeed = 3f;
        _characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}