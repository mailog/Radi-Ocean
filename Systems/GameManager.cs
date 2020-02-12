using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public Text roundText;

    public TextFlash roundFlash;

    private float waitCounter, waitTime = 1f;

    public MutationManager mutationManager;

    public GameObject screenFlash;

    private bool started;

    public AudioManager audioManager;

    public BossSpawner bossSpawner;

    public GameObject nuclearExplosion;

    private float radShakeDuration = 0.1f, radShakeDecrease = 1, radShakeAmount = 0.05f; 

    public CameraShake cameraShake;

    public WhiteFlash radMeterFlash;

    public LightFlash lightFlash;

    public GameObject gameOver;

    private float stallTime, stallScale;

    private bool stalling;

    private int roundNum;

    public float roundCounter, roundTime;

    public PredatorCount predCount;

    private bool roundDone;

    public PredatorSpawner predSpawner;

    public Spawner foodSpawner;

    public int numBosses = 1;

    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (stallTime > 0 && !stalling)
        {
            StartCoroutine(DoStall());
        }

        CheckRound();
    }

    public void AddExp(int exp)
    {
        mutationManager.AddExp(exp);
    }

    public void Flash()
    {
        screenFlash.SetActive(true);
    }

    public void StartGame()
    {
        started = true;
    }

    public bool GetRoundDone()
    {
        return roundDone;
    }

    void CheckRound()
    {
        if(bossSpawner.GetNumBosses() <= 0 && started && player.activeSelf && !gameOver.activeSelf)
        {
            roundCounter += Time.deltaTime;
            if(roundCounter >= roundTime)
            {
                roundDone = true;

                if (predCount.GetCount() <= 0)
                {
                    if (waitCounter <= 0)
                    {
                        waitCounter = waitTime;
                        bossSpawner.ChooseBosses(roundNum);
                    }
                    else
                    {
                        waitCounter -= Time.deltaTime;
                    }
                }
            }
        }
    }

    public void IncRound()
    {
        roundDone = false;
        roundNum++;
        roundTime += 5;
        roundCounter = 0;
        predSpawner.UpdateRound(roundNum);
        foodSpawner.SetCurrRound(roundNum);
        roundText.text = "ROUND " + (roundNum+1) + "/10";
        roundFlash.Flash();
    }

    public void FinalRound()
    {
        roundText.text = "FINAL BOSS";
        roundFlash.Flash();
        roundNum = 10;
    }

    public void Shake(float duration = 0.1f, float amount = 0.1f, float decrease = 1f)
    {
        cameraShake.Shake(duration, amount, decrease);
    }

    public void AddRadiation()
    {
        audioManager.PlaySound(3);
        lightFlash.Flash();
        Instantiate(nuclearExplosion, nuclearExplosion.transform.position, Quaternion.identity);
        Shake(radShakeDuration, radShakeAmount, radShakeDecrease);
    }

    public void Stall(float seconds = 0.025f, float scale = 0)
    {
        stallScale = scale;
        stallTime = seconds;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }

    IEnumerator DoStall()
    {
        stalling = true;
        Time.timeScale = stallScale;

        yield return new WaitForSecondsRealtime(stallTime);
        
        Time.timeScale = 1f;
        stallTime = 0;
        stalling = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Laser") || collision.gameObject.CompareTag("Enemy Laser"))
        {
            collision.gameObject.SetActive(false);
        }
        else if(collision.gameObject.CompareTag("Food") || collision.gameObject.CompareTag("Death"))    
        {
            AddRadiation();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Predator"))
        {
            Destroy(collision.gameObject);
        }
    }
}
