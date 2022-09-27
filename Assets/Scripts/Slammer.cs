using UnityEngine;

public class Slammer : MonoBehaviour
{
    public Transform theSlammer, slammerTarget;

    public float slamSpeed, waitAfterSlam, resetSpeed, distanceToAttackPlayer;
    private float attackCounter;

    private Vector3 originalPosition;

    private bool playerAttacked;


    // Start is called before the first frame update
    void Start()
    {
//        theSlammer.parent = null;
       slammerTarget.parent = null;
        originalPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        } else
        {

        if (Vector3.Distance(slammerTarget.transform.position, PlayerController.instance.transform.position) < distanceToAttackPlayer)
            {
                if (!playerAttacked) { 
                transform.position = Vector3.MoveTowards(transform.position, slammerTarget.position, slamSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, slammerTarget.position) <= .1f)
                {
                    attackCounter = waitAfterSlam;
                        playerAttacked = true;
                }
                } else
                {
                    transform.position = Vector3.MoveTowards(transform.position, originalPosition, resetSpeed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, originalPosition) <= .1f)
                    {
                        playerAttacked = false;
                    }
                }
            } else
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, resetSpeed * Time.deltaTime);
        }
        }

    }
}
