using UnityEngine;
using Entity;
/// <summary>
/// Reusable
/// 
/// Role?
/// 1.apply gravity manually because we mainly use CharacterController
/// 2.calculate appropriate jump height
/// 3.calculate appropriate gravity
/// </summary>
public class Gravity : MonoBehaviour
{
    // dependency
    private CharacterController _characterController;
    private StateMachine _stateMachine;
    private PlayerDebug _playerDebug;

    // necessary var
    private float _airborneGravityForce;
    private float _groundedGravityForce = -1f;
    private float _fallMultiplier = 2f;

    // 


    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _stateMachine = GetComponent<StateMachine>();
        _playerDebug = GetComponent<PlayerDebug>();
        CalculateJumpHeight();
    }

    private void CalculateJumpHeight()
    {
        float timeToHighest = _stateMachine.MaxJumpTime / 2;
        _airborneGravityForce = (-2 * _stateMachine.MaxJumpHeight) / Mathf.Pow(timeToHighest, 2);
        _stateMachine.InitialJumpVelocity = (2 * _stateMachine.MaxJumpHeight) / timeToHighest;
    }

    public void ApplyGravity()
    {
        _stateMachine.IsGrounded = _characterController.isGrounded;
        _stateMachine.IsFalling = (_stateMachine.Movement.y < 0);

        // character on the ground
        if (_stateMachine.IsGrounded)
        {
            _stateMachine.MovementMultiplierY = _groundedGravityForce;

            if (_playerDebug.DebugJump.IsDebugEnabled(DebugEntryKEY.GravityForceApply))
                Debug.Log("Grounded Gravity Force apply");
        }

        // character is airbone and falling downward
        else if (_stateMachine.IsFalling)
        {
            float previousYVelocity = _stateMachine.MovementMultiplierY;
            float newYVelocity = _stateMachine.MovementMultiplierY + (_airborneGravityForce * _fallMultiplier * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) / 2;
            _stateMachine.MovementMultiplierY = nextYVelocity;

            if (_playerDebug.DebugJump.IsDebugEnabled(DebugEntryKEY.GravityForceApply))
                Debug.Log("Airborne Falling GravityForce apply");

            if (_playerDebug.DebugJump.IsDebugEnabled(DebugEntryKEY.PreviousYAndNewY))
                Debug.Log("previousY: " + previousYVelocity + " newY: " + newYVelocity);
        }

        // character is airbone and jumping upward
        else
        {
            float previousYVelocity = _stateMachine.MovementMultiplierY;
            float newYVelocity = _stateMachine.MovementMultiplierY + (_airborneGravityForce * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) / 2;
            _stateMachine.MovementMultiplierY = nextYVelocity;

            if (_playerDebug.DebugJump.IsDebugEnabled(DebugEntryKEY.GravityForceApply))
                Debug.Log("Airborne Jumping GravityForce apply");

            if (_playerDebug.DebugJump.IsDebugEnabled(DebugEntryKEY.PreviousYAndNewY))
                Debug.Log("previousY: " + previousYVelocity + " newY: " + newYVelocity);
        }
    }
}