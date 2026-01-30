using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StableState : EntityState
{   
    
    public Player_StableState(Player player, StateMachine stateMachine, string stateName):base(player, stateMachine, stateName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        player.sr.sprite = player.stableSprite;
    }
    
}
