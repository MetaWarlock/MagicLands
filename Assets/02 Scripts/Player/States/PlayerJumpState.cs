using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Jump();
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);

        player.TrackConsecutiveJumps(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * xInput * 0.8f, rb.velocity.y);
        }

        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}