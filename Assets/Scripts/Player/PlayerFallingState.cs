using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerAirborneState
{
    private bool _isGrounded;
    public PlayerFallingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Player.Animator.SetTrigger(Player.Animator.FallHash);
    }
    public override void HandleInput()
    {
        base.HandleInput();
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _isGrounded = Player.CheckGround();
        if(_isGrounded)
            StateMachine.ChangeState(Player.LandingState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}
