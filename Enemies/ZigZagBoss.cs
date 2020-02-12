using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagBoss : MonoBehaviour
{
    private float xPos, yPos;

    public float minX, maxX;

    public float minY, maxY;

    public Vector2 xSpdRange, ySpdRange;

    private float xSpeed, ySpeed;

    private float xTime, yTime;

    // Start is called before the first frame update
    void Start()
    {
        xSpeed = Random.Range(xSpdRange.x, xSpdRange.y);
        ySpeed = Random.Range(ySpdRange.x, ySpdRange.y);
        xTime = Time.time + Random.Range(0f, xSpeed * 2f);
        yTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MoveX();
        MoveY();

        transform.position = new Vector2(xPos, yPos);
    }
    void MoveX()
    {
        xPos = Mathf.Lerp(minX, maxX, Mathf.PingPong(xTime, xSpeed)/xSpeed);
        xTime += Time.deltaTime;
    }

    void MoveY()
    {
        yPos = Mathf.Lerp(minY, maxY, Mathf.PingPong(yTime, ySpeed) / ySpeed);
        yTime += Time.deltaTime;
    }
}
