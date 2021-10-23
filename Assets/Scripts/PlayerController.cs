using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public MonsterController monsterController;
    public CameraShaking cameraShaking;

    public Rigidbody2D rbPlayer;
    public Light2D lightPlayer;
    public GameObject screenGameOver;

    private bool grounded;
    private bool running = true;
    public bool danger = false;

    public float speed = 1f;
    public float jumpStrength = 100f;
    public float dangerDistance = 1f;
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
            lightPlayer.transform.position = transform.position + new Vector3(-5, 2.5f, 0);
            rbPlayer.velocity = new Vector2(speed, rbPlayer.velocity.y);
            DetectGround();
            Jumping();
        } 
        else
        {
            lightPlayer.transform.position = transform.position + new Vector3(5, 2.5f, 0);
            rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
        }

        if (!danger)
        {
            DetectMonsterProximity();
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

    void DetectMonsterProximity()
    {
        if(transform.position.x - monsterController.rbMonster.transform.position.x <= dangerDistance)
        {
            danger = true;
            lightPlayer.color = new Color32(255, 0, 0, 255);
            StartCoroutine(cameraShaking.Shake(4.5f, 0.4f));
        }
        else
        {
            lightPlayer.color = new Color32(255, 255, 255, 255);
        }
    }

    public void GameOver()
    {
        screenGameOver.SetActive(true);
    }
}
