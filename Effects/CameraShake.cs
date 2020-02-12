using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraShake : MonoBehaviour
{
    private Transform cameraTransform;

    public float shakeDuration, shakeAmount, decreaseFactor;
    
    private Vector3 originalPos;

    public bool shaking;

    public float maxOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (shaking && Time.timeScale != 0)
        {
            if (shakeDuration > 0f)
            {
                cameraTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, cameraTransform.localPosition.y, originalPos.z);
                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = 0f;
                cameraTransform.localPosition = originalPos;
                shaking = false;
            }
        }
    }

    /*public void CheckBoundaries()
    {
        if (cameraTransform.localPosition.x > originalPos.x + maxOffset)
        {
            cameraTransform.localPosition = new Vector2(originalPos.x + maxOffset, cameraTransform.localPosition.y);
        }

        if (cameraTransform.localPosition.x < originalPos.x - maxOffset)
        {
            cameraTransform.localPosition = new Vector2(originalPos.x - maxOffset, cameraTransform.localPosition.y);
        }

        if (cameraTransform.localPosition.y > originalPos.y + maxOffset)
        {
            cameraTransform.localPosition = new Vector2(cameraTransform.localPosition.x, originalPos.y + maxOffset);
        }

        if (cameraTransform.localPosition.y < originalPos.y - maxOffset)
        {
            cameraTransform.localPosition = new Vector2(cameraTransform.localPosition.x, originalPos.y - maxOffset);
        }
    }*/

    public void Shake(float duration = 0.1f, float amount = 0.1f, float decrease = 1f)
    {
        if (!shaking)
        {
            originalPos = transform.localPosition;
            shaking = true;
        }

        if(duration > shakeDuration)
            shakeDuration = duration;
        if(amount > shakeAmount)
            shakeAmount = amount;
        decreaseFactor = decrease;
    }
}
