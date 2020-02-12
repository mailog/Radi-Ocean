using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public string adUnitId;

    private RewardedAd rewardAd;

    private bool requesting;

    public GameOver gameOver;

    private void Awake()
    {
        MobileAds.Initialize(initStatus => { });
    }

    // Start is called before the first frame update
    void Start()
    {
        this.rewardAd = new RewardedAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        // Called when an ad request has successfully loaded.
        this.rewardAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardAd.OnAdClosed += HandleRewardedAdClosed;

        this.rewardAd.LoadAd(request); 
    }

    // Update is called once per frame
    void Update()
    {
        if(requesting)
        {
            if (this.rewardAd.IsLoaded())
            {
                requesting = false;
                this.rewardAd.Show();
            }
        }
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        gameOver.WatchAd();

        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received!!!");
    }

    public void RequestRewardAd()
    {
        requesting = true;
    }
}
