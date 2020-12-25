using System;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkedVar;
using MLAPI.NetworkedVar.Collections;
using System.Collections.Generic;

public class PlayerController : NetworkedBehaviour
{
    [SerializeField] private PlayerStats _stats;
    public PlayerStats Stats => Instantiate(_stats);
    [SerializeField] private PlayerConfig _config;
    public PlayerConfig Config  => _config;
    [SerializeField] InventoryManager _inventoryManager;
    public InventoryManager InventoryManager => _inventoryManager;
    public PlayerInputGrabber Input { get; private set; }
    public PlayerAnimator Animator { get; private set; }
    public Transform Transform { get; private set; }
    
    [SerializeField] private GameObject _playerInterface;
    private TriggersHandler _triggersHandler;
    private CharacterController _controller;
    [SerializeField] private Transform _groundChecker;

    public string currentState;
    public NetworkedObject CurrentFocus;
    
    public Action OnInventoryButton;
    public Action<float> OnPerformingState;

    private Vector3 _ceilingRay;
    private Vector3 _targetPos;

    #region  State Machine Properties
    private StateMachine _stateMachine;
    public PlayerStandingState StandingState { get; private set; }
    public PlayerCrouchedState CrouchedState { get; private set; }
    public PlayerJumpPrepState JumpPreparingState { get; private set; }
    public PlayerLeapOffState LeapOffState { get; private set; }
    public PlayerFallingState FallingState { get; private set; }
    public PlayerLandingState LandingState { get; private set; }
    public PlayerPerformingState PerformingState { get; private set; }
    #endregion
    #region  Public Methods
    public void Move(Vector3 motion) => _controller.Move(motion * Time.deltaTime);
    public void SetCrouchCollider()
    {
        _controller.center = Config.crouchColliderCenter;
        _controller.height = Config.crouchColliderHeight;
    }
    public void SetStandingCollider()
    {
        _controller.center = Config.standingColliderCenter;
        _controller.height = Config.standingColliderHeight;
    }
    public bool CheckGround() => Physics.CheckSphere(_groundChecker.position, Config.groundDistance, Config.groundLayer, QueryTriggerInteraction.Ignore);
    public bool CheckCeiling()
    {
        _ceilingRay = transform.position;
        _ceilingRay.y = Config.crouchColliderCenter.y + Config.crouchColliderHeight / 2;
        return Physics.Raycast(_ceilingRay, Vector3.up, Config.ceilingDistance, Config.ceilingLayer);
    }
    public void LookAt(Transform target)
    {
        _targetPos.x = target.position.x;
        _targetPos.y = 0f;
        _targetPos.z = target.position.z;
        Transform.LookAt(_targetPos);
    }
    #endregion
    #region  Mono Behaviour
    private void Start()
    {
        if(IsLocalPlayer)
        {
            InitStateMachine();
            Init();
        }
    }
    private void Update()
    {
        if(IsLocalPlayer)
        {
            _stateMachine.CurrentState.HandleInput();
            _stateMachine.CurrentState.UpdateLogic();
            currentState = _stateMachine.CurrentState.ToString();
        }
    }
    private void FixedUpdate()
    {
        if(IsLocalPlayer)
            _stateMachine.CurrentState.UpdatePhysics();
    }
    #endregion
    #region  Private Methods
    private void Init()
    {
        Input = GetComponent<PlayerInputGrabber>();
        Animator = GetComponent<PlayerAnimator>();
        Transform = GetComponent<Transform>();
        _triggersHandler = GetComponent<TriggersHandler>();
        _controller = GetComponent<CharacterController>();
        _playerInterface.SetActive(true);
        
        _triggersHandler.Init();
        Input.Init();
        Animator.Init();
        _inventoryManager.Init(Stats, Input);

        _triggersHandler.OnFocusChange += SetFocus;
    }
    private void InitStateMachine()
    {
        _stateMachine = new StateMachine();
        StandingState = new PlayerStandingState(this, _stateMachine);
        CrouchedState = new PlayerCrouchedState(this, _stateMachine);
        JumpPreparingState = new PlayerJumpPrepState(this, _stateMachine);
        LeapOffState = new PlayerLeapOffState(this, _stateMachine);
        FallingState = new PlayerFallingState(this, _stateMachine);
        LandingState = new PlayerLandingState(this, _stateMachine);
        PerformingState = new PlayerPerformingState(this, _stateMachine);
        
        _stateMachine.Initialize(StandingState);
    }
    private void SetFocus(NetworkedObject focus)
    {
        if(CurrentFocus != focus)
            CurrentFocus = focus;
    }
    #endregion
}