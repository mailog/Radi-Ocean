using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterFlash : MonoBehaviour
{
    public Material flashMaterial;

    public float fullCounter, fullTime, quickCounter, quickTime;

    public float ogFullTime, secondaryFullTime;

    public Color ogColor, flashColor = Color.white;

    public bool fullFlashing, quickFlashing;

    public bool inc;

    // Start is called before the first frame update
    void Start()
    {
        ogFullTime = fullTime;
        ogColor = GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        FullFlash();
    }

    void FullFlash()
    {
        if (fullFlashing)
        {
            GetComponent<Image>().color = Color.Lerp(ogColor, flashColor, fullCounter / fullTime);
            if (inc)
            {
                fullCounter += Time.deltaTime;
                if (fullCounter >= fullTime)
                {
                    inc = false;
                }
            }
            else
            {
                fullCounter -= Time.deltaTime;
                if (fullCounter <= 0)
                {
                    inc = true;
                }
            }
        }
        else if (quickFlashing)
        {
            quickCounter += Time.deltaTime;
            GetComponent<Image>().color = Color.Lerp(flashColor, ogColor, quickCounter / quickTime);
            if (quickCounter >= quickTime)
            {
                quickCounter = 0;
                quickFlashing = false;
                GetComponent<Image>().material = null;
            }
        }
        else
        {
            GetComponent<Image>().color = ogColor;
        }
    }

    public void SetFlashing(bool flash, bool secondTime = false)
    {
        fullFlashing = flash;
        if (secondTime)
        {
            fullTime = secondaryFullTime;
        }
        else
        {
            fullTime = ogFullTime;
        }
    }

    public void QuickFlash()
    {
        quickCounter = 0;
        quickFlashing = true;
        GetComponent<Image>().material = flashMaterial;
    }

    private void OnEnable()
    {
        GetComponent<Image>().color = ogColor;
        GetComponent<Image>().material = flashMaterial;
    }

    private void OnDisable()
    {
        GetComponent<Image>().color = Color.white;
        GetComponent<Image>().material = null;
    }
}
