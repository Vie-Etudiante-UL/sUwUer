using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public PlayerController playerController;

    public Rigidbody2D rbMonster;

    private bool monsterIsReady = false;

    public float speed = 0.5f;
    public float distance = 0.25f;

    void Update()
    {
        if (monsterIsReady)
        {
            rbMonster.velocity = new Vector2(speed, rbMonster.velocity.y);
        }
        
        DetectDestructible();
    }

    private void OnCollisionEnter2D(Collision2D colliderDetected)
    {
        if (colliderDetected.transform.name == "Player")
        {
            playerController.GameOver();
        }
    }

    public void DetectDestructible()
    {
        Vector3 originRaycast = transform.position + new Vector3(2.75f, 4f, 0);
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