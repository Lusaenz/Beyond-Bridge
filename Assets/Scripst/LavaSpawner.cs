using UnityEngine;

public class LavaSpawner : MonoBehaviour
{
    public GameObject lavaParticlesPrefab; 
    public Transform[] lavaPositions;

    void Start()
    {
        foreach (Transform pos in lavaPositions)
        {
            Instantiate(lavaParticlesPrefab, pos.position, Quaternion.identity);
        }
    }
}
