using Cysharp.Threading.Tasks;
using Goryned.Core;
using Goryned.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmilesManager : MonoBehaviour
{
    private bool isIncreasing;
    private bool isAutoClicker;
    [SerializeField] private FxSmileButton _fxSmile;
    [SerializeField] private AdManager _adManager;
    public int smileTup;
    
    private void OnEnable()
    {
        isIncreasing = false;
        isAutoClicker = false;
        smileTup = 100;
    }
    public void OnPressed()
    {
        Vibration.Vibrate(20);

        BoostData smilesForTap = DataManager.instance.CreateBoostData(PlayerParameterType.SmilesForTap);
        int plusSmiles = DataManager.instance.MoodDatas.GetSmilesForTapByMood(smilesForTap.value);
        if (isIncreasing) plusSmiles *= 2;
        DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.Smiles, plusSmiles);
        smileTup--;
        if (smileTup == 0)
        {
            _adManager.GetComponent<AdManager>().AutoClickSceneOn();
        }
    }

    public async void IncreaseSmilesFor(float duration)
    {
        float time = duration;
        isAutoClicker = true;
        while (time > 0)
        {
            OnPressed();
            _fxSmile.SpawnSmileFx();
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            time--;
        }
        isAutoClicker = false;
    }
    public async void ActivateBoostSmilesFor(float duration)
    {
        isIncreasing = true;
        await UniTask.Delay(TimeSpan.FromSeconds(duration));
        isIncreasing = false;
    }
}
