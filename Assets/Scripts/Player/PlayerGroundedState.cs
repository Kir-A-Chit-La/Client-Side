using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : State
{
    protected Vector3 Velocity;
    protected float Speed;
    private bool _isGrounded;
    private bool _openInventory;
    public PlayerGroundedState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void HandleInput()
    {
        base.HandleInput();
        Velocity.x = Player.Input.GetIsometricAxis().x;
        Velocity.z = Player.Input.GetIsometricAxis().y;
        _openInventory = Player.Input.GetInvetoryButton();
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _isGrounded = Player.CheckGround();
        if(!_isGrounded)
            StateMachine.ChangeState(Player.FallingState);
        
        if(_openInventory)
        {
            Player.OnInventoryButton?.Invoke();
        }
        
        Player.Animator.SetFloat(Player.Animator.HorizontalVelocityHash, Velocity.x, 0.05f, Time.deltaTime);
        Player.Animator.SetFloat(Player.Animator.VerticalVelocityHash, Velocity.z, 0.05f, Time.deltaTime);

        if(Velocity != Vector3.zero)
            Player.Transform.forward = Velocity;
        
        Player.Move(Velocity * Speed);
    }
    public override void Exit()
    {
        base.Exit();
        Player.Animator.ResetVelocity();
    }
}
