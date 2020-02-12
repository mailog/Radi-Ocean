using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private Quaternion rotation;

    public float minYRot, minZRot;


    // Start is called before the first frame update
    void Start()
    {
        gyroEnabled = EnableGyro();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float UpdateRotation()
    {
        float totalRot = 0;

        if (gyroEnabled)
        {
            rotation = gyro.attitude;
            float yRot = gyro.rotationRateUnbiased.y;
            float zRot = gyro.rotationRateUnbiased.z;
            
            if (Mathf.Abs(yRot) > minYRot)
            {
                totalRot += yRot;
            }

            if (Mathf.Abs(zRot) > minZRot)
            {
                totalRot -= zRot / 2;
            }
        }

        return totalRot;
    }
    
    bool EnableGyro()
    {
        if(SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }
        return false;
    }
}
