using System.Collections;
using TMPro; // Aseg�rate de tener este namespace para usar TextMeshPro
using UnityEngine;

public class InstanceObject : MonoBehaviour
{
    public GameObject objectPrefab;
    public BoxCollider2D spawnRange;   // rango de aparici�n
    public float spawnY = 5f;          // Rango en el eje Y (constante o un valor fijo)
    public float dropInterval = 0.5f;  // Tiempo entre la aparici�n de gotas
    public float spawnDuration = 30f;  // Duraci�n total de la oleada 
    public float dropSpeed = 5f;       // Velocidad a la que suben las gotas
    public GameObject objectSpawn, spawner;
    public TextMeshProUGUI countdownText; // Referencia al texto del contador de tiempo
    public Animator wallAnimator;
    private bool triggered = false;

    private void Start()
    {
        objectSpawn.SetActive(false);

        if (countdownText != null)
        {
            countdownText.text = "Time Remaining: " + spawnDuration.ToString("F0") + "s"; // Mostrar el tiempo inicial
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
        }
        else if (other.CompareTag("Friend"))
        {
            objectSpawn.SetActive(true);
            wallAnimator.SetTrigger("ActivateWall");
            StartCoroutine(SpawnObjectDrops());
        }
    }

    private IEnumerator SpawnObjectDrops()
    {
        float elapsedTime = 0f;

        // Generar gotas durante un tiempo determinado
        while (elapsedTime < spawnDuration)
        {
            SpawnObjectDrop();
            elapsedTime += dropInterval; // Incrementar el tiempo transcurrido

            // Calcular el tiempo restante y actualizar el contador
            float timeRemaining = spawnDuration - elapsedTime;
            if (timeRemaining < 0)
            {
                timeRemaining = 0; // Asegurarse de que no sea negativo
            }

            // Actualizar el texto del contador de tiempo
            if (countdownText != null)
            {
                countdownText.text = "Time Remaining: " + timeRemaining.ToString("F0") + "s";
            }

            yield return new WaitForSeconds(dropInterval); // Esperar antes de generar la siguiente gota
        }

        // Finalizar la oleada
        if (wallAnimator != null)
        {
            wallAnimator.SetTrigger("DesactivateWall"); // Aseg�rate de que el trigger est� bien configurado en el Animator
        }

        // Agregar un peque�o retraso antes de desactivar el spawner
        yield return new WaitForSeconds(0.2f);

        spawner.gameObject.SetActive(false); // Desactivar el spawner
        objectSpawn.SetActive(false); // Desactivar el objeto que genera las gotas

        // Asegurarse de que el contador diga que ha terminado
        if (countdownText != null)
        {
            countdownText.text = "Wave Completed!";
        }
    }


    private void SpawnObjectDrop()
    {
        if (objectPrefab != null && spawnRange != null)
        {
            // Generar una posici�n aleatoria dentro del BoxCollider2D
            float randomX = Random.Range(spawnRange.bounds.min.x, spawnRange.bounds.max.x);
            Vector2 spawnPosition = new Vector2(randomX, spawnY);

            // Instanciar la gota de objectPrefab en el punto de aparici�n aleatorio
            AudioManager.Instance.PlaySFX("LavaPop");
            GameObject objDrop = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Gota instanciada en: " + spawnPosition);

            // Agregarle un movimiento ascendente
            Rigidbody2D rb = objDrop.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.up * dropSpeed; // Movimiento hacia arriba
            }
            else
            {
                Debug.LogWarning("falta un Rigidbody2D.");
            }
        }
        else
        {
            Debug.LogError("Prefab del Objeto o Spawn Range no asignados.");
        }
    }
}
