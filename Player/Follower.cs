using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform followTarget;

    public float moveSpeed;

    public float minDistance;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckSprite();
    }

    public void SetFollowTarget(Transform followTarget)
    {
        this.followTarget = followTarget;
    }

    void Move()
    {
        if(Vector2.Distance(transform.position, followTarget.position) > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(followTarget.position.x, transform.position.y), moveSpeed * Time.deltaTime);
        }
    }

    void CheckSprite()
    {
        sr.flipX = transform.position.x <= followTarget.position.x;
    }
}
