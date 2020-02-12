using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBoss : MonoBehaviour
{
    private float xPos, yPos;

    public float minX, maxX, xSpeed;

    private float shootCounter;
    public float shootTime;

    public float moveSpeed;

    public float targetY;

    private SpriteRenderer sr;

    private LaserSpawner laserSpawner;

    private Animator anim;

    private float startTime;

    private float lastX;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        laserSpawner = GetComponent<LaserSpawner>();

        xPos = transform.position.x;
        yPos = transform.position.y;
        startTime = Time.time + Random.Range(-xSpeed * 2, xSpeed * 2);
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }
    
    void Move()
    {
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
        startTime += Time.deltaTime;
        
        xPos = Mathf.Lerp(minX, maxX, Mathf.PingPong(startTime, xSpeed) / xSpeed);
        sr.flipX = xPos > lastX;
        lastX = xPos;

    }

    void Shoot()
    {
        if(shootCounter >= shootTime)
        {
            anim.Play("Shoot");
            StartCoroutine(laserSpawner.MakeLaser(0, -0.2f,0.25f));
            StartCoroutine(laserSpawner.MakeLaser(0, 0, 0.25f));
            StartCoroutine(laserSpawner.MakeLaser(0, 0.2f, 0.25f));
            shootCounter = 0;
        }
        else
        {
            shootCounter += Time.deltaTime;
        }
    }
}
