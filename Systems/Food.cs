using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public GameObject particles;

    private int numParticles = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetParticles(int numParticles)
    {
        this.numParticles = numParticles;
    }
    
    public void Eat()
    {
        ObjectPooler.laserPooler.ExpExplode(numParticles, transform.position);
        particles.transform.SetParent(null);
        ParticleSystem.EmissionModule em = particles.GetComponent<ParticleSystem>().emission;
        em.rateOverTime = 0;
        Destroy(particles, 3f);
        Destroy(gameObject);
    }
}
