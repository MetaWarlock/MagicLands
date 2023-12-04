using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();

        ExitAttackState();  // if in it
    }

    private void ExitAttackState()
    {
        if (player.attackBox.activeSelf)
            player.attackBox.SetActive(false);
    }

    private void PrimaryAttackTrigger()
    {
        player.attackBox.SetActive(true);
    }
}
