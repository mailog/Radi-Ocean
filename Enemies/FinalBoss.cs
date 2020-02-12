using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public Vector2 targetPos;
    public float moveSpeed;

    private bool atPos;

    public Laserbeam[] leftBeams, middleBeams, rightBeams;

    public Laserbeam[] beams;

    public LaserSpawner laserSpawner;

    public int numLasers;

    public float laserDelay;

    public float laserY;

    private int pattern;

    public float minX, maxX;

    public Vector2 leftXRange, midXRange, rightXRange;

    public Vector2 randDelayRange;

    public float beamTime, beamDelay;

    public float beamY;

    public GameObject bounceProj;

    public float bounceY;

    public int bounceCount;

    public float bounceDelay;

    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!atPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            gameManager.Shake();
            if((Vector2)transform.position == targetPos)
            {
                //GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>().FadeInBgm();
                atPos = true;
                Pattern1();
            }
        }
    }

    IEnumerator UpdatePattern(float delay)
    {
        yield return new WaitForSeconds(delay);

        pattern++;
        if(pattern > 2)
        {
            pattern = 0;
        }

        switch (pattern)
        {
            case 0:
                Pattern1();
                break;
            case 1:
                Pattern2();
                break;
            case 2:
                Pattern3();
                break;
            default:
                Pattern1();
                break;
        }
    }


    void Pattern1()
    {
        Debug.Log("Pattern 1");
        float delay = 0.25f;
        for (int i = 0; i < bounceCount; i++)
        {
            delay += bounceDelay;
            StartCoroutine(SpawnBounceProj(delay));
        }
        delay += 3f;
        StartCoroutine(UpdatePattern(delay));
    }

    //Shoot lasers
    void Pattern2()
    {
        Debug.Log("Pattern 2");
        float delay = 0.25f;
        for(int i = 0; i < numLasers; i++)
        {
            delay += laserDelay;
            StartCoroutine(laserSpawner.MakeLaserAtPos(new Vector2(Random.Range(minX, maxX), laserY), delay));
        }
        delay += 1f;
        StartCoroutine(UpdatePattern(delay));
    }

    //Shoot Beams
    void Pattern3()
    {
        Debug.Log("Pattern 3");
        float delay = 0.25f;

        for(int i = 0; i < leftBeams.Length; i++)
        {
            leftBeams[i].transform.position = new Vector2(Random.Range(leftXRange.x, leftXRange.y), beamY);
            StartCoroutine(ShootBeam(leftBeams[i], delay));
            //middleBeams[i].transform.position = new Vector2(Random.Range(midXRange.x, midXRange.y), beamY);
            //StartCoroutine(ShootBeam(middleBeams[i], delay));
            rightBeams[i].transform.position = new Vector2(Random.Range(rightXRange.x, rightXRange.y), beamY);
            StartCoroutine(ShootBeam(rightBeams[i], delay));
            delay += beamDelay;
        }

        /*foreach(Laserbeam beam in beams)
        {
            delay += beamDelay;
            beam.transform.position = new Vector2(Random.Range(minX, maxX), beamY);
            StartCoroutine(ShootBeam(beam, delay));
        }*/
        delay += 1f;
        StartCoroutine(UpdatePattern(delay));
    }

    IEnumerator SpawnBounceProj(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Bouncey");
        Instantiate(bounceProj, new Vector2(Random.Range(minX, maxX), bounceY), Quaternion.identity);
        gameManager.Shake();
    }

    IEnumerator ShootBeam(Laserbeam laserbeam, float delay)
    {
        yield return new WaitForSeconds(delay);
        laserbeam.gameObject.SetActive(true);
        yield return new WaitForSeconds(beamTime);
        laserbeam.TurnOff();
    }
}
