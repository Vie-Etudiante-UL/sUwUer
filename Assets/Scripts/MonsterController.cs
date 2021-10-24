using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public PlayerController playerController;

    public Rigidbody2D rbMonster;

    public bool monsterIsReady = true;

    public float speed = 0.5f;
    public float distance = 0.25f;

    void Update()
    {
        if (monsterIsReady == true)
        {
            rbMonster.velocity = new Vector2(speed, rbMonster.velocity.y);
        }
        
        DetectDestructible();
        DetectPlayer();
    }

    public void DetectPlayer()
    {
        Vector3 originRaycast = transform.position + new Vector3(1.75f, 4f, 0);
        Vector3 direction = new Vector3(0, -1, 0);

        if (Physics2D.Raycast(originRaycast, direction, distance))
        {
            GameObject objectHit = Physics2D.Raycast(originRaycast, direction, distance).transform.gameObject;

            if (objectHit.tag == "Player")
            {
                playerController.GameOver();
            }
            Debug.DrawRay(originRaycast, direction * distance, Color.green);
        }
        else
        {
            Debug.DrawRay(originRaycast, direction * distance, Color.red);
        }
    }

    public void DetectDestructible()
    {
        Vector3 originRaycast = transform.position + new Vector3(2f, 4f, 0);
        Vector3 direction = new Vector3(0, -1, 0);

        if (Physics2D.Raycast(originRaycast, direction, distance))
        {
            GameObject objectHit = Physics2D.Raycast(originRaycast, direction, distance).transform.gameObject;

            if (objectHit.tag == "Destructible")
            {
                Destroy(objectHit);
            }
            Debug.DrawRay(originRaycast, direction * distance, Color.green);
        }
        else
        {
            Debug.DrawRay(originRaycast, direction * distance, Color.red);
        }
    }

    public IEnumerator ApparitionMonster()
    {
        while(rbMonster.transform.position.x <= -8)
        {
            rbMonster.velocity = new Vector2(speed, rbMonster.velocity.y);

            yield return new WaitForSeconds(0.025f);
        }
        
        yield return new WaitForSeconds(2);
    }
}