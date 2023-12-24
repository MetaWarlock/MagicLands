using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0f, rb.velocity.y);
        player.DisableUserInput();
        if (!PlayerHealthController.instance.playerIsDead)
        {
            PlayerHealthController.instance.currentHealth = 0;
            UIController.instance.UpdateHealthDisplay();
            AudioManager.instance.PlaySFX(8);
            LevelManager.instance.RespawnPlayer();
            PlayerHealthController.instance.playerIsDead = true;
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.EnableUserInput();
    }

    public override void Update()
    {
        base.Update();
    }
}
