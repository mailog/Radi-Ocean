using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgm, ambientIntro;

    private bool mute;

    public Image soundButtImg, startSoundButtImg;

    public Sprite soundOnImg, soundOffImg;

    public GameObject[] sfx;

    public float fadeCounter, fadeTime;

    private bool fadeIn, fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("SoundOn", 1) == 0)
        {
            mute = true;
            soundButtImg.sprite = soundOffImg;
            startSoundButtImg.sprite = soundOffImg;
        }
        else
        {
            mute = false;
            soundButtImg.sprite = soundOnImg;
            startSoundButtImg.sprite = soundOnImg;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FadeBgm();
    }

    void FadeBgm()
    {
        if(fadeIn)
        {
            fadeCounter += Time.deltaTime;
            bgm.volume = Mathf.Lerp(0, 1, fadeCounter / fadeTime);
        }

        if(fadeOut)
        {
            fadeCounter += Time.deltaTime;
            bgm.volume = Mathf.Lerp(1, 0, fadeCounter / fadeTime);
        }
    }

    public void PlayBGM()
    {
        bgm.Play();
    }

    public void StopBgm()
    {
        bgm.Stop();
    }

    public GameObject PlaySound(int sfxIndex, float pitch = -1)
    {
        GameObject tmp = null;
        if(!mute)
        {
            tmp = Instantiate(sfx[sfxIndex], transform.position, Quaternion.identity);
            if(pitch > -1)
            {
                tmp.GetComponent<AudioSource>().pitch = pitch;
            }
        }
        return tmp;
    }

    public void ToggleMute()
    {
        mute = !mute;
        bgm.mute = mute;
        ambientIntro.mute = mute;
        if(mute)
        {
            soundButtImg.sprite = soundOffImg;
            PlayerPrefs.SetInt("SoundOn", 0);
            startSoundButtImg.sprite = soundOffImg;
        }
        else
        {
            soundButtImg.sprite = soundOnImg;
            PlayerPrefs.SetInt("SoundOn", 1);
            startSoundButtImg.sprite = soundOnImg;
        }
        PlayerPrefs.Save();
    }

    public void FadeInBgm()
    {
        bgm.Play();
        fadeIn = true;
        fadeOut = false;
        fadeCounter = 0;
    }

    public void FadeOutBgm()
    {
        fadeOut = true;
        fadeIn = false;
        fadeCounter = 0;
    }
    
    public bool GetMute()
    {
        return mute;
    }
}
