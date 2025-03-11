using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    private float movementX;
    private Rigidbody2D playerRb;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        {
            movementX = Input.GetAxis("Horizontal"); 
            playerRb.velocity = new Vector2(movementX * speed, playerRb.velocity.y);

            // Controlar animaciones
            animator.SetFloat("Speed", Mathf.Abs(movementX));

            if (movementX > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (movementX < 0)
            {
                spriteRenderer.flipX = true;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
                animator.SetBool("IsJumping", true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
