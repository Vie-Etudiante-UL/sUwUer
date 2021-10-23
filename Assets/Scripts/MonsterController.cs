using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public PlayerController playerController;

    public Rigidbody2D rbMonster;

    public float speed = 0.5f;

    void Update()
    {
        rbMonster.velocity = new Vector2(speed, rbMonster.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D colliderDetected)
    {
        if (colliderDetected.transform.name == "Player")
        {
            playerController.GameOver();
        }

        if(colliderDetected.transform.tag == "Destructible")
        {
            Destroy(colliderDetected.gameObject);
        }
    }
}
