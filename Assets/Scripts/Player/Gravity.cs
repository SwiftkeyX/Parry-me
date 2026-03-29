using UnityEngine;

/// <summary>
/// Reusable
/// 
/// Role?
/// 1.apply gravity manually because we mainly use CharacterController
/// 2.calculate appropriate jump height
/// 3.calculate appropriate gravity
/// </summary>
[RequireComponent(typeof(PlayerStateMachine))]
public class Gravity : MonoBehaviour
{
    // dependency
    private CharacterController _characterController;
    private PlayerStateMachine _playerStateMachine;

    // necessary var
    private float _airborneGravityForce;
    private float _groundedGravityForce = -1f;
    private float _fallMultiplier = 2f;

    // 


    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerStateMachine = GetComponent<PlayerStateMachine>();
        CalculateJumpHeight();
    }

    private void CalculateJumpHeight()
    {
        float timeToHighest = _playerStateMachine.MaxJumpTime / 2;
        _airborneGravityForce = (-2 * _playerStateMachine.MaxJumpHeight) / Mathf.Pow(timeToHighest, 2);
        _playerStateMachine.InitialJumpVelocity = (2 * _playerStateMachine.MaxJumpHeight) / timeToHighest;
    }

    public void ApplyGravity()
    {
        bool grounded = _characterController.isGrounded;
        bool isFalling = (_playerStateMachine.Movement.y < 0);

        // character on the ground
        if (grounded)
        {
            _playerStateMachine.MovementMultiplierY = _groundedGravityForce;
        }

        // character is airbone and falling downward
        // else if (isFalling)
        // {
        //     float previousYVelocity = _playerStateMachine.MovementMultiplierY;
        //     float newYVelocity = _playerStateMachine.MovementMultiplierY + (_airborneGravityForce * _fallMultiplier * Time.deltaTime);
        //     float nextYVelocity = (previousYVelocity + newYVelocity) / 2;
        //     _playerStateMachine.MovementMultiplierY = nextYVelocity;
        // }

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