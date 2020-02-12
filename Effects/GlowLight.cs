using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowLight : MonoBehaviour
{
    private Light lightSource;

    private LightFlash lightFlash;

    public float range;

    private float ogIntensity;

    // Start is called before the first frame update
    void Start()
    {
        lightFlash = GetComponent<LightFlash>();
        lightSource = GetComponent<Light>();
        ogIntensity = lightSource.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        Glow();
    }

    void Glow()
    {
        if(lightFlash)
        {
            if (!lightFlash.GetFlashing())
            {
                lightSource.intensity = Mathf.Lerp(ogIntensity - range, ogIntensity + range, Mathf.PingPong(Time.time, 1));
            }
        }
        else
        {
            lightSource.intensity = Mathf.Lerp(ogIntensity - range, ogIntensity + range, Mathf.PingPong(Time.time, 1));
        }
    }

    public void SetOgIntensity(float newIntensity)
    {
        ogIntensity = newIntensity;
        range = ogIntensity / 2;    
    }
}
