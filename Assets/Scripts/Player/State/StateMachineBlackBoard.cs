using UnityEngine;

/// <summary>
/// Not Reusable
/// 
/// Due to each State (idle/ walk/ run/ etc...) maybe need a lot more dependency from PlayerStateMachine in the future
/// Instead of me have to manually add new Parameter everytime
/// I will create BlackBoard that will keep all the dependency need in the future. and send this object to each State
/// 
/// this script role is:
/// 1.to be a glue between PlayerStateMachine and Concrete State 
/// 2.keep dependency for each State => so we can send the dependency to each state easily
/// 3.this also act as a temporaliry glue between script
///     3.1 glue between "InputController" and "PlatformerInputProcessor" (now, not anymore)
///     3.2 glue betwen "Attack State" and "AttackComboData" (now, not anymore)
///     3.3 glue between "PlayerStateMachine" and "Gravity" (now, not anymore)
///     3.4 glue between "PlayerStateMachine" and "Concrete State"
/// </summary>
public class StateMachineBlackBoard : MonoBehaviour
{
    // component var
    private PlayerStateMachine _playerStateMachine;
    private CharacterController _characterController;
    private Animator _animator;
    private InputController _inputController;
    private AttackComboData _attackComboData;
    private Gravity _gravity;

    // ============================== setter and getter ==============================
    // component getter
    public PlayerStateMachine PlayerStateMachine { get { return _playerStateMachine; } }
    public CharacterController CharacterController { get { return _characterController; } }
    public Animator Animator { get { return _animator; } }
    public InputController InputController { get { return _inputController; } }
    public AttackComboData AttackComboData { get { return _attackComboData; } }
    public Gravity Gravity { get { return _gravity; } }

    void Awake()
    {
        _playerStateMachine = GetComponent<PlayerStateMachine>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _inputController = GetComponent<InputController>();
        _attackComboData = GetComponent<AttackComboData>();
        _gravity = GetComponent<Gravity>();

    }
}