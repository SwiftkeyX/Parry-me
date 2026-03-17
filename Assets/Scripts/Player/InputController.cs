using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    private InputAction walkAction;
    private InputAction runAction;

    void Awake()
    {
        var map = inputActions.FindActionMap("PlayerInput");

        walkAction = map.FindAction("walk");
        runAction = map.FindAction("run");
    }


    void Update()
    {
        Vector2 move = walkAction.ReadValue<Vector2>();
        bool run = runAction.IsPressed();

        Debug.Log("move" + move);
        Debug.Log("run" + run);
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
