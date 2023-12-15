using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;
    private Player player;

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
        player = Player.Instance;

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
            currentHealth = maxHealth;

        if (invincibleCounter <= 0) 
        { 
            //currentHealth -= 1;
            currentHealth--;
            AudioManager.instance.PlaySFX(9);

            if (currentHealth <= 0)
            {
                invincibleCounter = invincibleLength;
                player.KnockBack();
                PlayerDeath();

            } 
            else
            {
                invincibleCounter = invincibleLength;
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 0.5f);

                player.ToggleAttackState(false);
                player.KnockBack();
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
            player.anim.SetBool("isDead", true);
            currentHealth = 0;
            UIController.instance.UpdateHealthDisplay();
            AudioManager.instance.PlaySFX(8);
            //gameObject.SetActive(false);
            player.ToggleAttackState(false);
            LevelManager.instance.RespawnPlayer();
            playerIsDead = true;
        }
    }
}
