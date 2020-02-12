using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticles : MonoBehaviour
{
    public bool autoDisable;

    public float stopCounter, stopTime = 2.5f;
    public float destroyCounter, destroyTime = 2.5f;

    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        if(autoDisable)
            ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }

            if (stopCounter >= stopTime)
            {
                ps.Stop();
            }
            stopCounter += Time.deltaTime;

            if (destroyCounter >= destroyTime)
            {
                Destroy(gameObject);
            }
            destroyCounter += Time.deltaTime;
        }
    }
}
