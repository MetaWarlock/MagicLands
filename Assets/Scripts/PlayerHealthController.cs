using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    public float invincibleLength;
    private float invincibleCounter;

    private bool playerIsDead;

    private SpriteRenderer theSR;

    private int damage;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        playerIsDead = false;
        UIController.instance.UpdateHealthDisplay();

        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
            if (invincibleCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);

            }
        }
    }

    public void DealDamage()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (invincibleCounter <= 0) { 
        //currentHealth -= 1;
        currentHealth--;
        AudioManager.instance.PlaySFX(9);

        if (currentHealth <= 0)
        {
                invincibleCounter = invincibleLength;
                PlayerController.instance.KnockBack();
                PlayerDeath();

        } else
        {
            invincibleCounter = invincibleLength;
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 0.5f);

                PlayerController.instance.stopAttack();
                PlayerController.instance.KnockBack();
        }

        UIController.instance.UpdateHealthDisplay();
        }
    }

    public void HealPlayer()
    {
        currentHealth++;
        if (currentHealth > maxHealth)
        {
            currentHealth=maxHealth;
        }
        UIController.instance.UpdateHealthDisplay();
    }

    public void Ressurect()
    {
        playerIsDead = false;
    }

    public void PlayerDeath()
    {
        if (!playerIsDead) {
            currentHealth = 0;
            UIController.instance.UpdateHealthDisplay();
            AudioManager.instance.PlaySFX(8);
            //gameObject.SetActive(false);
            PlayerController.instance.stopAttack();
            LevelManager.instance.RespawnPlayer();
            playerIsDead = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}
