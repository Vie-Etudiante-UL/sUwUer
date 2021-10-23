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
    public AudioSource dangerSound;

    private bool grounded;
    private bool running = true;
    public bool danger = false;
    private bool detectWallRight;

    public float speed = 1f;
    public float jumpStrength = 315f;
    public float fallMultiplier = 1.05f;
    public float maxFallSpeed = -10f;
    public float dangerDistance = 1f;
    public float distance = 0.25f;

    void Update()
    {
        WallRightDetection();
        FallAcceleration();

        if (Input.GetMouseButton(0))
        {
            running = false;
        }
        else
        {
            running = true;
        }

        if (running)
        {
            lightPlayer.transform.position = transform.position + new Vector3(-5, 2.5f, 0);

            if (detectWallRight == false)
            {
                rbPlayer.velocity = new Vector2(speed, rbPlayer.velocity.y);
            }

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

    void FallAcceleration()
    {
        if (rbPlayer.velocity.y < 0)
        {

            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, Mathf.Clamp(rbPlayer.velocity.y * fallMultiplier, maxFallSpeed, 0));
        }
    }

    void WallRightDetection()
    {
        Vector3 originFirstRightRaycast = transform.position + new Vector3(0.45f, 0.4f, 0);
        Vector3 originSecondRightRaycast = transform.position + new Vector3(0.45f, 0, 0);
        Vector3 originThirdRightRaycast = transform.position + new Vector3(0.45f, -0.4f, 0);
        Vector3 directionRight = new Vector3(-1, 0, 0);

        if (Physics2D.Raycast(originFirstRightRaycast, directionRight, distance) || Physics2D.Raycast(originSecondRightRaycast, directionRight, distance) || Physics2D.Raycast(originThirdRightRaycast, directionRight, distance))
        {
            detectWallRight = true;
            Debug.DrawRay(originFirstRightRaycast, directionRight * distance, Color.green);
            Debug.DrawRay(originSecondRightRaycast, directionRight * distance, Color.green);
            Debug.DrawRay(originThirdRightRaycast, directionRight * distance, Color.green);
        }
        else
        {
            detectWallRight = false;
            Debug.DrawRay(originFirstRightRaycast, directionRight * distance, Color.red);
            Debug.DrawRay(originSecondRightRaycast, directionRight * distance, Color.red);
            Debug.DrawRay(originThirdRightRaycast, directionRight * distance, Color.red);
        }
    }

    void DetectGround()
    {
        Vector3 originLeftRaycast = transform.position + new Vector3(-0.15f, -0.4f, 0);
        Vector3 originRightRaycast = transform.position + new Vector3(0.15f, -0.4f, 0);
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
            dangerSound.Play();
            lightPlayer.color = new Color32(255, 0, 0, 255);
            StartCoroutine(cameraShaking.Shake(5f, 0.4f));
        }
        else
        {
            dangerSound.Stop();
            lightPlayer.color = new Color32(255, 255, 255, 255);
        }
    }

    public void GameOver()
    {
        screenGameOver.SetActive(true);
    }
}
