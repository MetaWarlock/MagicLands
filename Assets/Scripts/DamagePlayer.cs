using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = Player.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (player)
        {
            PlayerHealthController.instance.DealDamage();
        }
    }
}
