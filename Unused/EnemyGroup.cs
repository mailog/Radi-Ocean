using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public GameObject[] enemies;

    public int[] difficulties;

    public Vector2 xSpawnRange;
    public float ySpawn, yRange;

    public int currDiff, totalDiff;

    private List<GameObject> chosenEnemies;

    // Start is called before the first frame update
    void Start()
    {
        SpawnGroup();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChooseEnemies()
    {
        chosenEnemies = new List<GameObject>();
        int chosenIndex;
        while(currDiff < totalDiff)
        {
            chosenIndex = Random.Range(0, enemies.Length);
            chosenEnemies.Add(enemies[chosenIndex]);
            currDiff += difficulties[chosenIndex];
        }
    }

    void SpawnGroup()
    {
        ChooseEnemies();
        foreach(GameObject enemy in enemies)
        {
            Instantiate(enemy, new Vector2(Random.Range(-0.75f, 0.75f), ySpawn - Random.Range(0, yRange)), Quaternion.identity);
        }
    }
}
