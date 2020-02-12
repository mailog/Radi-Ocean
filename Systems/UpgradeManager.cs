using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public GameObject mutateWindow;

    public int currTier, maxTier;

    public int[] tierCost;
    
    public ScoreKeeper scoreKeeper;

    public Text mutateCostText, mutateText;

    public Image mutateCostImg;

    public Button mutateButton;

    // Start is called before the first frame update
    void Start()
    {
        currTier = PlayerPrefs.GetInt("Tier", 0);
        UpdateMutateCost();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMutateWindow(bool on)
    {
        mutateWindow.SetActive(on);
    }

    public int GetTier()
    {
        return currTier;
    }

    public void Upgrade()
    {
        if (currTier < tierCost.Length - 1)
        {
            int totalCurrency = scoreKeeper.GetTotalCurrency();

            if (totalCurrency >= tierCost[currTier + 1])
            {
                scoreKeeper.AddCurrency(-1 * tierCost[currTier + 1]);

                IncTier();

                Debug.Log("UPGRADED!");
            }
        }
    }

    void UpdateMutateCost()
    {
        if (currTier >= tierCost.Length - 1)
        {
            mutateButton.interactable = false;
            mutateText.text = "YOU'RE A MONSTER!!!";        
            mutateText.color = Color.red;
            mutateCostImg.gameObject.SetActive(false);
            mutateCostText.gameObject.SetActive(false);
        }
        else
        {
            mutateCostText.text = "-" + tierCost[currTier + 1];
        }
    }

    void IncTier()
    {
        ToggleMutateWindow(true);
        currTier++;
        PlayerPrefs.SetInt("Tier", currTier);
        PlayerPrefs.Save();
        UpdateMutateCost();
    }
}
