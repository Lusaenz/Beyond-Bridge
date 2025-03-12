using UnityEngine;

public class SoundController : MonoBehaviour
{
    public Transform player;
    public AudioSource soundSource;
    public float maxVolumeDistance = 10f;
    public float minVolumeDistance = 2f;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= maxVolumeDistance)
        {
            float normalizedDistance = Mathf.Clamp01((distanceToPlayer - minVolumeDistance) / (maxVolumeDistance - minVolumeDistance));
            soundSource.volume = 1 - normalizedDistance;
        }
        else
        {
            soundSource.volume = 0f;
        }
    }
}
