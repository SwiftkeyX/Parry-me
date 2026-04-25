using UnityEngine;
/// <summary>
/// Reusable
/// 
/// Role?
/// 1.apply gravity manually because we mainly use CharacterController
/// 2.calculate appropriate jump height
/// 3.calculate appropriate gravity
/// </summary>
namespace Entity
{
    public abstract class BaseGravity<T> : MonoBehaviour where T : BaseStateMachine<T>
    {
        // dependency
        private CharacterController _characterController;
        private T _stateMachine;
        private PlayerDebugList _debugList;

        // necessary var
        private float _defaultGravity = -9.8f;
        private float _airborneGravityForce;
        private float _groundedGravityForce = -1f;
        private float _fallMultiplier = 2f;

        void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _stateMachine = GetComponent<T>();
            _debugList = GetComponent<PlayerDebugList>();
            CalculateJumpHeight();
        }

        /// <summary>
        /// If the current entity can jump, calculate gravity force based on the those variable (MaxJumpHeight, MaxJumpTime).
        /// If not, then use default gravity force.
        /// </summary>
        private void CalculateJumpHeight()
        {
            if (_stateMachine is IJumpable jumpableEntity)
            {
                float timeToHighest = jumpableEntity.MaxJumpTime / 2;
                _airborneGravityForce = (-2 * jumpableEntity.MaxJumpHeight) / Mathf.Pow(timeToHighest, 2);
                jumpableEntity.InitialJumpVelocity = (2 * jumpableEntity.MaxJumpHeight) / timeToHighest;
            }
            else
            {
                _airborneGravityForce = _defaultGravity;
            }

        }

        /// <summary>
        /// Apply gravity force based on Entity's position.
        /// </summary>
        public void ApplyGravity()
        {
            _stateMachine.IsGrounded = _characterController.isGrounded;
            _stateMachine.IsFalling = (_stateMachine.Movement.y < 0);

            // character on the ground
            if (_stateMachine.IsGrounded)
            {
                _stateMachine.MovementMultiplierY = _groundedGravityForce;

                if (_debugList.DebugJump.IsDebugEnabled(DebugEntryKEY.GravityForceApply))
                    Debug.Log("Grounded Gravity Force apply");
            }

            // character is airbone and falling downward
            else if (_stateMachine.IsFalling)
            {
                float previousYVelocity = _stateMachine.MovementMultiplierY;
                float newYVelocity = _stateMachine.MovementMultiplierY + (_airborneGravityForce * _fallMultiplier * Time.deltaTime);
                float nextYVelocity = (previousYVelocity + newYVelocity) / 2;
                _stateMachine.MovementMultiplierY = nextYVelocity;

                if (_debugList.DebugJump.IsDebugEnabled(DebugEntryKEY.GravityForceApply))
                    Debug.Log("Airborne Falling GravityForce apply");

                if (_debugList.DebugJump.IsDebugEnabled(DebugEntryKEY.PreviousYAndNewY))
                    Debug.Log("previousY: " + previousYVelocity + " newY: " + newYVelocity);
            }

            // character is airbone and jumping upward
            else
            {
                float previousYVelocity = _stateMachine.MovementMultiplierY;
                float newYVelocity = _stateMachine.MovementMultiplierY + (_airborneGravityForce * Time.deltaTime);
                float nextYVelocity = (previousYVelocity + newYVelocity) / 2;
                _stateMachine.MovementMultiplierY = nextYVelocity;

                if (_debugList.DebugJump.IsDebugEnabled(DebugEntryKEY.GravityForceApply))
                    Debug.Log("Airborne Jumping GravityForce apply");

                if (_debugList.DebugJump.IsDebugEnabled(DebugEntryKEY.PreviousYAndNewY))
                    Debug.Log("previousY: " + previousYVelocity + " newY: " + newYVelocity);
            }
        }
    }
}
