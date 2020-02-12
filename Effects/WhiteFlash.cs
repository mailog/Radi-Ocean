using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteFlash : MonoBehaviour
{
    private bool flashing;

    public float flashCounter, flashTime;

    public Material defaultMat, whiteMat;

    public GameObject exception;

    public bool changeColor;

    private Color ogColor;

    public Color flashColor;

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Image>())
        {
            ogColor = GetComponent<Image>().color;
        }
        else
        {
            ogColor = GetComponent<SpriteRenderer>().color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (flashing)
        {
            if (flashCounter <= 0)
            {
                if (changeColor)
                {
                    if (GetComponent<Image>() != null)
                    {
                        GetComponent<Image>().color = ogColor;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().color = ogColor;
                    }
                }
                SetAllMats(defaultMat, ogColor);
                flashing = false;
            }
            else
            {
                flashCounter -= Time.deltaTime;
            }
        }
    }

    public void AllFlashWhite()
    {
        flashCounter = flashTime;
        SetAllMats(whiteMat, flashColor);
        flashing = true;
    }

    public void SelfFlashWhite()
    {
        flashCounter = flashTime;
        SetSelfMat(whiteMat);
        flashing = true;

        if (changeColor)
        {
            if (GetComponent<Image>() != null)
            {
                GetComponent<Image>().color = flashColor;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = flashColor;
            }
        }
    }

    public void SetSelfMat(Material mat)
    {

        if (GetComponent<Image>() != null)
        {
            GetComponent<Image>().material = mat;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().material = mat;
        }
    }

    public void SetAllMats(Material mat, Color newColor)
    {
        if (GetComponent<Image>() != null)
        {
            GetComponent<Image>().material = mat;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().material = mat;
            GetComponent<SpriteRenderer>().color = newColor;
            foreach (Transform child in transform)
            {
                if (exception != child.gameObject)
                {
                    if (child.gameObject.GetComponent<SpriteRenderer>() != null)
                    {
                        child.gameObject.GetComponent<SpriteRenderer>().material = mat;
                        child.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                    }
                }
            }
        }
    }
}
