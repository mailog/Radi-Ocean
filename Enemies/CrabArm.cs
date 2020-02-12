using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabArm : MonoBehaviour
{
    private GameObject player;

    public LaserSpawner laserSpawner;

    public float shootTime;

    private float shootCounter;

    private float rotZ;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        CheckLaser();
    }

    void Rotate()
    {
        Vector2 diff = player.transform.position - transform.position;
        rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
        //transform.LookAt(player.transform, Vector2.up);
    }

    void CheckLaser()
    {
        shootCounter += Time.deltaTime;
        if(shootCounter >= shootTime)
        {
            GetComponent<Animator>().Play("Shoot");
            laserSpawner.ShootAngle(rotZ);
            shootCounter = 0;
        }
    }
}
