using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBoss : MonoBehaviour
{
    private float waitCounter, shootCounter;
    public float waitTime;
    public float shootTime;

    public GameObject laserBeam;

    private bool shooting;

    public float minX, maxX, targetY, xSpeed, moveSpeed;

    private float xPos, yPos;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;
        startTime = Time.time + Random.Range(0,xSpeed * 2);

        Move();
    }

    // Update is called once per frame
    void Update()
    {
        if(shooting)
        {
            CheckShooting();
        }
        else
        {
            CheckWait();
            Move();
        }
    }
    void Move()
    {
        startTime += Time.deltaTime;
        MoveY();
        MoveX();
        transform.position = new Vector2(xPos, yPos);
    }

    void MoveY()
    {
        yPos = Mathf.MoveTowards(yPos, targetY, moveSpeed * Time.deltaTime);
    }

    void MoveX()
    {
        xPos = Mathf.Lerp(minX, maxX, Mathf.PingPong(startTime, xSpeed) / xSpeed);
    }

    void CheckWait()
    {
        if(transform.position.y == targetY)
        {
            waitCounter += Time.deltaTime;
        }
        if(waitCounter >= waitTime)
        {
            waitCounter = 0;
            ShootLaser();
        }
    }

    void CheckShooting()
    {
        shootCounter += Time.deltaTime;
        if(shootCounter >= shootTime + 0.75f)
        {
            shootCounter = 0;
            shooting = false;
        }
        else if(shootCounter >= shootTime)
        {
            StopLaser();
        }
    }
    
    public void ShootLaser()
    {
        shooting = true;
        GetComponent<Animator>().Play("Shoot");
        laserBeam.SetActive(true);
    }

    void StopLaser()
    {
        GetComponent<Animator>().Play("Closed");
        laserBeam.GetComponent<Laserbeam>().TurnOff();
    }
}
