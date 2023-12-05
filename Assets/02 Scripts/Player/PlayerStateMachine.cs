using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }
    private StateUIViewer currentStateInfoViewer;

    public void Awake()
    {
        GameObject t = GameObject.Find("Current State Info");
        if (!t)
            Debug.Log("Not Found gameObject currentStateInfoViewer");
        currentStateInfoViewer = t?.GetComponent<StateUIViewer>();
        if (!currentStateInfoViewer)
            Debug.Log("Not Found StateUIViewer currentStateInfoViewer");
    }

    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
        currentStateInfoViewer.UpdateStateUI(_newState.ToString());
    }
}
