using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorSpawner : MonoBehaviour
{
    public GameObject player;

    public GameManager gameManager;

    public Vector2 xSpawn;

    public float ySpawn, yOffset;

    public List<GameObject> currEnemies;

    public float[] spawnTimes;

    private float spawnTime;

    private float spawnCounter;

    public GameObject[] newEnemies;

    // Start is called before the first frame update
    void Start()
    {
        currEnemies = new List<GameObject>();

        UpdateRound(0);
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpawn();
    }

    public void UpdateRound(int round)
    {
        if (round < spawnTimes.Length)
            spawnTime = spawnTimes[round];

        if (round < newEnemies.Length && newEnemies[round])
        {
            currEnemies.Add(newEnemies[round]);
        }
    }

    void CheckSpawn()
    {
        if (!gameManager.GetRoundDone() && player.activeSelf)
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
    }

    void Spawn()
    {
        bool underPlayer = Random.Range(0, 4) < 1;
        Vector2 spawnPos = underPlayer ? new Vector2(player.transform.position.x + Random.Range(-0.1f, 0.1f), ySpawn + Random.Range(0, yOffset)) : new Vector2(Random.Range(-0.75f, 0.75f), ySpawn + Random.Range(0, yOffset));
        Instantiate(currEnemies[(int)Random.Range(0, currEnemies.Count)], spawnPos, Quaternion.identity);
        spawnCounter = 0;
    }
}
