using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;
    private Player player;

    public int currentHealth, maxHealth;

    public float invincibleLength;
    public float invincibleCounter;

    public bool playerIsDead;

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

    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
            if (invincibleCounter <= 0)
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
        }
    }

    public void ReceiveDamage()
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (invincibleCounter <= 0) 
        { 
            player.canJump = false;
            currentHealth--;
            AudioManager.instance.PlaySFX(9);
            invincibleCounter = invincibleLength;
            player.stateMachine.ChangeState(player.hurtState);

            if (currentHealth <= 0)
                player.stateMachine.ChangeState(player.deadState);

            UIController.instance.UpdateHealthDisplay();
        }
    }

    public void Heal()
    {
        currentHealth++;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        UIController.instance.UpdateHealthDisplay();
    }

    public void Ressurect()
    {
        playerIsDead = false;
        player.EnableUserInput();
    }
}
