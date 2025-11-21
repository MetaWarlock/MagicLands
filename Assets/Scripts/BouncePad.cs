using UnityEngine;

public class BouncePad : MonoBehaviour
{
    private Player player;
    private Animator anim;

    public float bounceForce;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance;

        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, bounceForce);
            anim.SetTrigger("Bounce");
            player.Bounce();
        }
        
    }
}
