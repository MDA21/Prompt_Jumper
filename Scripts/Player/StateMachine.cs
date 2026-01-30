using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public EntityState CurrentState { get; private set; }

    public void Initialize(EntityState startingState)
    {
        this.CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(EntityState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    // Update is called once per frame
    public void UpdateActiveState()
    {
        CurrentState.Update();
    }
}
