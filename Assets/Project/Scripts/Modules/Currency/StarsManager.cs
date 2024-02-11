using Cysharp.Threading.Tasks;
using Goryned.Core;
using Goryned.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsManager : MonoBehaviour
{
    private readonly string ExitTimeKey = "Stars-ExitTime", StartTimeKey = "Stars-StartTime";

    private void Start()
    {
        FixTime(true);
        IncreaseStars();
    }

    private async void IncreaseStars()
    {
        BoostData starsGrow = DataManager.instance.CreateBoostData(PlayerParameterType.StarsGrow_Online);

        AddStars(starsGrow.value);

        await UniTask.Delay(TimeSpan.FromSeconds(1f));

        IncreaseStars();
    }
    private void AddStars(int increaser)
    {
        BoostData starsLimit = DataManager.instance.CreateBoostData(PlayerParameterType.StarsLimit);
        int stars = DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.Stars);
        stars = Mathf.Clamp(stars + increaser, 0, starsLimit.value);
        DataManager.instance.PlayerDatas.UpdatePlayerParameter(PlayerParameterType.Stars, stars);
    }

    private void OnApplicationQuit()
    {
        FixTime(false);
    }
    private void OnApplicationPause(bool pause)
    {
        FixTime(!pause);
    }

    private void FixTime(bool start)
    {
        if (start)
        {
            DateTime exitTime = DateTimeManager.GetDateTime(ExitTimeKey);
            float seconds = DateTimeManager.GetSeconds(exitTime);
            BoostData starsGrow = DataManager.instance.CreateBoostData(PlayerParameterType.StarsGrow_Online);
            int increaser = starsGrow.value * (int)seconds;
            AddStars(increaser);
            FixTime(false);

            Debug.Log(string.Format("������ {0} ������ * {1} = {2}", seconds, starsGrow.value, increaser));
        }
        else
        {
            DateTimeManager.SaveDateTime(ExitTimeKey);
        }
    }
}
