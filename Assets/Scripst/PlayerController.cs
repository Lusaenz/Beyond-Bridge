using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    private float movementX;
    private Rigidbody2D playerRb;
    private bool isGrounded;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D playerCollider;
    public LayerMask groundLayer;


    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();

    }
    void Update()
    {
        PlayerMovement();
        isGrounded = IsGrounded(); //  suelo
        Debug.Log("isGrounded: " + isGrounded);
    }

    void PlayerMovement()
    {
        movementX = Input.GetAxis("Horizontal");
        playerRb.velocity = new Vector2(movementX * speed, playerRb.velocity.y);

        // Controlar animaciones
        bool Speed = Mathf.Abs(movementX) > 0.1f;
        animator.SetBool("Speed", Speed);

        Debug.Log("Speed: " + Speed);

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
            Debug.Log("Salto presionado");
            
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            animator.SetBool("IsJumping", true);
            AudioManager.Instance.PlaySFX("JumpSound");
        }
        else
        {
            animator.SetBool("IsJumping", !isGrounded);
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.1f; //  margen
        RaycastHit2D hit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + extraHeight, groundLayer);

        // raycast
        Color rayColor = hit.collider != null ? Color.green : Color.red;
        Debug.DrawRay(playerCollider.bounds.center, Vector2.down * (playerCollider.bounds.extents.y + extraHeight), rayColor);


        if (hit.collider != null)
        {
            Debug.Log("está tocando el suelo: " + hit.collider.gameObject.name);
        }
        else
        {
            Debug.Log(" NO está tocando el suelo");
        }

        return hit.collider != null;
    }
}
