using UnityEngine;

/// <summary>
/// Due to each State (idle/ walk/ run/ etc...) maybe need a lot more dependency from PlayerStateMachine in the future
/// Instead of me have to manually add new Parameter everytime
/// I will create BlackBoard that will keep all the dependency need in the future. and send this object to each State
/// 
/// this script role is:
/// 1.keep dependency for each State => so we can send the dependency easily
/// 2.this also act as a glue between script
/// 2.1 glue between "InputController" and "InputProcessor"
/// </summary>
public class StateMachineBlackBoard : MonoBehaviour
{
    private PlayerStateMachine _playerStateMachine;
    private CharacterController _characterController;
    private Animator _animator;
    private InputController _inputController;
    private InputProcessor _inputProcessor;

    // setter and getter
    public PlayerStateMachine PlayerStateMachine { get { return _playerStateMachine; } }
    public CharacterController CharacterController { get { return _characterController; } }
    public Animator Animator { get { return _animator; } }
    public InputController InputController { get { return _inputController; } }
    public InputProcessor InputProcessor { get { return _inputProcessor; } }

    void Awake()
    {
        _playerStateMachine = GetComponent<PlayerStateMachine>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _inputController = GetComponent<InputController>();
        _inputProcessor = new InputProcessor(_inputController);
    }

    void Update()
    {
        // basically calculate which direction player move to
        _inputProcessor.GetInput();
        _inputProcessor.ProcessInput();
    }
}