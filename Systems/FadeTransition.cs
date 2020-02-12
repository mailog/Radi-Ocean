using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour
{
    private float fadeCounter;
    public float fadeTime;

    private bool fadeOut;

    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeOut)
        {
            fadeCounter -= Time.unscaledDeltaTime;
            img.color = Color.Lerp(Color.black, Color.clear, fadeCounter / fadeTime);
            if(fadeCounter <= 0)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else if(fadeCounter < fadeTime)
        {
            fadeCounter += Time.unscaledDeltaTime;
            img.color = Color.Lerp(Color.black, Color.clear, fadeCounter / fadeTime);
            if(fadeCounter >= fadeTime)
            {
                gameObject.SetActive(false);
            }
        }

    }

    public void FadeOut()
    {
        fadeCounter = fadeTime;
        fadeOut = true;
    }
}
