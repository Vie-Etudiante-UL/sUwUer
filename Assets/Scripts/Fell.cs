using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fell : MonoBehaviour


{
    public PlayerController playerController;


    private void OnCollisionEnter2D(Collision2D colliderDetected)
    {
        if (colliderDetected.transform.name == "Player")
        {
            playerController.GameOver();
        }
    }

}
