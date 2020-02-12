using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public Recoil recoil;

    public float damage, laserRate, laserSpeed, stunTime;

    public float defaultDamage, upgradedDamage;
    public float defaultRate, upgradedRate;
    public float defaultSpeed, upgradedSpeed;
    public float defaultStun, upgradedStun;

    public float laserShakeDuration, laserShakeAmount, laserShakeDecrease;
    private float laserCounter;

    private bool baby;

    private float currPitch;
    public float minPitch, maxPitch;

    private AudioManager audioManager;
    
    private GameManager gameManager;
    
    public GameObject bubbleBurst;

    public GameObject mutationCanvas, settingsCanvas;

    public void Awake()
    {
        UpgradeDamage(false);
        UpgradeRate(false);
        UpgradeSpeed(false);
        UpgradeStun(false);
        laserCounter = laserRate;
    }

    // Start is called before the first frame update
    void Start()
    {
        baby = gameObject.CompareTag("Baby");
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
        audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.gameOver.activeSelf)
        {
            DetectTouch();
            CheckLaser();
        }
    }
    
    public void UpgradeStun(bool upgrade)
    {
        stunTime = upgrade ? upgradedStun : defaultStun;
        laserRate = upgrade ? upgradedRate : defaultRate;
    }

    public void UpgradeDamage(bool upgrade)
    {
        damage = upgrade ? upgradedDamage : defaultDamage;
        laserSpeed = upgrade ? upgradedSpeed : defaultSpeed;
        currPitch = upgrade ? maxPitch : minPitch;
    }

    public void UpgradeRate(bool upgrade)
    {
        laserRate = upgrade ? upgradedRate : defaultRate;
    }

    public void UpgradeSpeed(bool upgrade)
    {
        laserSpeed = upgrade ? upgradedSpeed : defaultSpeed;
    }

    void DetectTouch()
    {
        if((Input.touchCount > 0 && !mutationCanvas.activeSelf && !settingsCanvas.activeSelf) || Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))// && !mutationWindow.activeSelf)
        {
            Shoot();
        }
    }

    void CheckLaser()
    {
        laserCounter += Time.deltaTime;
    }

    public void Shoot()
    {
        if (laserCounter >= laserRate)
        {
            if(recoil)
            {
                recoil.StartRecoil();
            }

            laserCounter = 0;

            GameObject tmp = ObjectPooler.laserPooler.GetPlayerLaser();

            tmp.transform.position = transform.position + new Vector3(0, -0.2f, 0);

            tmp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            tmp.GetComponent<Rigidbody2D>().velocity = tmp.transform.up * laserSpeed;
            tmp.transform.localScale = new Vector2(damage, damage);
            tmp.GetComponent<Laser>().SetDamage(damage, damage.Equals(upgradedDamage));
            tmp.GetComponent<Laser>().SetStun(stunTime, stunTime.Equals(upgradedStun));

            Instantiate(bubbleBurst, (Vector2)transform.position + new Vector2(0, -0.2f), Quaternion.identity);

            if(!baby)
            {
                gameManager.Shake(laserShakeDuration, laserShakeAmount, laserShakeDecrease);
                audioManager.PlaySound(1, currPitch);
            }
        }
    }
}
