using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected PlayerController Player;
    protected StateMachine StateMachine;
    protected State(PlayerController player, StateMachine stateMachine)
    {
        this.Player = player;
        this.StateMachine = stateMachine;
    }
    public virtual void Enter()
    {

    }
    public virtual void HandleInput()
    {

    }
    public virtual void UpdateLogic()
    {

    }
    public virtual void UpdatePhysics()
    {
        
    }
    public virtual void Exit()
    {

    }
}
