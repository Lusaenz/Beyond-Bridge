using System.Collections;
using UnityEngine;

public class BanderWinner : MonoBehaviour
{
    public Animator animator;
    public GameObject icons;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Activar"); 
        }
    }
    public void ActivarPanelYCongelarJuego()
    {
        //agregar aca activar el panel de ganar y musica de ganar
        GameManager.GameInstance.GameOverWin();
        AudioManager.Instance.musicSource.Stop();
        icons.SetActive(false);
        AudioManager.Instance.PlaySFX("WinTheme");

        Debug.Log("Ganaste el juego");
        //Time.timeScale = 0f;
    }
}
