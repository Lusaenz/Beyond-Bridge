using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public float followDistance = 2f;
    public float walkDistance = 3f;
    public float moveSpeed = 2f; // Velocidad de movimiento
    public float jumpForce = 5f; // Fuerza de salto
    public LayerMask groundLayer; // Capa de suelo para verificar si el acompañante está tocando el suelo

    private Rigidbody2D rb; // Referencia al Rigidbody2D del acompañante
    private bool isGrounded; // Verifica si el acompañante está en el suelo

    void Start()
    {
        // Obtener el Rigidbody2D del acompañante
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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

        // Lógica de salto
        isGrounded = IsGrounded();

        // Detectar si el acompañante debe saltar
        if (player.GetComponent<Rigidbody2D>().velocity.y > 0 && isGrounded)
        {
            animator.SetBool("IsJumping", true);
            Jump();
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }

        /*// Lógica de muerte (dependerá de cómo implementes la muerte)
        if (player.GetComponent<PlayerController>().isDead)
        {
            animator.SetTrigger("Die");
        }*/
    }

    // Método para mover al acompañante hacia el jugador
    private void MoveToPlayer()
    {
        // Solo se mueve si la distancia es mayor que la distancia de seguimiento
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
        // Cambiar la escala del acompañante en el eje X
        Vector3 escala = transform.localScale;
        escala.x = mirandoIzquierda ? -Mathf.Abs(escala.x) : Mathf.Abs(escala.x);
        transform.localScale = escala;
    }

    // Método para verificar si el acompañante está tocando el suelo
    private bool IsGrounded()
    {
        // Usamos un "Raycast" hacia abajo para comprobar si el acompañante está en el suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }

    // Método para hacer que el acompañante salte
    private void Jump()
    {
        if (isGrounded)
        {
            // Aplicar la fuerza de salto hacia arriba
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
