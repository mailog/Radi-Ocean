using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float shakeRange, shakeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float zRot = Mathf.Lerp(-shakeRange, shakeRange, Mathf.PingPong(Time.time, shakeSpeed) / shakeSpeed);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRot));
    }
}
