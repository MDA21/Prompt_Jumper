using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CrazyState : EntityState
{   
    
    public Player_CrazyState(Player player, StateMachine stateMachine, string stateName):base(player, stateMachine, stateName)
    {
        
    }
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        player.sr.sprite = player.normalSprite;
    }
}
