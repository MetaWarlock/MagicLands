using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerState
{
    public PlayerHurtState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.DisableUserInput();
        player.knockBackCounter = player.knockBackLength;
        player.ToggleAttackState(false);
        rb.velocity = new Vector2(0f, player.knockBackForce);
    }

    public override void Exit()
    {
        base.Exit();

        player.EnableUserInput();
    }

    public override void Update()
    {
        base.Update();

        if (PlayerHealthController.instance.invincibleCounter <= 0)
            player.stateMachine.ChangeState(player.idleState);
    }
}
