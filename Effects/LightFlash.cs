using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlash : MonoBehaviour
{
    public Light lightSource;

    public float flashCounter, flashTime;

    private float ogIntensity;
    public float flashIntensity;

    private bool flashing;

    // Start is called before the first frame update
    void Start()
    {
        ogIntensity = lightSource.intensity;   
    }

    // Update is called once per frame
    void Update()
    {
        CheckFlashing();
    }

    void CheckFlashing()
    {
        if(flashing)
        {
            lightSource.intensity = Mathf.Lerp(flashIntensity, ogIntensity, flashCounter / flashTime);
            flashCounter += Time.deltaTime;
            if (flashCounter / flashTime >= 1)
            {
                flashing = false;
            }
        }
    }

    public bool GetFlashing()
    {
        return flashing;
    }

    public void Flash()
    {
        flashing = true;
        lightSource.intensity = flashIntensity;
        flashCounter = 0;
    }
}
