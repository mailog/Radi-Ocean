using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PredatorHealth : MonoBehaviour
{
    public bool finalBoss;
    public bool proj;
    public GameObject zapEffect;

    public int difficulty;
    
    public Vector2 explosionOffset;
    
    private BossSpawner bossSpawner;

    public bool boss;
    
    public GameObject nuclearBurst, bubbleBurst, blood, death;

    public bool flipDeath;

    private float stunTime;
    
    public float health;
    
    private WhiteFlash selfFlash;

    private float beamCounter, beamTime;

    private PredatorCount predatorCount;

    // Start is called before the first frame update
    void Start()
    {
        selfFlash = GetComponent<WhiteFlash>();

        if(GameObject.FindWithTag("PredatorSpawner"))
        {
            predatorCount = GameObject.FindWithTag("PredatorSpawner").GetComponent<PredatorCount>();
            if (!boss)
                predatorCount.IncCount();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckStun();
    }

    public void SetBossSpawner(BossSpawner bossSpawner)
    {
        this.bossSpawner = bossSpawner;
    }

    public void AddHP(int addedHp)
    {
        health += addedHp;
    }

    public bool GetStun()
    {
        return stunTime > 0;
    }

    void CheckStun()
    {
        if (GetStun())
        {
            stunTime -= Time.deltaTime;
            if(stunTime <= 0)
            {
                if(zapEffect.activeSelf)
                    zapEffect.SetActive(false);
            }
        }
    }

    void TakeDamage(float damage)
    {
        selfFlash.AllFlashWhite();
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if(boss)
        {
            bossSpawner.BossDead();
        }

        if(GetComponent<ChaseFish>())
        {
            GetComponent<ChaseFish>().DetachBubbles();
        }

        if(GetComponent<SharkBoss>())
        {
            GetComponent<SharkBoss>().DetachBubbles();
        }

        ObjectPooler.laserPooler.ExpExplode(difficulty * 5, (Vector2)transform.position);

        Instantiate(nuclearBurst, transform.position, Quaternion.identity);
        Instantiate(bubbleBurst, transform.position, Quaternion.identity);
        Instantiate(blood, (Vector2)transform.position + explosionOffset, Quaternion.identity);

        if(death)
        {
            GameObject tmpDeath = Instantiate(death, transform.position, Quaternion.identity);
            tmpDeath.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            tmpDeath.GetComponent<SpriteRenderer>().flipY = flipDeath;
            tmpDeath.transform.localScale = transform.localScale;
            if(finalBoss)
            {
                GameObject.FindWithTag("Game Manager").GetComponent<GameManager>().Shake(3, 0.1f, 1);
            }
        }

        Delete();
    }

    public void Delete()
    {
        if(predatorCount)
        {
            if (!boss)
            {
                predatorCount.DecCount();
            }
        }

        if(proj)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().Play("Pop");
            Destroy(gameObject, 1f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void TakeLaserDamage(Laserbeam laserbeam)
    {
        if(beamCounter >= beamTime)
        {
            TakeDamage(laserbeam.GetDamage());
            beamCounter = 0;
        }
        beamCounter += Time.deltaTime;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Laser"))
        {
            if(!boss && !proj)
            {
                stunTime = collision.gameObject.GetComponent<Laser>().GetStun();
                if (collision.gameObject.GetComponent<Laser>().GetUpgradedStun())
                {
                    zapEffect.SetActive(true);
                }
            }
            collision.gameObject.GetComponent<Laser>().Impact();
            TakeDamage(collision.gameObject.GetComponent<Laser>().GetDamage());
            
        }
        else if(collision.gameObject.CompareTag("Laserbeam"))
        {
            beamTime = collision.gameObject.GetComponent<Laserbeam>().GetAttackTime();
            beamCounter = beamTime;
        }
        else if(collision.gameObject.CompareTag("Player Shield"))
        {
            TakeDamage(5);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Laserbeam"))
        {
            TakeLaserDamage(collision.gameObject.GetComponent<Laserbeam>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Laserbeam"))
        {
            beamCounter = 0;
            beamTime = 0;
        }
    }

}
