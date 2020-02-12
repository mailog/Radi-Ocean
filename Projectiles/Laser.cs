using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject impact;

    private float damage;

    private float stunTime = 0.05f;

    private GameManager gameManager;

    public GameObject stunEffect, damageEffect;

    public bool stunUpgraded;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -2f)
        {
            gameManager.AddRadiation();
            gameObject.SetActive(false);
        }
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void SetStun(float stunTime, bool upgraded)
    {
        this.stunTime = stunTime;
        if(stunEffect)
        {
            stunEffect.SetActive(upgraded);
            stunUpgraded = upgraded;
        }
    }
    public bool GetUpgradedStun()
    {
        return stunUpgraded;
    }

    public float GetStun()
    {
        return stunTime;
    }

    public void SetDamage(float damage, bool upgraded)
    {
        this.damage = damage;
        if(damageEffect)    
            damageEffect.SetActive(upgraded);
    }

    public float GetDamage()
    {
        return damage;
    }
    
    public void Impact(bool soundOn = true)
    {
        Instantiate(impact, transform.position, transform.rotation);
        if(soundOn)
            GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>().PlaySound(2);
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(CompareTag("Enemy Laser"))
        {
             if(collision.gameObject.CompareTag("Player Shield") || collision.gameObject.CompareTag("Laserbeam"))
             {
                 Impact();
             }
        }
    }
}
