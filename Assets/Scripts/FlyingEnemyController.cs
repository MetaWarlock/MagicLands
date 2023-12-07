using System.Runtime.CompilerServices;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    private Player player;

    public Transform[] points;
    public float moveSpeed;
    private int currentPoint;

    public SpriteRenderer theSR;

    public float distanceToAttackPlayer, chaseSpeed;

    private Vector3 attackTargetPosition;

    public float waitAfterAttack;
    private float attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance;

        for (int i=0;i<points.Length;i++)
        {
            points[i].parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        else { 
            if (Vector3.Distance(transform.position, player.transform.position) > distanceToAttackPlayer)
            {

                attackTargetPosition = Vector3.zero;

                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, points[currentPoint].position) < 0.5f)
                {
                    currentPoint++;

                    if (currentPoint >= points.Length)
                    {
                        currentPoint = 0;
                    }
                }

                FlipAnimationDirection(points[currentPoint].position.x);

            }

            else
            {
                //Attacking the Player

                if(attackTargetPosition == Vector3.zero)
                {
                    attackTargetPosition = player.transform.position;
                }

                transform.position = Vector3.MoveTowards(transform.position, attackTargetPosition, chaseSpeed * Time.deltaTime);
                FlipAnimationDirection(attackTargetPosition.x);

                if (Vector3.Distance(transform.position, attackTargetPosition) <= .1f)
                {
                    attackCounter = waitAfterAttack;
                    attackTargetPosition = Vector3.zero;
                }
            }
        }  
    }

    private void FlipAnimationDirection(float targetPositionX)
    {
        theSR.flipX = transform.position.x < targetPositionX;
    }
}
