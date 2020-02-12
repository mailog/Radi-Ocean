using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandShrink : MonoBehaviour
{
    public float expandCounter,expandTime;

    public Vector2 startScale, endScale;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSize();
    }

    void ChangeSize()
    {
        if(expandCounter < expandTime)
        {
            expandCounter += Time.unscaledDeltaTime;
            transform.localScale = Vector2.Lerp(startScale, endScale, expandCounter / expandTime);
        }
    }

    public void ResetSize()
    {
        expandCounter = 0;
        transform.localScale = startScale;
    }

    private void OnEnable()
    {
        ResetSize();
    }
}
