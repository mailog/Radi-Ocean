using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public StartMenu startMenu;

    public GameObject pauseMenu;

    public LaserController fishLaser, babyFishLaser;
    
    public Fish fish;

    private bool pause;

    public GameObject pauseBG;

    public GameObject restartButton, settingsButton, pauseButton, unpauseButton;

    public AudioLowPassFilter bgmLowPass;

    public float ogFreq, pauseFreq, pauseCounter, pauseTime;

    private float currFreq, startFreq, targetFreq;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FadeBGM();
    }

    void FadeBGM()
    {
        if(currFreq != targetFreq)
        {
            pauseCounter += Time.unscaledDeltaTime;
            currFreq = Mathf.Lerp(startFreq, targetFreq, pauseCounter / pauseTime);
            bgmLowPass.cutoffFrequency = currFreq;
        }
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void SetPause(bool pause)
    {
        this.pause = pause;
        PauseChanged();
    }

    public void TogglePause()
    {
        pause = !pause;
        PauseChanged();
    }

    void PauseChanged()
    {
        fishLaser.enabled = !pause;
        babyFishLaser.enabled = !pause;

        bgmLowPass.enabled = pause;
        pauseCounter = 0;

        if (pause)
        {
            startFreq = ogFreq;
            targetFreq = pauseFreq;
        }
        else
        {
            startFreq = pauseFreq;
            targetFreq = ogFreq;
        }
        currFreq = startFreq;

        fish.enabled = !pause;

        pauseButton.GetComponent<Button>().interactable = !pause;
        pauseMenu.SetActive(pause);
        //pauseBG.SetActive(pause);
        //restartButton.SetActive(pause);
        //settingsButton.SetActive(pause);

        Time.timeScale = pause ? 0f : 1f;
        /*if (pause)
        {
            //unpauseButton.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseButton.GetComponent<Button>().interactable = true;
            //unpauseButton.SetActive(false);
            Time.timeScale = 1f;
        }*/
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus && startMenu.started)
        {
            SetPause(true);
        }
    }
}
