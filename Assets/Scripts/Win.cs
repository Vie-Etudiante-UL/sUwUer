using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour


{
    public PlayerController playerController;
    public GameObject winScreen;


    private void OnCollisionEnter2D(Collision2D colliderDetected)
    {
        if (colliderDetected.transform.name == "Player")
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);
            playerController.GameOver();
        }
    }

}
