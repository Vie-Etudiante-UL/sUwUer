using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public MonsterController monsterController;
    public CameraShaking cameraShaking;
    public Win win;
    public PauseMenuManager pauseMenuManager;

    public Rigidbody2D rbPlayer;
    public Light2D lightPlayer;
    public AudioSource ambiantSound;
    public AudioSource runSound;
    public AudioSource dangerSound;
    public AudioSource monsterSound;
    public AudioSource monsterSound2;
    public AudioSource gameOverSound;
    public Animator animatorPlayer;
    public SpriteRenderer spritePlayer;
    public GameObject menuGameOver;

    private bool grounded;
    private bool running = true;
    public bool danger = false;
    private bool detectWallRight;
    private bool gameOverLaunched = false;
    private bool playerFear = false;

    public float speed = 1f;
    public float jumpStrength = 315f;
    public float fallMultiplier = 1.5f;
    public float maxFallSpeed = -0.5f;
    public float dangerDistance = 1f;
    public float distance = 0.25f;
    public float time = 0f;

    void Update()
    {
        time += 1;
        WallRightDetection();
        FallAcceleration();
        DetectGround();

        if (Input.GetMouseButton(0))
        {
            runSound.Stop();
            spritePlayer.flipX = true;
            animatorPlayer.SetBool("isRunning", false);
            running = false;
        }
        else
        {
            spritePlayer.flipX = false;
            animatorPlayer.SetBool("isRunning", true);
            running = true;
        }

        if (running)
        {
            lightPlayer.transform.position = transform.position + new Vector3(-5, 1.5f, 0);

            if (detectWallRight == false)
            {
                rbPlayer.velocity = new Vector2(speed, rbPlayer.velocity.y);
            }

            Jumping();
        } 
        else
        {
            lightPlayer.transform.position = transform.position + new Vector3(5, 1.5f, 0);
            rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
        }

        if (!danger)
        {
            DetectMonsterProximity();
        }

        if(time == 1000)
        {
            playerFear = true;
            lightPlayer.color = new Color32(255, 0, 0, 255);
            monsterSound.Play();
            StartCoroutine(Fear());
        }

        if(time == 3000)
        {
            playerFear = true;
            lightPlayer.color = new Color32(255, 0, 0, 255);
            monsterSound2.Play();
            StartCoroutine(Fear());
        }
    }

    void Jumping()
    {
        if (grounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animatorPlayer.SetBool("isRunning", false);
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

        if (grounded)
        {
            animatorPlayer.SetBool("isRunning", true);
            animatorPlayer.SetBool("isGrounded", true);
        }
        else if (!grounded)
        {
            animatorPlayer.SetBool("isGrounded", false);
        }
    }

    void WallRightDetection()
    {
        Vector3 originFirstRightRaycast = transform.position + new Vector3(0.45f, 0.4f, 0);
        Vector3 originSecondRightRaycast = transform.position + new Vector3(0.45f, 0, 0);
        Vector3 originThirdRightRaycast = transform.position + new Vector3(0.45f, -0.4f, 0);
        Vector3 originFourthRightRaycast = transform.position + new Vector3(0.45f, -0.7f, 0);
        Vector3 originThifthRightRaycast = transform.position + new Vector3(0.45f, 0.7f, 0);
        Vector3 directionRight = new Vector3(-1, 0, 0);

        if (Physics2D.Raycast(originFirstRightRaycast, directionRight, distance) || Physics2D.Raycast(originSecondRightRaycast, directionRight, distance) || Physics2D.Raycast(originThirdRightRaycast, directionRight, distance))
        {
            detectWallRight = true;
            Debug.DrawRay(originFirstRightRaycast, directionRight * distance, Color.green);
            Debug.DrawRay(originSecondRightRaycast, directionRight * distance, Color.green);
            Debug.DrawRay(originThirdRightRaycast, directionRight * distance, Color.green);
            Debug.DrawRay(originFourthRightRaycast, directionRight * distance, Color.green);
            Debug.DrawRay(originThifthRightRaycast, directionRight * distance, Color.green);
        }
        else
        {
            detectWallRight = false;
            Debug.DrawRay(originFirstRightRaycast, directionRight * distance, Color.red);
            Debug.DrawRay(originSecondRightRaycast, directionRight * distance, Color.red);
            Debug.DrawRay(originThirdRightRaycast, directionRight * distance, Color.red);
            Debug.DrawRay(originFourthRightRaycast, directionRight * distance, Color.red);
            Debug.DrawRay(originThifthRightRaycast, directionRight * distance, Color.red);
        }
    }

    void DetectGround()
    {
        Vector3 originLeftRaycast = transform.position + new Vector3(-0.15f, -0.8f, 0);
        Vector3 originRightRaycast = transform.position + new Vector3(0.15f, -0.8f, 0);
        Vector3 direction = new Vector3(0, -1, 0);

        if (Physics2D.Raycast(originLeftRaycast, direction, distance) || Physics2D.Raycast(originRightRaycast, direction, distance))
        {
            grounded = true;
            if (!runSound.isPlaying && running == true && gameOverLaunched == false && win.victoire == false && pauseMenuManager.pause == false)
            {
                runSound.Play();
            }
            Debug.DrawRay(originLeftRaycast, direction * distance, Color.green);
            Debug.DrawRay(originRightRaycast, direction * distance, Color.green);
        }
        else
        {
            grounded = false;
            runSound.Stop();
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

            if(playerFear == false)
            {
                lightPlayer.color = new Color32(75, 139, 183, 255);
            }
        }
    }

    public void GameOver()
    {
        if (gameOverLaunched == false)
        {
            gameOverLaunched = true;
            runSound.Stop();
            ambiantSound.Stop();
            gameOverSound.Play();
            menuGameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }

    IEnumerator Fear()
    {
        yield return new WaitForSeconds(1.5f);

        lightPlayer.color = new Color32(75, 139, 183, 255);
        playerFear = false;
    }
}
