using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.KnockBack();
            PlayerHealthController.instance.Die();
        }
    }
}
