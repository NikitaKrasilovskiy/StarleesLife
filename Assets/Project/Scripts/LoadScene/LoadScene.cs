using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Goryned.PlayFab;
using PlayFab.ClientModels;
using Goryned.Core;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Image fillerImage;
    private readonly string loadMusicName = "Starlees_Life_OST_Intro";
    async void Start()
    {
        //PlayerPrefs.DeleteAll();
        LoadChecker.Reset();
        await UniTask.WaitUntil(() => DataManager.instance != null);
        Login();
        AudioClip loadMusic = DataManager.instance.AudioDatas.GetAudio(loadMusicName);
        StartLoadScene("MainScene", loadMusic.length * 0.7f);
        DataManager.instance.AudioDatas.UpdateVolume(AudioDatas.AudioType.Music, 0.5f);
        SoundEngine.PlayAudio(loadMusic.name, true, AudioDatas.AudioType.Music);
    }

    public async void Login()
    {
        PlayFabManager.LoginPlayFab();
        await UniTask.WaitUntil(() => PlayFabTempData.loginResult != null);

        LoginResult loginResult = PlayFabTempData.loginResult;
        PlayFabTempData.loginResult = null;

        DataManager.instance.PlayerDatas.PlayerID = loginResult.PlayFabId;

        DataManager.instance.GetPlayerProfile(loginResult.PlayFabId);
        DataManager.instance.GetUserData(loginResult.PlayFabId);
        DataManager.instance.GetPlayerStatistics(loginResult.PlayFabId);
        DataManager.instance.GetTitleData();

        LoadChecker.Complete(LoadChecker.LoadStepType.PlayFabLogged);
    }

    private async void StartLoadScene(string sceneName, float time)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        float loadTime = time;
        float stepTime = time / 100f;
        while (time > 0)
        {
            time -= stepTime;
            LoadChecker.ShowProgress(1 - (time / loadTime));
            fillerImage.fillAmount = 1 - (time / loadTime);
            await UniTask.Delay(TimeSpan.FromSeconds(stepTime));
        }
        LoadChecker.Complete(LoadChecker.LoadStepType.SceneLoaded);
        await UniTask.WaitUntil(() => LoadChecker.IsAllReady() == true);
        SoundEngine.DestroyAudioSource(loadMusicName);
        asyncOperation.allowSceneActivation = true;
    }

    void Update()
    {
        
    }

}
