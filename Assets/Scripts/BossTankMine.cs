using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    [SerializeField] private GameObject explosion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DealDamage();

            Explode();
        }
    }

    public void Explode()
    {
        AudioManager.instance.PlaySFX(3);

        Instantiate(explosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
