using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject player;

    public static ObjectPooler laserPooler;

    public List<GameObject> playerLasers, enemyLasers, expParticles;

    public GameObject playerLaser, enemyLaser, expParticle;

    private int numPlayerLasers = 20, numEnemyLasers = 20, numExpParticles = 50;

    public GameManager gameManager;

    private void Awake()
    {
        laserPooler = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerLasers();
        LoadEnemyLasers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void LoadPlayerLasers()
    {
        playerLasers = new List<GameObject>();
        for(int i = 0; i < numPlayerLasers; i++)
        {
            GameObject tmp = Instantiate(playerLaser);
            tmp.SetActive(false);
            tmp.GetComponent<Laser>().SetGameManager(gameManager);
            playerLasers.Add(tmp);
        }
    }

    public GameObject GetPlayerLaser()
    {
        for(int i = 0; i < playerLasers.Count; i++)
        {
            if(!playerLasers[i].activeSelf)
            {
                playerLasers[i].SetActive(true);
                return playerLasers[i];
            }
        }

        GameObject tmp = Instantiate(playerLaser);
        playerLasers.Add(tmp);
        tmp.GetComponent<Laser>().SetGameManager(gameManager);
        return tmp;
    }

    void LoadEnemyLasers()
    {
        enemyLasers = new List<GameObject>();
        for (int i = 0; i < numEnemyLasers; i++)
        {
            GameObject tmp = Instantiate(enemyLaser);
            tmp.SetActive(false);
            tmp.GetComponent<Laser>().SetGameManager(gameManager);
            enemyLasers.Add(tmp);
        }
    }

    public GameObject GetEnemyLaser()
    {
        for (int i = 0; i < enemyLasers.Count; i++)
        {
            if(enemyLasers[i])
            {
                if (!enemyLasers[i].activeSelf)
                {
                    enemyLasers[i].SetActive(true);
                    return enemyLasers[i];
                }
            }
        }

        GameObject tmp = Instantiate(enemyLaser);
        enemyLasers.Add(tmp);
        tmp.GetComponent<Laser>().SetGameManager(gameManager);
        return tmp;
    }

    void LoadExpParticles()
    {
        expParticles = new List<GameObject>();
        for (int i = 0; i < numExpParticles; i++)
        {
            GameObject tmp = Instantiate(expParticle);
            tmp.SetActive(false);
            tmp.GetComponent<ExpParticle>().SetTarget(player);
            expParticles.Add(tmp);
        }
    }

    public GameObject GetExpParticle()
    {
        for (int i = 0; i < expParticles.Count; i++)
        {
            if (!expParticles[i].activeSelf)
            {
                expParticles[i].SetActive(true);
                return expParticles[i];
            }
        }

        GameObject tmp = Instantiate(expParticle);
        expParticles.Add(tmp);
        tmp.GetComponent<ExpParticle>().SetTarget(player);
        return tmp;
    }

    public void ExpExplode(int numParticles, Vector2 pos)
    {
        for(int i = 0; i < numParticles; i++)
        {
            GameObject tmp = GetExpParticle();
            tmp.GetComponent<ExpParticle>().SetPosition(pos);
            tmp.SetActive(true);
        }
    }
}
