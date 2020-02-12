using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    private Transform trans, playerTrans;

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = player.transform;
        trans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        trans.position = Vector2.MoveTowards(trans.position, new Vector2(playerTrans.position.x, playerTrans.position.y), moveSpeed * Time.deltaTime);
    }
}
