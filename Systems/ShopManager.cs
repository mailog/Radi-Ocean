using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public SkinManager skinManager;

    public GameObject mutationShop, skinShop;

    public Button mutationButton, skinButton;
    public Image mutationImg, skinImg;

    public Sprite mutationOn, mutationOff, skinOn, skinOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToMutation()
    {
        mutationButton.interactable = false;
        mutationImg.sprite = mutationOn;
        skinButton.interactable = true;
        skinImg.sprite = skinOff;
        mutationShop.SetActive(true);
        skinShop.SetActive(false);
    }

    public void SwitchToSkin()
    {
        mutationButton.interactable = true;
        mutationImg.sprite = mutationOff;
        skinButton.interactable = false;
        skinImg.sprite = skinOn;
        mutationShop.SetActive(false);
        skinShop.SetActive(true);
    }

    private void OnEnable()
    {
        skinManager.CheckUnlocked();
    }
}
