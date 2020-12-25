using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandingState : PlayerGroundedState
{
    private bool _isCrouching;
    private bool _isJumping;
    private bool _interuct;
    public PlayerStandingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Speed = Player.Stats.StandingSpeed.BaseValue;
    }
    public override void HandleInput()
    {
        base.HandleInput();
        _isJumping = Player.Input.GetJumpButton();
        _isCrouching = Player.Input.GetCrouchButton();
        _interuct = Player.Input.GetInteructButton();
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        Player.CheckCeiling();
        if(_isJumping)
            StateMachine.ChangeState(Player.JumpPreparingState);
        if(_isCrouching)
            StateMachine.ChangeState(Player.CrouchedState);
        if(_interuct && Player.CurrentFocus != null)
            StateMachine.ChangeState(Player.PerformingState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}
