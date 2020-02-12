﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBoss : MonoBehaviour
{
    public Vector2 targetPos;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}
