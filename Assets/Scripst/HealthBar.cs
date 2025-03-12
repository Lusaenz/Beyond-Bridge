using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;  //imagen de la barra de vida
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthBar();
    }

    public void Die()
    {
        //SceneManager.LoadScene("GameOver");
        Debug.Log("Player muerto");
        // pantalla de Game Over
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fireball"))
        {
            AudioManager.Instance.PlaySFX("HitSound");
            CameraController.Instance.MoverCam(1f, 2, 0.5f);
            TakeDamage(20f); // Reduce 20 de vida 
        }
        else if (other.CompareTag("Lava") || other.CompareTag("Spikes"))
        {
            AudioManager.Instance.PlaySFX("HitSound");
            TakeDamage(maxHealth); // Muerte 
        }
    }
}
