using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseFish : MonoBehaviour
{
    private float waitCounter;

    public float waitTime;

    private Vector2 stopPos;

    public float stopY;

    public float moveSpeed1, moveSpeed2, moveSpeed3;
    
    public PredatorHealth predatorHealth;

    private bool exit;

    public float maxY;

    public GameObject bubbles;

    // Start is called before the first frame update
    void Start()
    {
        stopPos = new Vector2(transform.position.x, stopY);
    }

    // Update is called once per frame
    void Update()
    {
        if(!predatorHealth.GetStun())
        {
            Move();
        }
    }

    void Move()
    {
        if(exit)
        {
            if(waitCounter >= waitTime)
            {
                transform.Translate(moveSpeed2 * new Vector3(0, 1, 0) * Time.deltaTime, Space.World);
                if (transform.position.y >= maxY)
                {
                    GameObject predSpawner = GameObject.FindWithTag("PredatorSpawner");
                    if(predSpawner)
                        predSpawner.GetComponent<PredatorCount>().DecCount();
                    DetachBubbles();
                    Destroy(gameObject);
                }
            }
            else
            {
                waitCounter += Time.deltaTime;
                transform.Translate(moveSpeed3 * new Vector3(0, -1, 0) * Time.deltaTime, Space.World);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, stopPos, moveSpeed1 * Time.deltaTime);
            if((Vector2)transform.position == stopPos)
            {
                bubbles.SetActive(true);
                exit = true;
            }
        }
    }

    public void DetachBubbles()
    {
        bubbles.GetComponent<AutoDestroyParticles>().enabled = true;    
        bubbles.transform.SetParent(null);
    }
}
