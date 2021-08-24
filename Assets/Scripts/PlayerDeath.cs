using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public Animator playerAnimController;

    public string playerDeathTrigger = "IsDead";

    public AudioManager audioManager;


    private void Awake()
    {
        audioManager = Object.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }
    public void Die()
    {
        if(playerAnimController != null)
        {
            playerAnimController.SetTrigger(playerDeathTrigger);

        }
        if (audioManager != null)
        {
            audioManager.Play("Fall");
        }

    }
}
