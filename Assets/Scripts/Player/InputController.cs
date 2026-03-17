using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    private InputAction walkAction;
    private InputAction runAction;

    private Vector2 _movementDirection;
    private bool _isRunPressed;

    // setter and getter
    public Vector2 MovementDirection { get { return _movementDirection; } set { _movementDirection = value; } }
    public bool IsRunPressed { get { return _isRunPressed; } set { _isRunPressed = value; } }

    void Awake()
    {
        var map = inputActions.FindActionMap("PlayerInput");

        walkAction = map.FindAction("walk");
        runAction = map.FindAction("run");
    }

    void Update()
    {
        // update to read input from player
        _movementDirection = walkAction.ReadValue<Vector2>();
        _isRunPressed = runAction.IsPressed();
    }


    void OnEnable()
    {
        walkAction.Enable();
        runAction.Enable();
    }

    void OnDisable()
    {
        walkAction.Disable();
        runAction.Disable();
    }

}
