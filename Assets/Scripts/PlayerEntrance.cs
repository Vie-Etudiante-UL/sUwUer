using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerEntrance : MonoBehaviour
{
    public PlayerController playerController;
    public MonsterController monsterController;

    public Rigidbody2D rbPlayer;
    public Canvas canvas;
    public GameObject controlsWindow;
    public AudioSource terrifyingSound;
    public Animator animatorPlayer;
    public SpriteRenderer spritePlayer;

    public float speed = 1f;
    public string firstFullText;
    public string secondFullText;
    public string thirdFullText;

    private string currentText = "";

    void Start()
    {
        StartCoroutine(Entrance());
    }

    private void Update()
    {
        if(controlsWindow.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                controlsWindow.SetActive(false);
            }
        }
    }

    void DisplayControls()
    {
        controlsWindow.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("NathanScene");
        }
    }

    IEnumerator Entrance()
    {
        animatorPlayer.SetBool("isMoving", true);

        while (rbPlayer.transform.position.x <= 0)
        {
            rbPlayer.velocity = new Vector2(speed, 0);

            yield return new WaitForSeconds(0.025f);
        }

        animatorPlayer.SetBool("isMoving", false);
        yield return new WaitForSeconds(2);

        spritePlayer.flipX = true;
        playerController.lightPlayer.transform.position = transform.position + new Vector3(5, 1.5f, 0);
        animatorPlayer.SetBool("isMoving", true);

        while (rbPlayer.transform.position.x >= -1)
        {
            rbPlayer.velocity = new Vector2(-speed, 0);

            yield return new WaitForSeconds(0.025f);
        }

        animatorPlayer.SetBool("isMoving", false);
        yield return new WaitForSeconds(2);

        StartCoroutine(ProgressiveDialogue1());
    }

    IEnumerator ProgressiveDialogue1()
    {
        for (int i = 0; i <= firstFullText.Length; i++)
        {
            currentText = firstFullText.Substring(0, i);
            canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(2f);

        canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);

        StartCoroutine(ProgressiveDialogue2());
    }

    IEnumerator ProgressiveDialogue2()
    {
        for (int i = 0; i <= secondFullText.Length; i++)
        {
            currentText = secondFullText.Substring(0, i);
            canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(2f);

        canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);

        StartCoroutine(TerrifyingSound());
    }

    IEnumerator TerrifyingSound()
    {
        monsterController.gameObject.SetActive(true);
        StartCoroutine(monsterController.ApparitionMonster());
        terrifyingSound.Play();

        yield return new WaitForSeconds(1f);

        StartCoroutine(PrecipitationPlayer());
    }

    IEnumerator PrecipitationPlayer()
    {
        playerController.lightPlayer.transform.position = transform.position + new Vector3(-5, 1.5f, 0);
        spritePlayer.flipX = false;
        animatorPlayer.SetBool("isMoving", true);

        while (rbPlayer.transform.position.x <= 0)
        {
            rbPlayer.velocity = new Vector2(speed * 2, 0);

            yield return new WaitForSeconds(0.025f);
        }

        animatorPlayer.SetBool("isMoving", false);
        yield return new WaitForSeconds(1);

        StartCoroutine(ProgressiveDialogue3());
    }

    IEnumerator ProgressiveDialogue3()
    {
        for (int i = 0; i <= thirdFullText.Length; i++)
        {
            currentText = thirdFullText.Substring(0, i);
            canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(2f);

        canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);

        DisplayControls();
    }
}
