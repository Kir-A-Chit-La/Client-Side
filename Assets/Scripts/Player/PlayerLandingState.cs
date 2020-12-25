using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : State
{
    public PlayerLandingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        Player.Animator.SetTrigger(Player.Animator.LandHash);
    }
    public override void HandleInput()
    {
        base.HandleInput();
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(Player.Animator.GetCurrentState(0).IsName("Standing Locomotion"))
            StateMachine.ChangeState(Player.StandingState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}
