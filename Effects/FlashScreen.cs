using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashScreen : MonoBehaviour
{
    public float flashCounter, flashTime;

    public Color startColor, endColor;

    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Flash();
    }

    void Flash()
    {
        flashCounter -= Time.unscaledDeltaTime;
        img.color = Color.Lerp(endColor, startColor, flashCounter / flashTime);
        if(flashCounter <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        flashCounter = flashTime;
        img.color = startColor;
    }

    private void OnDisable()
    {
        flashCounter = flashTime;
        img.color = startColor;
    }
}
