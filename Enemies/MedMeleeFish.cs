using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedMeleeFish : MonoBehaviour
{
    private float minX, maxX;

    public float xRange;

    private float xPos, yPos;

    public float xSpeed, ySpeed;
    
    private PredatorHealth predatorHealth;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        predatorHealth = GetComponent<PredatorHealth>();
        startTime = Time.time + Random.Range(0, xSpeed * 2);
        minX = transform.position.x - xRange;
        maxX = transform.position.x + xRange;
        xPos = transform.position.x;
        yPos = transform.position.y;    
    }

    // Update is called once per frame
    void Update()
    {
        if(!predatorHealth.GetStun())
        {
            Move();
            if(transform.position.y > 3)
            {
                GetComponent<PredatorHealth>().Delete();
            }
        }
    }

    void Move()
    {
        MoveX();
        MoveY();
        transform.position = new Vector2(xPos, yPos);
    }

    void MoveX()
    {
        xPos = Mathf.Lerp(minX, maxX, Mathf.PingPong(startTime, xSpeed) / xSpeed);
        startTime += Time.deltaTime;
        GetComponent<SpriteRenderer>().flipX = (int)startTime % 2 != 0;
    }

    void MoveY()
    {
        yPos += Time.deltaTime * ySpeed;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Game Manager"))
        {
            Destroy(gameObject); 
        }
    }
}
