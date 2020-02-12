using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public bool started;

    public AudioSource ambientNoise;

    public Button startButton;

    public AudioManager audioManager;

    public FadeTransition fade;
    
    public Button shopButton, settingsButton;

    public ExpandShrink shopSize, settingsSize, audioSize;

    public GameObject title;

    public GameObject[] spawners;

    public GameObject creditsMenu, shopMenu, settings, homeCanvas;

    //public GameObject instructions

    public MoveDeactivate meter, rotateControls, tapControls, pauseButton, roundCount, beamMeter;

    public GameManager gameManager;

    public LaserController playerLaser, followerLaser;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(StartFade());
    }

    IEnumerator StartFade()
    {
        yield return new WaitForSeconds(0.5f);
        fade.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        started = true;
        ambientNoise.Stop();
        playerLaser.enabled = true;
        followerLaser.enabled = true;
        if(beamMeter.gameObject.activeSelf)
        {
            beamMeter.enabled = true;
        }
        roundCount.enabled = true;
        shopButton.interactable = false;
        shopSize.enabled = true;
        settingsButton.interactable = false;
        settingsSize.enabled = true;

        audioSize.enabled = true;
        
        pauseButton.GetComponent<MoveDeactivate>().enabled = true;

        foreach (GameObject spawner in spawners)
        {
            spawner.SetActive(true);
        }
        title.GetComponent<Hover>().enabled = false;
        title.GetComponent<MoveDeactivate>().enabled = true;

        rotateControls.enabled = true;
        tapControls.enabled = true;
        meter.enabled = true;

        Debug.Log("Start Game");

        audioManager.PlayBGM();

        startButton.gameObject.SetActive(false);

        gameManager.StartGame();
    }

    public void ToggleCredits()
    {
        creditsMenu.SetActive(!creditsMenu.activeSelf);
    }

    public void ToggleShop()
    {
        shopMenu.SetActive(!shopMenu.activeSelf);
    }
    
    public void ToggleSettings()
    {
        settings.SetActive(!settings.activeSelf);
    }

    public void ToggleHomeCanvas()
    {
        homeCanvas.SetActive(!homeCanvas.activeSelf);
    }

    public void RestartGame()
    {
        fade.gameObject.SetActive(true);
        fade.FadeOut();
    }

    public void ResetGame(bool deleteData)
    {
        if(deleteData)
            PlayerPrefs.DeleteAll();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
