using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchedState : PlayerGroundedState
{
    private bool _isStanding;
    private bool _isUnderCeil;
    public PlayerCrouchedState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Speed = Player.Stats.CrouchingSpeed.BaseValue;
        Player.Animator.SetBool(Player.Animator.CrouchingHash, true);
        Player.SetCrouchCollider();
    }
    public override void HandleInput()
    {
        base.HandleInput();
        _isStanding = !Player.Input.GetCrouchButton();
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(_isStanding && !_isUnderCeil)
            StateMachine.ChangeState(Player.StandingState);
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _isUnderCeil = Player.CheckCeiling();
    }
    public override void Exit()
    {
        base.Exit();
        Player.Animator.SetBool(Player.Animator.CrouchingHash, false);
        Player.SetStandingCollider();
    }
}
