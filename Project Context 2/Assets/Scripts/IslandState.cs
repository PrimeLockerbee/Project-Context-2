using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandState : MonoBehaviour
{
    public enum ObjectState
    {
        Inactive,
        Active,
        Paused
    }

    private ObjectState _currentState;

    public IslandState()
    {
        _currentState = ObjectState.Inactive;
    }

    public void ChangeState(ObjectState newState)
    {
        _currentState = newState;
    }

    public void DoAction()
    {
        switch (_currentState)
        {
            case ObjectState.Inactive:
                Debug.Log("Object is inactive, no action taken");
                break;
            case ObjectState.Active:
                Debug.Log("Object is active, performing action");
                // perform action for active state
                break;
            case ObjectState.Paused:
                Debug.Log("Object is paused, action postponed");
                // perform action for paused state
                break;
        }
    }
}
