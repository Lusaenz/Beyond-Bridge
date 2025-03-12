using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBridge : MonoBehaviour
{
    public Animator bridgeAnimator;
    [SerializeField]private bool playerInZone = false;
    [SerializeField]private bool friendInZone = false;

    private string playerTag = "Player";
    private string friendTag = "Friend";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(playerTag))
        {
            playerInZone = true;
        }
        else if(other.CompareTag(friendTag)) 
        {
            friendInZone = true;
        }

        if (playerInZone && friendInZone)
        {
            ActivateBridgeAnim();
        }
    }
    private void ActivateBridgeAnim()
    {
        // Activa la animación del puente
        if (bridgeAnimator != null)
        {
            bridgeAnimator.SetTrigger("ActivateBridge");
            CameraController.Instance.MoverCam(2f, 3, 0.9f);


        }
    }

        
}
