using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context
{
    private State currentState;

    public Context()
    {

    }

    public void SetState(State state)
    {
        if(currentState != null)
        {
            currentState = null;
        }
        currentState = state;
        currentState.Begin(this);
    }

    public void ClearCurrentState()
    {
        currentState = null;
    }
}
