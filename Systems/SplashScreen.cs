using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public FadeTransition fadeScreen;

    public float initialDelay, holdTime, fadeDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeSequence()
    {
        yield return new WaitForSeconds(initialDelay);
        fadeScreen.enabled = true;
        yield return new WaitForSeconds(holdTime);
        fadeScreen.FadeOut();
        fadeScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(fadeDelay);
        SceneManager.LoadScene(1);
    }
}
