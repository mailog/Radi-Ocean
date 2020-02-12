using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public AudioManager audioManager;

    public Fish fish;

    public PredatorSpawner predSpawner;

    public GameManager gameManager;

    private int numBosses;

    public GameObject[] bosses;

    public int[] round1, round2, round3, round4, round5, round6, round7, round8, round9, round10;

    public GameObject finalBoss;

    private float spawnTime = 1.5f;

    private int currRound;

    private bool bossDead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChooseBosses(int roundNum)
    {
        currRound = roundNum;
        bossDead = false;
        switch (roundNum)
        {
            case 0:
                SpawnBosses(round1);
                break;
            case 1:
                SpawnBosses(round2);
                break;
            case 2:
                SpawnBosses(round3);
                break;
            case 3:
                SpawnBosses(round4);
                break;
            case 4:
                SpawnBosses(round5);
                break;
            case 5:
                SpawnBosses(round6);
                break;
            case 6:
                SpawnBosses(round7);
                break;
            case 7:
                SpawnBosses(round8);
                break;
            case 8:
                SpawnBosses(round9);
                break;
            case 9:
                SpawnBosses(round10);
                break;
            default:
                SpawnBosses(round1);
                break;
        }
    }

    void SpawnBosses(int[] roundBosses)
    {
        numBosses = roundBosses.Length;
        for(int i = 0; i < roundBosses.Length; i++)
        { 
            StartCoroutine(SpawnBoss(bosses[roundBosses[i]], spawnTime * i));
        }
    }

    IEnumerator SpawnBoss(GameObject boss, float pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);
        GameObject tmp = Instantiate(boss, boss.transform.position, Quaternion.identity);
        tmp.GetComponent<PredatorHealth>().SetBossSpawner(GetComponent<BossSpawner>());
        tmp.GetComponent<PredatorHealth>().AddHP(currRound * 5);
        gameManager.AddRadiation();
    }

    public int GetNumBosses()
    {
        return numBosses;
    }

    public void BossDead()
    {
        numBosses--;
        if (numBosses <= 0 && !bossDead)
        {
            bossDead = true;
            if (currRound == 9)
            {
                //audioManager.FadeOutBgm();
                gameManager.FinalRound();
                numBosses = 1;
                currRound++;
                StartCoroutine(SpawnBoss(finalBoss, 1f));
                currRound = 10;
            }
            else if(currRound >= 10)
            {
                gameManager.GameOver();
                enabled = false;
            }
            else
            {
                gameManager.IncRound();
                predSpawner.enabled = true;
            }
        }
    }
}
