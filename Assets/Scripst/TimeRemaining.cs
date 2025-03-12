using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeRemaining : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    private bool timerActive = false;  // Variable para controlar cuando el temporizador comienza a contar

    private void Update()
    {
        // Si el temporizador está activo y el tiempo restante es mayor que cero, comienza a contar
        if (timerActive)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime < 0)
            {
                remainingTime = 0;
            }

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("Time Remaining: {0:00}:{1:00}", minutes, seconds);
        }
    }

    // Método para activar el temporizador
    public void StartTimer()
    {
        timerActive = true;
    }

    // Método para detener el temporizador, si lo necesitas
    public void StopTimer()
    {
        timerActive = false;
    }
}
