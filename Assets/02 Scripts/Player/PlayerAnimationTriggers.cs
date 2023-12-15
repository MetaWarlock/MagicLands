using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationFinishTrigger()
    {
        player.AnimationFinishTrigger();

        if (player.attackBox.activeSelf)
            player.ToggleAttackState(false);
    }

    private void PrimaryAttackTrigger()
    {
        player.ToggleAttackState(true);
    }
}
