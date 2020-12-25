using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputGrabber : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    private InputManager _input;
    private Vector2 _pointerPosition;
    private Vector2 _axis;
    private Vector2 _isometricAxis;
    private bool _jump;
    private bool _crouch;
    private bool _crawl;
    [SerializeField] private bool _interuct;
    private bool _openInventory;
    public Vector2 GetPointerPosition() => _pointerPosition;
    public Vector2 GetAxis() => _axis;
    public Vector2 GetIsometricAxis()
    {
        _isometricAxis.x = _axis.x + _axis.y;
        _isometricAxis.y = _axis.y - _axis.x;
        return _isometricAxis;
    }
    public bool GetJumpButton() => _jump;
    public bool GetCrouchButton() => _crouch;
    public bool GetInteructButton() => _interuct;
    public bool GetInvetoryButton() => _openInventory;
    #region  MonoBehaviour Methods
    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();
    private void Awake()
    {
        _input = new InputManager();
    }
    public void Init()
    {
        _input.UI.Point.performed += context => _pointerPosition = context.ReadValue<Vector2>();
        _input.Player.Movement.performed += context => _axis = context.ReadValue<Vector2>();
        _input.Player.Jump.performed += context => _jump = context.ReadValueAsButton();
        _input.Player.Crouch.performed += context => _crouch = !_crouch;
        _input.Player.Crawl.performed += context => _crawl = !_crawl;
        _input.Player.Interuct.performed += context => _interuct = context.ReadValueAsButton();
        _input.Player.OpenInventory.performed += context => _openInventory = context.ReadValueAsButton();
    }
    #endregion
}
