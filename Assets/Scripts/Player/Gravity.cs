using UnityEngine;

public class Gravity : MonoBehaviour
{
    // dependency
    private CharacterController _characterController;
    private StateMachineBlackBoard _bb;

    // necessary var
    private float _gravityForce = -9.8f;
    private Vector3 _verticalDir = new Vector3(0, 1, 0);

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _bb = GetComponent<StateMachineBlackBoard>();
    }

    void Update()
    {
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        if (_bb.VerticalMovement > -1f) _bb.VerticalMovement += _gravityForce * Time.deltaTime;

        else _bb.VerticalMovement = -1f;

        _characterController.Move(_verticalDir * _bb.VerticalMovement * Time.deltaTime);
    }
}