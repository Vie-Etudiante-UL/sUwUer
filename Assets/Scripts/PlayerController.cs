using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rbPlayer;
    private bool grounded;
    private bool running = true;

    public float speed = 1f;
    public float jumpStrength = 100f;
    public float distance = 0.25f;
    public float heightDecal = -0.15f;

    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            running = !running;
        }

        if (running)
        {
            rbPlayer.velocity = new Vector2(speed, rbPlayer.velocity.y);
            DetectGround();
            Jumping();
        } 
        else
        {
            rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
        }

        //if ()
        {
            GameOver();
        }
    }

    void Jumping()
    {
        if (grounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rbPlayer.AddForce(new Vector2(0, jumpStrength));
            }
        }
    }

    void DetectGround()
    {
        Vector3 originLeftRaycast = transform.position + new Vector3(-0.15f, heightDecal, 0);
        Vector3 originRightRaycast = transform.position + new Vector3(0.15f, heightDecal, 0);
        Vector3 direction = new Vector3(0, -1, 0);

        if (Physics2D.Raycast(originLeftRaycast, direction, distance) || Physics2D.Raycast(originRightRaycast, direction, distance))
        {
            grounded = true;
            Debug.DrawRay(originLeftRaycast, direction * distance, Color.green);
            Debug.DrawRay(originRightRaycast, direction * distance, Color.green);
        }
        else
        {
            grounded = false;
            Debug.DrawRay(originLeftRaycast, direction * distance, Color.red);
            Debug.DrawRay(originRightRaycast, direction * distance, Color.red);
        }
    }

    void GameOver()
    {
        // écran game over + recommencer
    }
}
