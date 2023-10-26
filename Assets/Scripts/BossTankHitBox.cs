using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankHitBox : MonoBehaviour
{
    public BossTankController BossTankController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Attack")
        {
            BossTankController.TakeHit();

            gameObject.SetActive(false);
        }
    }
}
