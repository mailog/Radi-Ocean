using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    public GameObject bubble;
    public float bubbleRespawnTime;

    public float invTime = 1;
    private bool invincible;
    
    public GameObject shield;
    
    public GameObject follower;

    private bool extraHit;

    public bool laserBeamAdded;
    
    public GameObject beamMeter;

    public float beamRechargeTime;
    private float beamRechargeCounter;

    public float beamTime;
    private float beamCounter;
    
    public GameObject laserBeam;
    public GameObject beamSound;
    
    public AudioManager audioManager;

    public LaserController laserController;

    private bool meterActive;
    public MeterFlash meterFlash;

    public WhiteFlash radMeterFlash, selfFlash;

    public Slider radMeter;

    public float maxRadMeter;

    private float currRadMeter;

    public GyroControl gyroControl;

    public GameObject eatNuclearExp;
    public LightFlash lightFlash;

    private float rotationY;
    public float maxYRot;

    public Vector2 leftPos, rightPos;
    public GameManager gameManager;

    public GameObject death;
    public GameObject bubbleBurst;

    public SpriteRenderer sr;

    private bool gyroEnabled;

    // Start is called before the first frame update
    void Start()
    {
        gyroEnabled = Application.isMobilePlatform;
        rotationY = maxYRot / 2;
        LoadMeter();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInvincible();
        if(gyroEnabled)
        {
            UpdateGyroPos();
        }
        else
        {
            UpdateMousePos();
        }
        CheckMeter();
    }

    void CheckInvincible()
    {
        if(invincible)
        {
            invTime -= Time.deltaTime;
            if(invTime <= 0)
            {
                invincible = false;
            }
        }
    }

    void LoadMeter()
    {
        radMeter.maxValue = maxRadMeter;
        radMeter.value = currRadMeter;
    }

    public void AddMeter(float meter)
    {
        if(!meterActive)
        {
            radMeterFlash.SelfFlashWhite();
            currRadMeter += meter;
            if(currRadMeter >= maxRadMeter)
            {
                ActivateMeter();
            }
            radMeter.value = currRadMeter;
        }
    }

    void ActivateMeter()
    {
        currRadMeter = 0;
        meterActive = true;
        meterFlash.enabled = true;
        laserBeam.SetActive(true);
        beamSound = audioManager.PlaySound(4);
        beamCounter = beamTime;
        radMeter.maxValue = beamTime;
        radMeter.value = beamCounter;
    }

    void DeactivateMeter()
    {
        meterActive = false;
        currRadMeter = 0;
        meterFlash.enabled = false;
        laserBeam.GetComponent<Laserbeam>().TurnOff();
        
        radMeter.value = currRadMeter;
        radMeter.maxValue = maxRadMeter;
    }

    void CheckMeter()
    {
        if(meterActive)
        {
            beamCounter -= Time.deltaTime;
            if(beamCounter <= 0)
            {
                DeactivateMeter();
            }
            radMeter.value = beamCounter;
        }
    }

    void UpdateGyroPos()
    {
        float yRot = gyroControl.UpdateRotation();

        rotationY += yRot;
        
        if(rotationY <= 0)
        {
            rotationY = 0;
        }

        if(rotationY >= maxYRot)
        {
            rotationY = maxYRot;
        }

        if(yRot > 0)
        {
            sr.flipX = true;
        }
        else if(yRot < 0)
        {
            sr.flipX = false;
        }

        transform.position = Vector2.Lerp(leftPos, rightPos, rotationY/maxYRot);
    }

    void UpdateMousePos()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float xMousePos = mousePos.x;
        if(xMousePos > rightPos.x)
        {
            xMousePos = rightPos.x;
        }
        else if(xMousePos < leftPos.x)
        {
            xMousePos = leftPos.x;
        }
        transform.position = new Vector2(xMousePos, transform.position.y);
    }

    public void AddFollower(bool equipped)
    {
        follower.SetActive(equipped);
    }

    public void AddHP(bool equipped)
    {
        bubble.SetActive(equipped);
        extraHit = equipped;
    }

    public void AddShield(bool equipped)
    {
        shield.SetActive(equipped);
    }

    public void AddLaserBeam(bool equipped)
    {
        laserBeamAdded = equipped;
        radMeter.gameObject.SetActive(equipped);
    }

    void EatFood(GameObject food)
    {
        Instantiate(bubbleBurst, transform.position, Quaternion.identity);
        Instantiate(eatNuclearExp, transform.position, Quaternion.identity);
        audioManager.PlaySound(0);
        lightFlash.Flash();
        food.GetComponent<Food>().Eat();
    }

    void HitHazard(GameObject hazard)
    {
        GetHit();
    }

    void HitLaser(GameObject laser)
    {
        laser.GetComponent<Laser>().Impact(false);
        Destroy(laser);
        GetHit();
    }

    void GetHit()
    {
        if(extraHit)
        {
            gameManager.Stall(0.25f, 0);
            bubble.GetComponent<Animator>().Play("Pop");
            audioManager.PlaySound(5);
            extraHit = false;
            invincible = true;
            invTime = 1;
            StartCoroutine(BubbleRespawn());
        }
        else if(!invincible)
        {
            shield.GetComponent<ShieldPivot>().enabled = false;
            shield.transform.SetParent(null);
            ObjectPooler.laserPooler.ExpExplode(30, transform.position);
            Instantiate(bubbleBurst, transform.position, Quaternion.identity);
            gameManager.Stall(0.5f, 0);
            gameManager.GameOver();
            GameObject tmp = Instantiate(death, transform.position, Quaternion.identity);
            tmp.GetComponent<SpriteRenderer>().sprite = sr.sprite;
            audioManager.StopBgm();
            if(beamSound != null)
            {
                beamSound.GetComponent<AudioSource>().Stop();
            }
            audioManager.PlaySound(2);
            gameObject.SetActive(false);
        }
    }

    IEnumerator BubbleRespawn()
    {
        yield return new WaitForSeconds(bubbleRespawnTime);
        //bubble.SetActive(false);
        //bubble.SetActive(true);
        bubble.GetComponent<Animator>().Play("Bubble");
        bubble.GetComponent<ExpandShrink>().ResetSize();
        extraHit = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Food"))
        {
            EatFood(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy Laser"))
        {
            HitLaser(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy Beam"))
        {
            GetHit();
        }
        else if (collision.gameObject.CompareTag("Predator") || collision.gameObject.CompareTag("Boss"))
        {
            HitHazard(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            EatFood(collision.gameObject);
        }
    }
}
