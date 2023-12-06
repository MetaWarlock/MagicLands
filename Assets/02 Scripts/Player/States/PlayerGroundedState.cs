using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.currentStateInfoViewer.UpdateStateUI("PlayerGroundedState");
        player.canJump = true;
        player.canDoubleJump = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.attackInput)
            stateMachine.ChangeState(player.primaryAttackState);

        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
