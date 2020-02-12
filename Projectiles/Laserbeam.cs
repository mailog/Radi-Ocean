using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laserbeam : MonoBehaviour
{
    public AudioManager audioManager;

    public LaserController laserController;

    public float attackTime, damage;

    private Animator anim;

    private float offCounter, offTime;

    private float onCounter, onTime;

    private bool beamStart, beamEnd;

    public GameManager gameManager;

    public float laserShakeDuration, laserShakeAmount, laserShakeDecrease;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(beamStart)
        {
            CheckStart();
        }
        else if(beamEnd)
        {
            CheckEnd();
        }
        else
        {
            gameManager.Shake(laserShakeDuration, laserShakeAmount, laserShakeDecrease);
        }
    }

    public float GetAttackTime()
    {
        return attackTime;
    }

    public void SetAttackSpeed(float attackTime)
    {
        this.attackTime = attackTime;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    void CheckStart()
    {
        if(onCounter >= onTime)
        {
            //if (laserController)
            //    laserController.SetCanShoot(false);
            beamStart = false;
            GetComponent<Collider2D>().enabled = true;
            onCounter = 0;
        }
        onCounter += Time.deltaTime;
    }

    void CheckEnd()
    {
        if(offCounter >= offTime)
        {
            offCounter = 0;
            //if (laserController)
            //    laserController.SetCanShoot(true);
            gameObject.SetActive(false);
        }
        offCounter += Time.deltaTime;
    }

    public void TurnOff()
    {
        GetComponent<Collider2D>().enabled = false;
        anim.Play("Beam End");
        offTime = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Debug.Log("END: " + offTime);
        beamEnd = true;
    }

    void TurnOn()
    {
        anim.Play("Beam Start");
        onTime = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Debug.Log("END: " + onTime);
        beamStart = true;
        Debug.Log(onTime);
    }

    private void OnEnable()
    {
        if(CompareTag("Enemy Beam"))
        {
            if (audioManager == null)
            {
                audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
            }
            audioManager.PlaySound(7);
        }
        GetComponent<Collider2D>().enabled = false;
        anim = GetComponent<Animator>();
        beamEnd = false;
        TurnOn();
    }
    
}
