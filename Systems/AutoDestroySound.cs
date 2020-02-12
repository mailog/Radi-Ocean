using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroySound : MonoBehaviour
{
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        
        audioSource.Play();

        Destroy(gameObject, audioSource.clip.length + delay);
    }
}
