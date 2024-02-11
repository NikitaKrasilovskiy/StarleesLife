using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Goryned;

namespace Goryned
{
    namespace PlayFab
    {
        public static class PlayFabManager
        {
            public static void LoginPlayFab(bool current = true)
            {
#if UNITY_ANDROID || UNITY_EDITOR || UNITY_STANDALONE
                //var request = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = "308A6ED0AFD652A9", CreateAccount = true };
                var request = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = Core.System.GetDeviceID(current), CreateAccount = true };
                PlayFabClientAPI.LoginWithAndroidDeviceID(request, OnLoginSuccess, OnError);
#elif UNITY_IOS
                var request = new LoginWithIOSDeviceIDRequest {DeviceId = Core.System.GetDeviceID(current), CreateAccount = true};
                PlayFabClientAPI.LoginWithIOSDeviceID(request, OnLoginSuccess, OnError);
#else
                return;
#endif
            }
            private static void OnError(PlayFabError obj)
            {
                Debug.Log(obj.GenerateErrorReport());
            }

            private static void OnLoginSuccess(LoginResult obj)
            {
                Debug.Log(string.Format("LoginSuccess for {0} at {1}", obj.PlayFabId, obj.LastLoginTime));
                PlayFabTempData.playFabID = obj.PlayFabId;
                PlayFabTempData.loginResult = obj;
            }

            public static void GetPlayerProfile(string playFabID)
            {
                PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest()
                {
                    PlayFabId = playFabID,
                    ProfileConstraints = new PlayerProfileViewConstraints()
                    {
                        ShowDisplayName = true
                    }
                }, result => PlayFabTempData.playerProfileModel = result.PlayerProfile, OnError);
            }
            public static void GetUserData(string playFabId)
            {
                PlayFabClientAPI.GetUserData(new GetUserDataRequest()
                {
                    PlayFabId = playFabId,
                    Keys = null,
                }, result =>
                {
                    Dictionary<string, string> dataDictionary = new Dictionary<string, string>();
                    foreach (var key in result.Data.Keys)
                        dataDictionary.Add(key, result.Data[key].Value);
                    PlayFabTempData.userData = dataDictionary;
                }, OnError);
            }
            public static void UpdateUserData(Dictionary<string, string> dataDictionary)
            {
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = dataDictionary,
                    Permission = UserDataPermission.Public
                },result => PlayFabTempData.updateUserDataResult = result, OnError);
            }

            public static void GetPlayerStatistics()
            {
                PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest(),
                result =>
                {
                    Dictionary<string, int> dataDictionary = new Dictionary<string, int>();
                    foreach (var statistic in result.Statistics)
                        dataDictionary.Add(statistic.StatisticName, statistic.Value);
                    PlayFabTempData.playerStatistics = dataDictionary;
                }, OnError);
            }

            public static void UpdatePlayerStatistics(Dictionary<string, int> dataDictionary)
            {
                List<StatisticUpdate> statisticUpdates = new List<StatisticUpdate>();
                foreach (var key in dataDictionary.Keys)
                {
                    StatisticUpdate statisticUpdate = new StatisticUpdate();
                    statisticUpdate.StatisticName = key;
                    statisticUpdate.Value = dataDictionary[key];
                    statisticUpdates.Add(statisticUpdate);
                }
                PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
                {
                    Statistics = statisticUpdates
                }, result => PlayFabTempData.updatePlayerStatisticsResult = result, OnError);
            }

            public static void GetTitleData()
            {
                PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
                result => {
                    Dictionary<string, string> dataDictionary = new Dictionary<string, string>();
                    foreach (var key in result.Data.Keys)
                        dataDictionary.Add(key, result.Data[key]);
                    PlayFabTempData.titleData = dataDictionary;
                },
                error => {
                    Debug.Log("Got error getting titleData:");
                    Debug.Log(error.GenerateErrorReport());
                });
            }
        }
    }
}
