using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeapOffState : PlayerAirborneState
{
    public PlayerLeapOffState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Player.Animator.SetTrigger(Player.Animator.LeapOffHash);
        Velocity.x = Player.Input.GetIsometricAxis().x * Player.Stats.StandingSpeed.BaseValue;
        Player.FallingState.Velocity.x = Velocity.x;
        Velocity.y += Mathf.Sqrt(-2f * Player.Config.jumpHeight * Player.Config.gravity);
        Velocity.z = Player.Input.GetIsometricAxis().y * Player.Stats.StandingSpeed.BaseValue;
        Player.FallingState.Velocity.z = Velocity.z;
        Debug.Log(Velocity);
    }
    public override void HandleInput()
    {
        base.HandleInput();
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(Player.Animator.GetCurrentState(0).normalizedTime >= 1f && Velocity.y <= 0f)
            StateMachine.ChangeState(Player.FallingState);
    }
    public override void Exit()
    {

    }
}
