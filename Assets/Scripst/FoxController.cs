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

        // Si el jugador est� saltando y el acompa�ante est� en el suelo, hace el salto
        if (player.GetComponent<Rigidbody2D>().velocity.y > 0 && isGrounded)
        {
            animator.SetBool("IsJumping", true);
            Jump();
        }
        else if (isGrounded)
        {
            // Si est� en el suelo y no est� saltando, resetea la animaci�n de salto
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
            if (direccion.x < 0) // Si el jugador est� a la izquierda
            {
                Flip(true); // Voltea el acompa�ante
            }
            else if (direccion.x > 0) // Si el jugador est� a la derecha
            {
                Flip(false); // Vuelve a la posici�n original
            }

            // Mueve el acompa�ante con la velocidad configurada
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * moveSpeed);
        }
    }

    // M�todo para voltear al acompa�ante
    private void Flip(bool mirandoIzquierda)
    {
        Vector3 escala = transform.localScale;
        escala.x = mirandoIzquierda ? -Mathf.Abs(escala.x) : Mathf.Abs(escala.x);
        transform.localScale = escala;
    }

    // M�todo para verificar si el acompa�ante est� tocando el suelo utilizando el Ground Check
    private bool IsGrounded()
    {
        // Verificamos si hay una colisi�n cerca del punto de Ground Check (un �rea circular).
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
