using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpPrepState : State
{
    public PlayerJumpPrepState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        Player.Animator.SetTrigger(Player.Animator.JumpPreparationHash);
    }
    public override void HandleInput()
    {
        base.HandleInput();
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(Player.Animator.GetCurrentState(0).normalizedTime >= 1f)
            StateMachine.ChangeState(Player.LeapOffState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}
