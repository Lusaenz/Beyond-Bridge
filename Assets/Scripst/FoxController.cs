using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public float followDistance = 2f;
    public float walkDistance = 3f;
    public float moveSpeed = 2f; 
    public float jumpForce = 5f; 
    public LayerMask groundLayer; 

    private Rigidbody2D rb; 
    private bool isGrounded; 
    public Transform groundCheck; 
    public float groundCheckRadius = 0.2f; 

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        // Verifica la distancia al jugador
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > walkDistance)
        {
            animator.SetBool("Walking", true);
            MoveToPlayer();
        }
        else
        {
            animator.SetBool("Walking", false);
        }

       
        isGrounded = IsGrounded();

        // Si el jugador está saltando y el acompañante está en el suelo, hace el salto
        if (player.GetComponent<Rigidbody2D>().velocity.y > 0 && isGrounded)
        {
            animator.SetBool("IsJumping", true);
            Jump();
        }
        else if (isGrounded)
        {
            // Si está en el suelo y no está saltando, resetea la animación de salto
            animator.SetBool("IsJumping", false);
        }

        /*
        if (player.GetComponent<PlayerController>().isDead)
        {
            animator.SetTrigger("Die");
        }*/
    }

    private void MoveToPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) > followDistance)
        {
            // Moverse hacia el jugador con una velocidad constante
            Vector3 direccion = (player.position - transform.position).normalized;

            // Flip horizontal
            if (direccion.x < 0) // Si el jugador está a la izquierda
            {
                Flip(true); // Voltea el acompañante
            }
            else if (direccion.x > 0) // Si el jugador está a la derecha
            {
                Flip(false); // Vuelve a la posición original
            }

            // Mueve el acompañante con la velocidad configurada
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * moveSpeed);
        }
    }

    // Método para voltear al acompañante
    private void Flip(bool mirandoIzquierda)
    {
        Vector3 escala = transform.localScale;
        escala.x = mirandoIzquierda ? -Mathf.Abs(escala.x) : Mathf.Abs(escala.x);
        transform.localScale = escala;
    }

    // Método para verificar si el acompañante está tocando el suelo utilizando el Ground Check
    private bool IsGrounded()
    {
        // Verificamos si hay una colisión cerca del punto de Ground Check (un área circular).
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
