using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlash : MonoBehaviour
{
    private bool isFlashing;

    private Color ogTextColor, ogOutlineColor;

    public Color flashTextColor, flashOutlineColor;

    public float flashCounter, flashTimer;

    public bool dontFlashStart;

    // Start is called before the first frame update
    void Start()
    {
        /*ogTextColor = GetComponent<Text>().color;
        if (GetComponent<Outline>())
            ogOutlineColor = GetComponent<Outline>().effectColor;*/
        if (!dontFlashStart)
            Flash();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlashing)
        {
            GetComponent<Text>().color = Color.Lerp(flashTextColor, ogTextColor, flashCounter / flashTimer);
            if(GetComponent<Outline>())
                GetComponent<Outline>().effectColor = Color.Lerp(flashOutlineColor, ogOutlineColor, flashCounter / flashTimer);
            flashCounter += Time.deltaTime;
            if(flashCounter >= flashTimer)
            {
                isFlashing = false;
            }
        }
        else
        {
            flashCounter = 0;
        }
    }

    public void Flash()
    {
        GetComponent<Text>().color = ogTextColor;
        isFlashing = true;
        flashCounter = 0;
    }

    private void OnEnable()
    {
        ogTextColor = GetComponent<Text>().color;
        if (GetComponent<Outline>())
            ogOutlineColor = GetComponent<Outline>().effectColor;
    }
}
