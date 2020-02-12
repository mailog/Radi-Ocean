using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public float recoilDist;
    public float recoilCounter, recoilTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GoBack();
    }

    void GoBack()
    {
        if(recoilCounter < recoilTime)
        {
            recoilCounter += Time.deltaTime;
            float currRecoil = Mathf.Lerp(recoilDist, 0, recoilCounter / recoilTime);
            transform.localPosition = new Vector2(0, currRecoil);
        }
    }

    public void StartRecoil()
    {
        transform.localPosition = new Vector2(0, recoilDist);
        recoilCounter = 0;
    }
}
