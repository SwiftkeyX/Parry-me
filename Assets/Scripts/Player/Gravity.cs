using UnityEngine;

/// <summary>
/// Reusable
/// 
/// Role?
/// 1.apply gravity manually because we mainly use CharacterController
/// 2.calculate appropriate jump height
/// </summary>
public class Gravity : MonoBehaviour
{
    // dependency
    private CharacterController _characterController;
    private PlayerStateMachine _playerStateMachine;

    // necessary var
    private float _airborneGravityForce = -9.8f;
    private float _groundedGravityForce = -1f;
    private bool _isFalling;
    private float _fallMultiplier = 2f;

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    void Update()
    {
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        // character on the ground
        if (_characterController.isGrounded)
        {
            _playerStateMachine.MovementMultiplierY = _groundedGravityForce;
        }

        // character is airbone and falling downward
        else if (_isFalling)
        {
            float previousYVelocity = _playerStateMachine.MovementMultiplierY;
            float newYVelocity = _playerStateMachine.MovementMultiplierY + (_airborneGravityForce * _fallMultiplier * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) / 2;
            _playerStateMachine.MovementMultiplierY = nextYVelocity;
        }

        // character is airbone and jumping upward
        else
        {
            float previousYVelocity = _playerStateMachine.MovementMultiplierY;
            float newYVelocity = _playerStateMachine.MovementMultiplierY + (_airborneGravityForce * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) / 2;
            _playerStateMachine.MovementMultiplierY = nextYVelocity;
        }
    }
}