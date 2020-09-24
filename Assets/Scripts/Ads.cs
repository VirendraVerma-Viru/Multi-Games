using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class Ads : MonoBehaviour
{
    string AppId = "ca-app-pub-5870321000407115~2435374965";
    string InterestitialAdID = "ca-app-pub-5870321000407115/9704123410";//test: ca-app-pub-3940256099942544/1033173712 //Realad: ca-app-pub-5870321000407115/3486072145
    string BannerAdId = "ca-app-pub-3940256099942544/6300978111";
    string RewardAd = "ca-app-pub-5870321000407115/7496129951";

    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(initStatus => { });

            // Get singleton reward based video ad reference.
            this.rewardBasedVideo = RewardBasedVideoAd.Instance;

            // Called when an ad request has successfully loaded.
            rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
            // Called when an ad request failed to load.
            rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            // Called when an ad is shown.
            rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
            // Called when the ad starts to play.
            rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
            // Called when the user should be rewarded for watching a video.
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            // Called when the ad is closed.
            rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
            // Called when the ad click caused the user to leave the application.
            rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

            this.RequestRewardBasedVideo();
           
        }
        
    }

    public void ShowInter()
    {
        print("Called");
        if (Application.platform == RuntimePlatform.Android)
        {
            UserOptToWatchAd();
        }
    }
    private void UserOptToWatchAd()
    {
      if (rewardBasedVideo.IsLoaded()) {
        rewardBasedVideo.Show();
      }
    }

    private void RequestRewardBasedVideo()
    {
        #if UNITY_ANDROID
            string adUnitId = RewardAd;
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
        this.RequestRewardBasedVideo();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
        
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }
}


