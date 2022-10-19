using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    public Rigidbody2D theRB;
    private Vector2 moveInput;

    [SerializeField] private bool isGrounded, isAttacking = false;
    public Transform groundCheckPoint;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField]  private bool canJump;
    [SerializeField]  private bool canDoubleJump;

    public Animator anim;
    private SpriteRenderer theSR;

    [SerializeField] private float knockBackLength, knockBackForce;
    private float knockBackCounter;

    public float bounceForce;

    public bool stopInput;

    public GameObject attackBox;
    public Transform attackPoint;

    private bool m_FacingRight = true;  // For determining which way the player is currently facing.



    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PauseMenu.instance.isPaused && !stopInput) {

            if (knockBackCounter <= 0 & PlayerHealthController.instance.currentHealth > 0)
            {

                theRB.velocity = new Vector2(moveSpeed * moveInput.x, theRB.velocity.y);
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);
                anim.SetBool("isGrounded", isGrounded);
                anim.SetFloat("moveSpeed", Mathf.Abs(moveInput.x));

                if (theRB.velocity.x < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                if (theRB.velocity.x > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }

                canJump = true;

            }

        else
        {
            knockBackCounter -= Time.deltaTime;
            if (m_FacingRight) theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
            else theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);

            canJump = false;
            if (PlayerHealthController.instance.currentHealth > 0) { 
            if (knockBackCounter > 0)

                {
                    anim.SetBool("isHurt", true);
                }
                else
                {
                    anim.SetBool("isHurt", false);
                }
            } else
            {
                if (knockBackCounter > 0)
                {
                    anim.SetBool("isDead", true);
                }
                else
                {
                    PlayerHealthController.instance.PlayerDeath();
                }
            }


        }

        }

    }

    public void SetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (canJump) { 

            if (isGrounded) canDoubleJump = true;

            if (context.performed)
            {
                if (isGrounded)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    AudioManager.instance.PlaySFX(10);
                }
                else
                {
                    if (canDoubleJump)
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        canDoubleJump = false;
                        AudioManager.instance.PlaySFX(10);
                    }
                }
            }
        }

    }

    public void attackTarget(InputAction.CallbackContext context)
    {
        if (!PauseMenu.instance.isPaused && !stopInput)
        {

            if (knockBackCounter <= 0)
            {

                if (context.performed)
                {
                    if (!isAttacking)
                    { 
                    anim.SetTrigger("isAttack");
                    AudioManager.instance.PlaySFX(10);
                    attackBox.SetActive(true);
                        isAttacking = true;
                    }
                }

            }

        }
 
    }

    public void stopAttack()
    {
        attackBox.SetActive(false);
        isAttacking = false;
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        theRB.velocity = new Vector2(0f, knockBackForce);
    }

    public void Bounce()
    {
        //theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(8);
        canDoubleJump = true;    

    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
