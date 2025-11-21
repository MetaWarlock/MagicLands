using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    private Player player;
    public PlayerState currentState { get; private set; }

    public PlayerStateMachine()
    {
        player = Player.Instance;
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
        player.currentStateInfoViewer.UpdateStateUI(_newState.ToString());
        currentState.Enter();
    }
}
