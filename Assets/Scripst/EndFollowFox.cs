using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFollowFox : MonoBehaviour
{
    public GameObject textFinish;
    public Animator foxAnimator;
    [SerializeField] private bool playerInZone = false;
    [SerializeField] private bool friendInZone = false;

    private string playerTag = "Player";
    private string friendTag = "Friend";
    public FoxController foxController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInZone = true;
        }
        else if (other.CompareTag(friendTag))
        {
            friendInZone = true;
        }

        if (playerInZone && friendInZone)
        {
            textFinish.SetActive(true);
            FinishFollowFox();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Friend"))
        {
            textFinish.SetActive(false);
        }
    }
    private void FinishFollowFox()
    {
        foxController.enabled = false;
        foxAnimator.Play("byeFox");
    }


}
