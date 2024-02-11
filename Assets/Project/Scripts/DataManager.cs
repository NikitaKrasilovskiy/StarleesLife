using Cysharp.Threading.Tasks;
using Goryned.Core;
using Goryned.PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Goryned.Core;
using System;
using Random = UnityEngine.Random;
using Goryned.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    [HideInInspector] [Inject] public GameData gameData;
    [HideInInspector] [Inject] public ShopData shopData;

    [HideInInspector] [Inject] public PlayerDatas PlayerDatas;
    [HideInInspector] [Inject] public CommonDatas CommonDatas;
    [HideInInspector] [Inject] public ActionsDatas ActionDatas;
    [HideInInspector] [Inject] public MoodDatas MoodDatas;
    [HideInInspector] [Inject] public BoostDatas BoostDatas;
    [HideInInspector] [Inject] public ClothesDatas ClothesDatas;
    [HideInInspector] [Inject] public FurnitureDatas FurnitureDatas;
    [HideInInspector] [Inject] public AudioDatas AudioDatas;

    public Coroutine updatingPlayerStatistics;

    private void Awake()
    {
        if (instance == null) instance = this;
        DontDestroyOnLoad(gameObject);

        //for (int i = 0; i < 4; i++)
        //{
        //    int value = Random.Range(0, (int)Mathf.Pow(1000, i % 4));
        //    Debug.Log(string.Format("{0} = {1}", value, Data.ReduceInt(value)));
        //}

    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    public async void GetPlayerProfile(string playFabID)
    {
        PlayFabManager.GetPlayerProfile(playFabID);
        await UniTask.WaitUntil(() => PlayFabTempData.playerProfileModel != null);

        PlayerProfileModel playerProfileModel = PlayFabTempData.playerProfileModel;
        PlayFabTempData.playerProfileModel = null;

        PlayerDatas.PlayerID = playerProfileModel.PlayerId;
        PlayerDatas.PlayerName = playerProfileModel.DisplayName;
        PlayerDatas.DeviceID = Goryned.Core.System.GetDeviceID();

        LoadChecker.Complete(LoadChecker.LoadStepType.ProfileLoaded);
    }

    public async void GetUserData(string playFabID)
    {
        PlayFabManager.GetUserData(playFabID);
        await UniTask.WaitUntil(() => PlayFabTempData.userData != null);

        Dictionary<string, string> userData = PlayFabTempData.userData;
        PlayFabTempData.userData = null;

        LoadChecker.Complete(LoadChecker.LoadStepType.UserDataLoaded);
    }

    public async void GetPlayerStatistics(string playFabID = null)
    {
        PlayFabManager.GetPlayerStatistics();
        await UniTask.WaitUntil(() => PlayFabTempData.playerStatistics != null);

        Dictionary<string, int> playerStatistics = PlayFabTempData.playerStatistics;
        PlayFabTempData.playerStatistics = null;

        Dictionary<string, int> playerParameters = new Dictionary<string, int>();
        foreach (PlayerParameterType playerParameter in Enum.GetValues(typeof(PlayerParameterType)))
        {
            int value = playerStatistics.ContainsKey(playerParameter.ToString()) ? playerStatistics[playerParameter.ToString()] : GetDefaultParameterValue(playerParameter);
            playerParameters.Add(playerParameter.ToString(), value);
        }
        PlayerDatas.UpdateDictionary(PlayerDatas.PlayerDataType.PlayerParameterType, playerParameters);

        UpdatePlayerStatistics();

        LoadChecker.Complete(LoadChecker.LoadStepType.StatisticsLoaded);
    }

    public async void UpdatePlayerStatistics()
    {
        PlayFabTempData.updatePlayerStatisticsResult = null;
        PlayerDatas.PlayerDataType playerDataType = PlayerDatas.PlayerDataType.PlayerParameterType;
        PlayFabManager.UpdatePlayerStatistics(PlayerDatas.GetDictionary(playerDataType));
        PlayerPrefs.SetString("PlayerParameters", Data.DictionaryToString(PlayerDatas.GetDictionary(playerDataType)));
        await UniTask.WaitUntil(() => PlayFabTempData.updatePlayerStatisticsResult != null);
        //Debug.Log("PlayerParameters: " + Data.DictionaryToString(PlayerDatas.GetDictionary(playerDataType)));
    }

    public async void GetTitleData()
    {
        PlayFabManager.GetTitleData();
        await UniTask.WaitUntil(() => PlayFabTempData.titleData != null);

        Dictionary<string, string> titleData = PlayFabTempData.titleData;
        PlayFabTempData.titleData = null;

        Dictionary<string, int> baseParameters = new Dictionary<string, int>();
        //Debug.Log("=====TITLE DATA=====");
        foreach (BaseParameterType baseParameter in Enum.GetValues(typeof(BaseParameterType)))
        {
            if (titleData.ContainsKey(baseParameter.ToString()))
            {
                int value = int.Parse(titleData[baseParameter.ToString()]);
                baseParameters.Add(baseParameter.ToString(), value);
                //Debug.Log(string.Format("{0} - {1}", baseParameter, value));
            }
        }
        PlayerDatas.UpdateDictionary(PlayerDatas.PlayerDataType.BaseParameterType, baseParameters);

        LoadChecker.Complete(LoadChecker.LoadStepType.TitleDataLoaded);
    }

    public void StartUpdatingPlayerStatistics(bool start = true)
    {
        if (updatingPlayerStatistics != null) StopCoroutine(updatingPlayerStatistics);

        if (start) updatingPlayerStatistics = StartCoroutine(UpdatingPlayerStatistics());
    }
    private IEnumerator UpdatingPlayerStatistics()
    {
        yield return new WaitForSeconds(5f);
        UpdatePlayerStatistics();
        yield return new WaitUntil(() => PlayFabTempData.updatePlayerStatisticsResult != null);
        StartUpdatingPlayerStatistics();
    }

    private void OnApplicationPause(bool pause)
    {
        UpdatePlayerStatistics();
    }
    private void OnApplicationQuit()
    {
        UpdatePlayerStatistics();
    }

    public BoostData CreateBoostData(PlayerParameterType boostType)
    {
        BoostData boostData = new BoostData();
        boostData.boostType = boostType;
        boostData = boostData.UpdateParameters();
        return boostData;
    }

    public List<Sprite> GetIconsByGrowLevel(int growLevel)
    {
        List<Sprite> icons = new List<Sprite>();
        icons.AddRange(ActionDatas.GetIconsByGrowLevel(growLevel));
        icons.AddRange(FurnitureDatas.GetIconsByGrowLevel(growLevel));
        icons.AddRange(ClothesDatas.GetIconsByGrowLevel(growLevel));
        return icons;
    }

    public int GetDefaultParameterValue(PlayerParameterType playerParameterType)
    {
        switch (playerParameterType)
        {
            case PlayerParameterType.HardCoins: return 357;
            case PlayerParameterType.Mood: return 5;
            default: return 0;
        }
    }
}

[Serializable]
public struct UpgradeParameter
{
    public PlayerParameterType parameterType;
    public int cost;
    public int value;
    public int level;
}