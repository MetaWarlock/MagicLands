using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.KnockBack();
            PlayerHealthController.instance.PlayerDeath();
        }
    }
}
