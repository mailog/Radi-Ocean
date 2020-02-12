using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpParticle : MonoBehaviour
{
    public float maxRadius;
    
    public Vector2 sizeRange;

    private GameObject target;

    public float moveSpeed1, moveSpeed2, floatSpeed;

    private bool phase1;

    private Vector2 radiusPos;

    private Vector2 startPos;

    private float waitCounter, waitTime = 0.5f;

    public float rotateSpeedRange;

    private float rotateSpeed;

    public Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();

        if (phase1)
        {
            MoveRadius();
        }
        else if(target.activeSelf)
        {
            MoveToTarget();
        }
        else
        {
            transform.Translate(new Vector2(0,1) * floatSpeed * Time.deltaTime, Space.World);
        }
        if (waitCounter > 0)
        {
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotateSpeed) );
    }

    void MoveRadius()
    {
        transform.position = Vector2.MoveTowards(transform.position, radiusPos, moveSpeed1 * Time.deltaTime);
        if((Vector2)transform.position == radiusPos)
        {
            phase1 = false;
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public void SetPosition(Vector2 startPos)
    {
        this.startPos = startPos;
        transform.position = startPos;
        ResetStatus();
    }

    void ResetStatus()
    {
        GetComponent<Collider2D>().enabled = false;
        rotateSpeed = Random.Range(-rotateSpeedRange, rotateSpeedRange);
        float scale = Random.Range(sizeRange.x, sizeRange.y);
        transform.localScale = new Vector3(scale, scale,1);

        phase1 = true;

        float radiusDist = Random.Range(0, maxRadius);
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * radiusDist;
        radiusPos = startPos + randomDirection;
    }

    void MoveToTarget()
    {
        if(waitCounter <= 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed2 * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        waitCounter = waitTime;
    }
}
