using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    private Animator anim;
    private PlayerController PlayerController;

    public float bounceForce;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerController.instance.theRB.velocity = new Vector2(PlayerController.instance.theRB.velocity.x, bounceForce);
            anim.SetTrigger("Bounce");
            PlayerController.Bounce();
        }
        
    }
}
