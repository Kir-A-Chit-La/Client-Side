using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class PlayerPerformingState : State
{
    private float _perfomingTime;
    private IInteractable _interactable;
    private ILootable _lootable;
    public PlayerPerformingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _interactable = Player.CurrentFocus.GetComponent<IInteractable>();

        _perfomingTime = Time.time + _interactable.InteractionTime;
        Player.OnPerformingState?.Invoke(_interactable.InteractionTime);

        Player.StartCoroutine(Perform());
        Player.Animator.SetBool(Player.Animator.PerformHash, true);
        Player.Animator.SetTrigger(Player.Animator.TreeSlashHash);
    }
    public override void HandleInput()
    {
        base.HandleInput();
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
    public override void Exit()
    {
        base.Exit();
        Player.Animator.SetBool(Player.Animator.PerformHash, false);
    }
    private IEnumerator Perform()
    {
        Player.LookAt(Player.CurrentFocus.transform);
        while(Time.time < _perfomingTime)
        {
            if(Player.CurrentFocus == null)
            {
                StateMachine.ChangeState(Player.StandingState);
                yield break;
            }
            yield return null;
        }
        if(Player.CurrentFocus == null)
        {
            StateMachine.ChangeState(Player.StandingState);
            yield break;
        }
        Player.InvokeServerRpc(Interuct, Player.CurrentFocus.NetworkId);
        StateMachine.ChangeState(Player.StandingState);
    }
    [ServerRPC] private void Interuct(ulong targetNetworkId) {}
}
