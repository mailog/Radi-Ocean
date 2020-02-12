using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutationManager : MonoBehaviour
{
    public AudioManager audioManager;

    public int sessionExp;

    public GameObject mutationCanvas;

    public int[] mutationCosts;
    
    public string[] mutationDescriptions;

    public Animator[] mutationBubbles;

    public Text[] mutationCostTexts;

    public Button[] mutationButtons;
    
    public Image[] mutationImages;

    public Animator[] mutationAnims;

    public Sprite[] mutationSprites;

    public RuntimeAnimatorController[] mutationRuntimeAnims;
    
    public GameObject confirmWindow;
    
    public Image confirmImage;

    public Animator confirmAnim;

    public GameObject confirmBubble;
    
    public Button confirmButton, denyButton;
    
    public Text confirmCost, confirmDescription;

    public Image[] equippedImgs;

    public Sprite equippedSprite, unequippedSprite;

    public WhiteFlash flash;

    private int currExp;

    public LaserController laserController;
    public Fish fish;
    public Text radCount, shopRadCount;

    public TextFlash radFlash, shopRadFlash;

    public WhiteFlash radFoodFlash, shopFoodFlash;
    
    // Start is called before the first frame update
    void Start()
    {
        sessionExp = 0;
        currExp = PlayerPrefs.GetInt("Exp", 0);
        UpdateText();

        LoadMutations();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMutations()
    {
        for(int i = 0; i < mutationCosts.Length; i++)
        {
            if(PlayerPrefs.GetInt("Mutation:" + i, 0) == 1)
            {
                LoadMutation(i, PlayerPrefs.GetInt("Equipped:"+i,0) == 1);
            }
        }
    }

    public void UpdateShop()
    {
        for (int i = 0; i < mutationCosts.Length; i++)
        {
            mutationImages[i].sprite = mutationSprites[i];
            mutationAnims[i].runtimeAnimatorController = mutationRuntimeAnims[i];
            mutationButtons[i].onClick.RemoveAllListeners();
            int tmp = i;
            if (PlayerPrefs.GetInt("Mutation:" + i, 0) == 0)
            {
                mutationCostTexts[i].text = mutationCosts[i] + "";
                mutationButtons[i].onClick.AddListener(delegate { OpenConfirmWindow(tmp); });
            }
            else
            {
                mutationBubbles[i].gameObject.SetActive(false);
                mutationCostTexts[i].text = "";
                mutationButtons[i].onClick.AddListener(delegate { MutationDescription(tmp); });
                equippedImgs[i].gameObject.SetActive(true);
            }
        }
    }

    public int GetExp()
    {
        return currExp;
    }

    public void AddExp(int addedExp)
    {
        flash.SelfFlashWhite();
        currExp += addedExp;
        if (addedExp > 0)
            sessionExp += addedExp;
        UpdateText();
        if(fish.laserBeamAdded)
        {
            fish.AddMeter(addedExp);
        }
        SaveExp();
    }

    public int GetSessionExp()
    {
        return sessionExp;
    }

    void SaveExp()
    {
        PlayerPrefs.SetInt("Exp", currExp);
        PlayerPrefs.Save();
    }

    void UpdateText()
    {
        radCount.text = sessionExp + "";
        while(radCount.text.Length < 4)
        {
            radCount.text = "0" + radCount.text;
        }
        radFlash.Flash();
        radFoodFlash.SelfFlashWhite();
        if(mutationCanvas.activeSelf)
        {
            shopRadFlash.Flash();
            shopFoodFlash.SelfFlashWhite();
        }
        shopRadCount.text = currExp + "";
        while (shopRadCount.text.Length < 4)
        {
            shopRadCount.text = "0" + shopRadCount.text;
        }
    }
    
    public void OpenConfirmWindow(int mutationIndex)
    {
        confirmWindow.SetActive(true);
        confirmImage.sprite = mutationImages[mutationIndex].sprite;
        if(!confirmButton.gameObject.activeSelf)
            confirmButton.gameObject.SetActive(true);

        confirmAnim.runtimeAnimatorController = mutationRuntimeAnims[mutationIndex];
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(delegate{Mutate(mutationIndex);});
        confirmCost.text = "-" + mutationCosts[mutationIndex];
        confirmDescription.text = mutationDescriptions[mutationIndex];

        denyButton.onClick.RemoveAllListeners();
        denyButton.onClick.AddListener(CloseConfirmWindow);

        if (!confirmBubble.activeSelf)
            confirmBubble.SetActive(true);
    }
    
    public void MutationDescription(int mutationIndex)
    {
        confirmCost.text = "";
        confirmWindow.SetActive(true);
        confirmImage.sprite = mutationImages[mutationIndex].sprite;
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(CloseConfirmWindow);
        confirmButton.onClick.AddListener(delegate { LoadMutation(mutationIndex, true); });

        denyButton.onClick.RemoveAllListeners();
        denyButton.onClick.AddListener(CloseConfirmWindow);
        denyButton.onClick.AddListener(delegate { LoadMutation(mutationIndex, false); });

        if (confirmBubble.activeSelf)
            confirmBubble.SetActive(false);
    }
    
    public void CloseConfirmWindow()
    {
        if(confirmWindow.activeSelf)
            confirmWindow.SetActive(false);
        confirmButton.onClick.RemoveAllListeners();
    }
    
    public void Mutate(int mutationIndex)
    {
        if(CheckCost(mutationCosts[mutationIndex]))
        {
            CloseConfirmWindow();
            mutationButtons[mutationIndex].onClick.RemoveAllListeners();
            mutationButtons[mutationIndex].onClick.AddListener(delegate{MutationDescription(mutationIndex);});
            PlayerPrefs.SetInt("Mutation:" + mutationIndex, 1);
            PlayerPrefs.Save();
            mutationBubbles[mutationIndex].Play("Pop");
            audioManager.PlaySound(5);
            mutationCostTexts[mutationIndex].text = "";
            equippedImgs[mutationIndex].gameObject.SetActive(true);
            LoadMutation(mutationIndex, true);
        }
        else
        {
            shopFoodFlash.SelfFlashWhite();
            shopRadFlash.Flash();
        }
    }

    public void LoadMutation(int mutationIndex, bool equipped)
    {
        int equip = equipped ? 1 : 0;
        PlayerPrefs.SetInt("Equipped:" + mutationIndex, equip);
        PlayerPrefs.Save();

        equippedImgs[mutationIndex].sprite = equipped ? equippedSprite : unequippedSprite;
        switch (mutationIndex)
        {
            case 0:
                fish.AddFollower(equipped);
            break;
            case 1:
                laserController.UpgradeStun(equipped);
                break;
            case 2:
                fish.AddShield(equipped);
                break;
            case 3:
                laserController.UpgradeDamage(equipped);
                break;
            case 4:
                fish.AddHP(equipped);
                break;
            case 5:
                fish.AddLaserBeam(equipped);
                break;
            /*case 6:
                laserController.UpgradeRate(equipped);
                break;
            case 7:
                fish.AddLaserBeam(equipped);
            break;*/
            default:
            break;
        }
    }

    public bool CheckCost(int cost)
    {
        if(cost <= currExp)
        {
            currExp -= cost;
            UpdateText();
            return true;
        }
        return false;
    }

    public void SetExp(int exp)
    {
        currExp = exp;
        SaveExp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ExpParticle"))
        {
            AddExp(1);
            collision.gameObject.SetActive(false);
        }
    }
}
