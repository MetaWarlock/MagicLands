using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();

        if (player.attackBox.activeSelf)
            player.ToggleAttackState(false);
    }

    private void PrimaryAttackTrigger()
    {
        player.ToggleAttackState(true);
    }
}
