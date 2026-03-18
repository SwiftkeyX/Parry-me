using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    private InputAction _walkInput;
    private InputAction _runInput;
    private InputAction _jumpInput;
    private InputAction _attackInput;
    private InputAction _parryInput;

    private Vector2 _movementDirection;
    private bool _isRunPressed;
    private bool _isJumpPressed;
    private bool _isAttackPressed;
    private bool _isParryPressed;

    // setter and getter
    public Vector2 MovementDirection { get { return _movementDirection; } }
    public bool IsRunPressed { get { return _isRunPressed; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public bool IsAttackPressed { get { return _isAttackPressed; } }
    public bool IsParryPresed { get { return _isParryPressed; } }

    void Awake()
    {
        var map = inputActions.FindActionMap("PlayerInput");

        _walkInput = map.FindAction("walk");
        _runInput = map.FindAction("run");
        _jumpInput = map.FindAction("jump");
        _attackInput = map.FindAction("attack");
        _parryInput = map.FindAction("parry");
    }

    void Update()
    {
        // update to read input from player
        _movementDirection = _walkInput.ReadValue<Vector2>();
        _isRunPressed = _runInput.IsPressed();
        _isJumpPressed = _jumpInput.IsPressed();
        _isAttackPressed = _attackInput.IsPressed();
        _isParryPressed = _parryInput.IsPressed();

        // Debug.Log(_isAttackPressed.ToString());
    }


    void OnEnable()
    {
        _walkInput.Enable();
        _runInput.Enable();
        _jumpInput.Enable();
        _attackInput.Enable();
        _parryInput.Enable();
    }

    void OnDisable()
    {
        _walkInput.Disable();
        _runInput.Disable();
        _jumpInput.Disable();
        _attackInput.Disable();
        _parryInput.Disable();
    }

}
