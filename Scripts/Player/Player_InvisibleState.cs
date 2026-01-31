using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InvisibleState :  EntityState
{
    
    public Player_InvisibleState(Player player, StateMachine stateMachine, string stateName):base(player, stateMachine, stateName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        player.sr.sprite = player.invisibleSprite;
        player.SetTransparency(0.5f);
        player.StartInvisibleTimer();
        SetCooldown(5f);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetTransparency(1f);
        StartCooldown();
    }
}
