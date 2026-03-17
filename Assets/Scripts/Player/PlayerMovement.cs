using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputController _inputController;
    private CharacterController _characterController;

    void Awake()
    {
        _inputController = GetComponent<InputController>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 moveDirection = new Vector3(1, 0, 1);
        float moveSpeed = 3f;
        _characterController.Move(moveDirection * moveSpeed);
    }
}