using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaking : MonoBehaviour
{
    public PlayerController playerController;

    Vector3 originalCameraPos;

    private void Start()
    {
        originalCameraPos = transform.localPosition;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalCameraPos.z);

            elapsed += Time.deltaTime; 

            yield return null;
        }

        playerController.danger = false;
        transform.localPosition = originalCameraPos;
    }
}
