using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_NormalState : EntityState
{
    public Player_NormalState(Player player, StateMachine stateMachine, string stateName):base(player, stateMachine, stateName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        player.sr.sprite = player.normalSprite;
    }
}
