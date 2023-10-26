using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    public enum bossStates
    {
        shooting, hurt, moving, ended
    }
    [SerializeField] private bossStates currentState;

    [SerializeField] private Transform theBoss;
    [SerializeField] private Animator anim;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform leftPoint, rightPoint;
    private bool moveRight = false;

    [Header("Mines")]
    [SerializeField] private GameObject mine;
    [SerializeField] private Transform minePoint;
    [SerializeField] private float timeBetweenMines;
    private float mineCounter;


    [Header("Shooting")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float timeBetweenShots;
    private float shotCounter;

    [Header("Hurt")]
    [SerializeField] private float hurtTime;
    private float hurtCounter;
    [SerializeField] private GameObject hitBox;

    [Header("Health")]
    [SerializeField] private int health = 5;
    [SerializeField] private GameObject explosion, winPlatform;
    private bool isDefeated;
    [SerializeField] private float shotSpeedUp, mineSpeedUp;




    private void Start()
    {
        currentState = bossStates.shooting;
        shotCounter = 0f;
    }

    private void Update()
    {
        switch (currentState)
        {
            case bossStates.shooting:

                shotCounter -= Time.deltaTime;

                if(shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;

                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = theBoss.localScale;
                }

                break;

            case bossStates.hurt:
                if (hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;

                    if (hurtCounter <= 0)
                    {
                        currentState = bossStates.moving;

                        mineCounter = 0f;

                        if (isDefeated)
                        {
                            theBoss.gameObject.SetActive(false);
                            Instantiate(explosion, theBoss.position, theBoss.rotation);

                            winPlatform.SetActive(true);

                            AudioManager.instance.StopBossMusic();

                            currentState = bossStates.ended;
                        }
                    }
                }

                break;
            case bossStates.moving:

                if(moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(1f,1f,1f);

                        moveRight = false;

                        EndMovement();
                    }
                } else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);

                        moveRight = true;

                        EndMovement();
                    }
                }

                mineCounter -= Time.deltaTime;

                if (mineCounter <= 0)
                {
                    mineCounter = timeBetweenMines;

                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }

                break;
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeHit();
        }
#endif
    }

    public void TakeHit()
    {
        currentState = bossStates.hurt;
        hurtCounter = hurtTime;

        anim.SetTrigger("Hit");

        AudioManager.instance.PlaySFX(0);

        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();

        if (mines.Length > 0)
        {
            foreach (BossTankMine m in mines)
            {
                m.Explode();
            }
        }

        health--;

        if (health <= 0)
        {
            isDefeated = true;
        } else
        {
            timeBetweenShots -= shotSpeedUp;
            timeBetweenMines -= mineSpeedUp;
        }
    }

    private void EndMovement()
    {
        currentState = bossStates.shooting;

        shotCounter = 0f;

        anim.SetTrigger("StopMoving");

        hitBox.SetActive(true);
    }

}
