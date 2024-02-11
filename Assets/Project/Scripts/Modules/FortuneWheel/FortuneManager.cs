using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using Goryned.Core;

public class FortuneManager : MonoBehaviour
{
    private readonly string lastFreeSpinKey = "LastFreeSpinDate";

    [Header("Поля")]
    [SerializeField] private Transform wheelTransform;
    [SerializeField] private List<FortunePrizeDataViewer> prizeDataViewers;
    [SerializeField] private float maxAngleSpeedPerSecond;
    [SerializeField] private TMP_Text spinButtonText;

    [Header("Параметры кручения")]
    [SerializeField] private AnimationCurve wheelSpeedDynamics;
    [Range(2, 4)] [SerializeField] private int minSpins = 2;
    [Range(4, 6)] [SerializeField] private int maxSpins = 6;
    [SerializeField] private int spinCost;

    [Header("Текущий приз")]
    [SerializeField] private FortunePrizeData currentPrizeData;
    [SerializeField] private bool isSpining;

    [Header("Получение приза")]
    [SerializeField] private GameObject pickZone;
    [SerializeField] private FortunePrizeDataViewer prizeViewer;
    
    [SerializeField] private GameObject adRegion;
    [SerializeField] private int adIndex;
    [SerializeField] private AdManager adManager;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        SoundEngine.PlayAudio("windows");
        UpdatePrizes();
        UpdateScreen();
    }

    void Update()
    {

    }

    private void UpdateScreen()
    {
        isSpining = false;
        ShowPrize(false);
        UpdateSpinButton(false);
        adManager.FortuneSceneOn();
        /*
        if (adIndex == 0 && adManager.GetComponent<AdManager>()._spinTime == 0)
        {
            
        }
        else if (adIndex == 1)
        {
            adIndex = 0;
        }
        */
    }

    public void AdStart(int fortuneScreen)
    {
        adIndex = 1;
        adRegion.SetActive(false);
        adManager.GetComponent<AdManager>().RewardAdRequest(fortuneScreen);
    }
    
    private void UpdateSpinButton(bool update)
    {
        if (update)
        {
            DateTimeManager.SaveDateTime(lastFreeSpinKey);
        }
        else
        {
            if (!DateTimeManager.HasKey(lastFreeSpinKey)) DateTimeManager.SaveDateTime(lastFreeSpinKey, DateTime.UtcNow.AddDays(-1));
            DateTime lastDate = DateTimeManager.GetDateTime(lastFreeSpinKey);
            bool freeSpin = DateTimeManager.GetInterval(lastDate).Days > 0;

            spinCost = freeSpin ? 0 : DataManager.instance.gameData.spinCost;
            spinButtonText.text = freeSpin ? "Spin for free" : string.Format("Spin for {0} crystalls", spinCost);
        }

    }

    public void OnSpinButtonPressed()
    {
        if (!isSpining && DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.HardCoins) >= spinCost)
        {
            SoundEngine.PlayAudio("fortune_wheel_start");
            Spin(false);
        }
    }

    public async void Spin(bool adSpin)
    {
        isSpining = true;
        DateTime startTime = DateTime.Now;

        if (!adSpin)
        {
            DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.HardCoins, -spinCost);
        }
        
        float anglePerPrize = 360 / prizeDataViewers.Count;
        float turns = UnityEngine.Random.Range(minSpins, maxSpins + 1);

        currentPrizeData = GetRandomPrizeData();
        int prizeIndex = DataManager.instance.gameData.fortunePrizeDatas.IndexOf(currentPrizeData);
        float maxRotation = (360 * turns) + (anglePerPrize * prizeIndex) - wheelTransform.eulerAngles.z;

        float currentRotation = 0;

        float cyclesPerSecond = 100;

        Debug.Log(prizeIndex);

        SoundEngine.PlayAudio("fortune_wheel_work", true);
        while (currentRotation < maxRotation)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1 / cyclesPerSecond));

            float plusAngle = wheelSpeedDynamics.Evaluate(currentRotation / maxRotation) * (maxAngleSpeedPerSecond / 100);

            wheelTransform.Rotate(0, 0, plusAngle);
            currentRotation += plusAngle;
        }

        Debug.Log(JsonUtility.ToJson(currentPrizeData));

        UpdateSpinButton(true);
        SoundEngine.DestroyAudioSource("fortune_wheel_work");
        SoundEngine.PlayAudio("fortune_wheel_stop");
        ShowPrize(true);

        Debug.Log((DateTime.Now - startTime).TotalSeconds);

        isSpining = false;
    }

    private FortunePrizeData GetRandomPrizeData()
    {
        List<FortunePrizeData> prizeDatas = new List<FortunePrizeData>(DataManager.instance.gameData.fortunePrizeDatas);
        System.Random rnd = new System.Random();
        prizeDatas = prizeDatas.OrderBy(d => rnd.Next()).ToList();

        int maxPercent = 0;
        List<int> minPercents = new List<int>();
        foreach (var item in prizeDatas)
        {
            minPercents.Add(maxPercent);
            maxPercent += item.percent;
        }

        int percent = UnityEngine.Random.Range(0, maxPercent);
        int index = 0;
        for (int i = 0; i < minPercents.Count; i++)
        {
            if (minPercents[i] > percent)
            {
                index = i;
                break;
            }
        }

        FortunePrizeData prizeData = prizeDatas[index];
        return prizeData;
    }

    private void UpdatePrizes()
    {
        for (int i = 0; i < prizeDataViewers.Count; i++)
        {
            prizeDataViewers[i].Activate(DataManager.instance.gameData.fortunePrizeDatas[i]);
        }
    }

    private void ShowPrize(bool show)
    {
        pickZone.SetActive(show);
        prizeViewer.Activate(currentPrizeData, true);
    }

    public void OnPickButtonPressed()
    {
        switch (currentPrizeData.prizeType)
        {
            case FortunePrizeType.None:
                break;
            case FortunePrizeType.IncreaseStarsLimitLevel:
                DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.StarsLimit, currentPrizeData.value);
                break;
            case FortunePrizeType.IncreaseStarsGrow_OnlineLevel:
                DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.StarsGrow_Online, currentPrizeData.value);
                break;
            case FortunePrizeType.IncreaseStarsGrow_OfflineLevel:
                DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.StarsGrow_Offline, currentPrizeData.value);
                break;
            case FortunePrizeType.IncreaseSmilesForTapLevel:
                DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.SmilesForTap, currentPrizeData.value);
                break;
            case FortunePrizeType.HardCoins:
                DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.HardCoins, currentPrizeData.value);
                break;
            case FortunePrizeType.FreeSpin:
                DateTimeManager.SaveDateTime(lastFreeSpinKey, DateTime.UtcNow.AddDays(-1));
                break;
            case FortunePrizeType.AutoClickerSmiles:
                MainScene.instance.smilesManager.IncreaseSmilesFor(currentPrizeData.value);
                break;
            case FortunePrizeType.IncreaseSmilesForClick:
                MainScene.instance.smilesManager.ActivateBoostSmilesFor(currentPrizeData.value);
                break;
            default:
                break;
        }
        UpdateScreen();
    }

    public void OnCloseButtonPressed(bool onScreen)
    {
        if (!onScreen)
        {
            if (!isSpining) UIManager.Instance.OpenPanel(MainScenePanelType.ShopPanel);
        }
    }
}