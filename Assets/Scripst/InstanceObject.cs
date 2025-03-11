using System.Collections;
using UnityEngine;

public class InstanceObject : MonoBehaviour
{
    public GameObject objectPrefab; 
    public BoxCollider2D spawnRange;   // rango de aparición
    public float spawnY = 5f;          // Rango en el eje Y (constante o un valor fijo)
    public float dropInterval = 0.5f;  // Tiempo entre la aparición de gotas
    public float spawnDuration = 30f;  // Duración total de la oleada 
    public float dropSpeed = 5f;       // Velocidad a la que suben las gotas

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
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
            yield return new WaitForSeconds(dropInterval); // Esperar antes de generar la siguiente gota
        }
    }

    private void SpawnObjectDrop()
    {
        if (objectPrefab != null && spawnRange != null)
        {
            // Generar una posición aleatoria dentro del BoxCollider2D
            float randomX = Random.Range(spawnRange.bounds.min.x, spawnRange.bounds.max.x);
            Vector2 spawnPosition = new Vector2(randomX, spawnY);

            // Instanciar la gota de objectPrefab en el punto de aparición aleatorio
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

