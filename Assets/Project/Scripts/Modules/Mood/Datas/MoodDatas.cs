using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "MoodDatas", menuName = "StarleesLife/MoodDatas", order = 3)]
public class MoodDatas : ScriptableObjectInstaller<MoodDatas>
{
    public override void InstallBindings()
    {
        Container.BindInstance(this).AsSingle();
    }

    [Header("Параметры")]
    public int minPercentSmilesForTap;

    [Header("Стадии настроения")]
    public List<MoodData> Moods;

    public int GetSmilesForTapByMood(int maxSmilesForTap)
    {
        minPercentSmilesForTap = minPercentSmilesForTap <= 0 ? 75 : minPercentSmilesForTap;
        int mood = DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.Mood);
        int step = (100 - minPercentSmilesForTap) / Moods.Count;
        int percent = minPercentSmilesForTap + (step * mood);
        int smilesForTap = (maxSmilesForTap * percent) / 100;
        Debug.Log(maxSmilesForTap + " -> " + smilesForTap);
        return smilesForTap;
    }

    public List<string> GetMoodAnimNames(bool withIdle = true)
    {
        List<string> names = new List<string>();
        if (withIdle) names.AddRange(new List<string>() { "Idle", "IDLE" });
        foreach (var mood in Moods) names.Add(mood.idleAnimationClip.name);
        return names;
    }
}