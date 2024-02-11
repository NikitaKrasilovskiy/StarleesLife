using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.IO;
//using Firebase.Analytics;

public class AdManager : MonoBehaviour
{
    private InterstitialAd _interstitialAd;
    private RewardedAd _rewardAdSpin;
    private RewardedAd _rewardAutoAdSpin;

    [SerializeField] private GameObject FortuneScene;
    [SerializeField] private GameObject FortuneSceneShop;
    [SerializeField] private FortuneManager _fortuneMainScreen;
    [SerializeField] private FortuneManager _fortuneScreen;
    [SerializeField] private GameObject _autoClickScreen;
    [SerializeField] private SmilesManager _smilesManager;

    private int _fortuneScreenIndex;
    public int _spinTime;
    public int _autoClickTime;

    private string _interstitialAdUnitId;
    private string _rewardSpinAdUnitId;
    private string _rewardAutoAdUnitId;
    
    void Awake()
    {
        MobileAds.Initialize(status => {});
    }

    private void Start()
    {
        //_autoClickTime = 180;
        //ResetAutoClickTime();
        //_spinTime = 10;
        //ResetSpinTime();
        RewardSpinLoad();
        RewardAutoLoad();
        _rewardSpinAdUnitId = "ca-app-pub-5853277310445367/7944092170";
        _rewardAutoAdUnitId = "ca-app-pub-5853277310445367/6794662034";
    }
    
    public void ResetSpinTime()
    {
        if (_spinTime > 0)
        {
            _spinTime--;
            Invoke("ResetSpinTime", 1);
        }
    }
    
    public void ResetAutoClickTime()
    {
        if (_autoClickTime > 0)
        {
            _autoClickTime--;
            Invoke("ResetAutoClickTime", 1);
        }
        else if (_spinTime == 0)
        {
            _autoClickScreen.SetActive(true);
        }
    }

    public void AutoClickSceneOn()
    {
        if (_rewardAutoAdSpin.IsLoaded())
        {
            _autoClickScreen.SetActive(true);
        }
    }

    public void FortuneSceneOn()
    {
        if (_rewardAdSpin.IsLoaded())
        {
            if (_spinTime == 0)
            {
                switch (_fortuneScreenIndex)
                {
                    case 0:
                        FortuneScene.SetActive(true);
                        break;
                    case 1:
                        FortuneSceneShop.SetActive(true);
                        break;
                }
            }
        }
    }

    public void FortuneSceneIndex(int _fortuneScene)
    {
        _fortuneScreenIndex = _fortuneScene;
    }
    public void AutoClickSceneOff()
    {
        _smilesManager.GetComponent<SmilesManager>().smileTup = 100;
    }

    private void RewardSpinLoad()
    {
        _rewardSpinAdUnitId = "ca-app-pub-5853277310445367/7944092170";
        _rewardAdSpin = new RewardedAd(_rewardSpinAdUnitId);
        
        _rewardAdSpin.OnUserEarnedReward += HandleUserEarnedReward;
        _rewardAdSpin.OnAdClosed += HandleRewardedAdClosed;
        _rewardAdSpin.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        _rewardAdSpin.OnAdFailedToLoad += HandleFailedToLoad;

        AdRequest request = new AdRequest.Builder().Build();
        _rewardAdSpin.LoadAd(request);
    }

    private void HandleFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        RewardSpinLoad();
    }

    public void RewardAdRequest(int fortuneScreen)
    {
        _fortuneScreenIndex = fortuneScreen;
        ShowRewardSpinVideo();
    }
    
    public void ShowRewardSpinVideo()
    {
        if (_rewardAdSpin.IsLoaded())
        {
            _rewardAdSpin.Show();
            //FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAdImpression);
        }
    }
    
    private void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        RewardAdRequest(_fortuneScreenIndex);
    }

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        
    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        switch (_fortuneScreenIndex)
        {
            case 0:
                _spinTime = 120;
                ResetSpinTime();
                _fortuneMainScreen.GetComponent<FortuneManager>().Spin(true);
                break;
            case 1:
                _spinTime = 120;
                ResetSpinTime();
                _fortuneScreen.GetComponent<FortuneManager>().Spin(true);
                break;
        }
        RewardSpinLoad();
    }

    private void RewardAutoLoad()
    {
        _rewardAutoAdUnitId = "ca-app-pub-5853277310445367/6794662034";
        _rewardAutoAdSpin = new RewardedAd(_rewardAutoAdUnitId);
        
        _rewardAutoAdSpin.OnUserEarnedReward += HandleUserEarnedRewardAutoClicker;
        _rewardAutoAdSpin.OnAdClosed += HandleRewardedAdClosedAutoClicker;
        _rewardAutoAdSpin.OnAdFailedToShow += HandleRewardedAdFailedToShowAutoClicker;
        _rewardAutoAdSpin.OnAdFailedToLoad += HandleFailedToLoadAutoClicker;

        AdRequest request = new AdRequest.Builder().Build();
        _rewardAutoAdSpin.LoadAd(request);
    }

    private void HandleFailedToLoadAutoClicker(object sender, AdFailedToLoadEventArgs e)
    {
        RewardAutoLoad();
    }


    public void RewardAdRequestAutoClicker()
    {
        ShowRewardAutoVideo();
    }
    
    public void ShowRewardAutoVideo()
    {
        if (_rewardAutoAdSpin.IsLoaded())
        {
            _rewardAutoAdSpin.Show();
            //FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAdImpression);
        }
    }
    
    private void HandleRewardedAdFailedToShowAutoClicker(object sender, AdErrorEventArgs e)
    {
        //RewardAdRequestAutoClicker();
        //ResetAutoClickTime();
    }

    private void HandleRewardedAdClosedAutoClicker(object sender, EventArgs e)
    {
        
    }

    private void HandleUserEarnedRewardAutoClicker(object sender, Reward e)
    {
        MainScene.instance.smilesManager.IncreaseSmilesFor(20);
        _smilesManager.GetComponent<SmilesManager>().smileTup = 100;
        RewardAutoLoad();
    }
}
