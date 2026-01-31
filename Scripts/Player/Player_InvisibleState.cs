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
    }
}
