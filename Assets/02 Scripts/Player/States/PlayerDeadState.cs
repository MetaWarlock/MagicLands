using UnityEngine;
using System.Diagnostics;

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
            PlayerHealthController.instance.playerIsDead = true;
        }
    }

    public override void Exit()
    {
        //base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (isStateAnimationFinished)
        {
            UnityEngine.Debug.Log("Trigger called");
            LevelManager.instance.RespawnPlayer();
        }

    }
}
