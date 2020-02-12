using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public int totalCurrency;

    public Text currencyText;
    
    public Text currScoreTxt, hiScoreTxt;

    private int currScore, hiScore;

    public TextFlash currencyFlash, scoreFlash;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();

        //PlayerPrefs.SetInt("Currency", 9999);
        //PlayerPrefs.Save();

        totalCurrency = PlayerPrefs.GetInt("Currency", 0);
        UpdateCurrencyText();

        hiScore = PlayerPrefs.GetInt("HiScore", 0);
        UpdateHiScore();
    }   

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHiScore()
    {
        hiScoreTxt.text = "" + hiScore;
    }

    public void AddCurrency(int add)
    {
        totalCurrency += add;

        PlayerPrefs.SetInt("Currency", totalCurrency);
        PlayerPrefs.Save();
        UpdateCurrencyText();
        currencyFlash.Flash();
    }

    void UpdateCurrencyText()
    {
        string currencyStr = "" + totalCurrency;

        /*while(currencyStr.Length < 4)
        {
            currencyStr = "0" + currencyStr;
        }*/
        currencyText.text = "" + currencyStr;
    }
    
    public int GetTotalCurrency()
    {
        return totalCurrency;
    }
    
    public void AddScore(int add)
    {
        currScore += add;

        if (currScore > hiScore)
        {
            hiScore = currScore;
            PlayerPrefs.SetInt("HiScore", currScore);
            PlayerPrefs.Save();
            UpdateHiScore();
        }

        scoreFlash.Flash();

        UpdateScore();
    }

    void UpdateScore()
    {
        currScoreTxt.text = "" + currScore; ;
    }

    public bool EndRound()
    {
        if (currScore > hiScore)
        {
            PlayerPrefs.SetInt("HiScore", currScore);
            PlayerPrefs.Save();
            UpdateHiScore();
            return true;
        }
        return false;
    }
}
