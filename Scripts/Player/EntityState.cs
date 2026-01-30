using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;

    protected Rigidbody2D rb;

    protected PlayerInputSet input;
    
    protected string stateName;
    // Start is called before the first frame update
    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
        
        input = player.input;
        rb = player.rb;
    }

    public virtual void Enter()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void Exit()
    {
        
    }
    
    

    public void Jump()
    {
        if (player.groundDetected)
        {
            player.SetVelocity(rb.velocity.x, player.jumpForce);
        }
    }
}
