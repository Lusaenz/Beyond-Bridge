using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject PauseCanvas;

    private void Start()
    {
        Debug.Log("Iniciando escena, asegurando Time.timeScale = 1"); // Para depuración
        Time.timeScale = 1; // Asegura que el juego no inicia pausado
        if (GameManager.GameInstance != null)
        {
            GameManager.GameInstance.isPaused = false; // Asegura que el estado no quede en pausa
            GameManager.GameInstance.OnWin += Winner;
            GameManager.GameInstance.OnLose += Loser;
        }
        PauseCanvas.SetActive(false); // Ocultar menú de pausa al inicio
    }

    private void OnDestroy()
    {
        // Evitar errores al cambiar de escena
        if (GameManager.GameInstance != null)
        {
            GameManager.GameInstance.OnWin -= Winner;
            GameManager.GameInstance.OnLose -= Loser;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Tecla P Presionada");
            GameManager.GameInstance.PauseGame();
            PauseCanvas.SetActive(GameManager.GameInstance.isPaused);
        }
    }

    private void Winner()
    {
        winScreen.SetActive(true);
        PauseCanvas.SetActive(false);
    }

    private void Loser()
    {
        loseScreen.SetActive(true);
        PauseCanvas.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        GameManager.GameInstance.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        GameManager.GameInstance.ReloadScene();
        Time.timeScale = 1.0f;
    }

    public void ResumeGame()
    {
        GameManager.GameInstance.PauseGame();

    }
}
