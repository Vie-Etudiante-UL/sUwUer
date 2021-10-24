using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public PlayerController playerController;

    public GameObject winScreen;
    public AudioSource ambiantSound;
    public AudioSource victorySound;

    public bool victoire = false;

    private void OnTriggerEnter2D(Collider2D colliderDetected)
    {
        if (colliderDetected.transform.name == "Player")
        {
            victoire = true;
            playerController.runSound.Stop();
            ambiantSound.Stop();
            victorySound.Play();
            Time.timeScale = 0;
            winScreen.SetActive(true);
        }
    }
}
