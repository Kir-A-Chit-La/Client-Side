using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState : State
{
    public Vector3 Velocity = Vector3.zero;
    public PlayerAirborneState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void HandleInput()
    {
        base.HandleInput();
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        Velocity.y += Player.Config.gravity * Time.deltaTime;
        Player.Move(Velocity);
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("RESETING " + Velocity);
        Velocity = Vector3.zero;
    }
}
