using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    public int size;
    
    //public GameObject proj;

    //public float projSpeed;

    private bool animPlay;

    public float animTime;
    
    private float shootCounter;

    public float moveSpeed;

    public float targetY;

    private Vector2 targetPos;

    private bool atTargetPos;
    
    private Animator anim;

    private PredatorHealth predatorHealth;

    public float shootDelay;

    //public Vector2 laserOffset;

    private bool shot;

    public float exitDelay;

    public float exitSpeed;

    public GameObject laserBeam;

    public float beamTime;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = new Vector2(transform.position.x, targetY);
        anim = GetComponent<Animator>();
        predatorHealth = GetComponent<PredatorHealth>();
        shootCounter = animTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!predatorHealth.GetStun())
        {
            if (shot)
            {
                if(laserBeam)
                {
                    if (laserBeam.activeSelf)
                    {
                        WaitBeam();
                    }
                    else
                    {
                        Exit();
                    }
                }
                else
                {
                    Exit();
                }
            }
            else if (atTargetPos)
            {
                Aim();
            }
            else
            {
                MoveToTargetPos();
            }
        }
    }

    void WaitBeam()
    {
        beamTime -= Time.deltaTime;
        if(beamTime <= 0)
        {
            StopLaser();
        }
    }

    void MoveToTargetPos()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if((Vector2)transform.position == targetPos)
        {
            atTargetPos = true;
        }
    }

    void Exit()
    {
        if (exitDelay < 0)
        {
            transform.Translate(new Vector3(0, -1, 0) * exitSpeed * Time.deltaTime);
            if (transform.position.y < -2.25f)
            {
                GetComponent<PredatorHealth>().Delete();
            }
        }
        else
        {
            exitDelay -= Time.deltaTime;
        }
    }

    void Aim()
    {
        shootCounter += Time.deltaTime;
        if (shootCounter >= animTime && !animPlay)
        {
            anim.Play("Shoot");
            animPlay = true;
            Shoot();
        }
        else if(shootCounter >= animTime + shootDelay)
        {
            shot = true;
        }
    }



    void Shoot()
    {
        switch(size)
        {
            case 0:
                SmallShoot();
                break;
            case 1:
                MedShoot();
                break;
            case 2:
                BigShoot();
                break;
            default:
                break;
        }
    }

    void SmallShoot()
    {
        StartCoroutine(GetComponent<LaserSpawner>().MakeLaser(0f,0f,shootDelay));
    }

    void MedShoot()
    {
        StartCoroutine(GetComponent<LaserSpawner>().MakeLaser(0f, -0.15f, shootDelay));
        StartCoroutine(GetComponent<LaserSpawner>().MakeLaser(0f, 0f, shootDelay));
        StartCoroutine(GetComponent<LaserSpawner>().MakeLaser(0f, 0.15f, shootDelay));
    }

    void BigShoot()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        laserBeam.SetActive(true);
    }

    void StopLaser()
    {
        laserBeam.GetComponent<Laserbeam>().TurnOff();
    }
}
