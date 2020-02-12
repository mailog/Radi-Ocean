using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinShop : MonoBehaviour
{
    public SkinManager skinManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        skinManager.CheckUnlocked();
    }
}
