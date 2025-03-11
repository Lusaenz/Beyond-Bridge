using UnityEngine;

public class PlayerMovPrev : MonoBehaviour
{
    public float moveSpeed = 5f;        // Velocidad de movimiento
    public float jumpForce = 7f;        // Fuerza del salto
    public Transform groundCheck;       // Transform para comprobar si el jugador está tocando el suelo
    public LayerMask groundLayer;       // Capa para el suelo

    private Rigidbody2D rb;             // Componente Rigidbody2D
    private bool isGrounded;            // Para verificar si el jugador está en el suelo
    private float groundRadius = 0.2f;  // Radio para la comprobación de contacto con el suelo

    void Start()
    {
        // Obtener el Rigidbody2D del jugador
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Comprobar si está en el suelo usando un raycast
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // Movimiento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Saltar
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    // Dibujar un círculo de comprobación de suelo en el editor (opcional)
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}
