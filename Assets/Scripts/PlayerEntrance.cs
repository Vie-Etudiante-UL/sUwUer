using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerEntrance : MonoBehaviour
{
    public Rigidbody2D rbPlayer;
    public Canvas canvas;

    public float speed = 1f;
    public string firstFullText;

    private string currentText = "";

    void Start()
    {
        StartCoroutine(Entrance());
    }

    IEnumerator Entrance()
    {
        while(rbPlayer.transform.position.x <= 0)
        {
            rbPlayer.velocity = new Vector2(speed, 0);

            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(2);

        while (rbPlayer.transform.position.x >= -1)
        {
            rbPlayer.velocity = new Vector2(-speed, 0);

            yield return new WaitForSeconds(0.025f);
        }

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
    }
}
