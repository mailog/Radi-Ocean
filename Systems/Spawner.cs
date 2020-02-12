using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject food;

    public Vector2 xSpawn;

    public float ySpawn;

    public float spawnTime;

    private float spawnCounter;

    private int currRound;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpawn();
     
    }

    public void SetCurrRound(int currRound)
    {
        this.currRound = currRound;
    }

    void CheckSpawn()
    {
        if (spawnCounter >= spawnTime)
        {
            Spawn();
        }
        else
        {
            spawnCounter += Time.deltaTime;
        }
    }

    void Spawn()
    {
        GameObject tmp = Instantiate(food, new Vector2(Random.Range(xSpawn.x, xSpawn.y), ySpawn), Quaternion.identity);
        tmp.GetComponent<Food>().SetParticles(5 + ((int)(currRound / 2) * 5));
        spawnCounter = 0;
    }
}
