using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BoostData
{
    [Header("Улучшение")]
    public string boostName;
    public PlayerParameterType boostType;
    public PlayerParameterType currencyType;

    [Header("Визуал")]
    public Sprite circleBackground;
    public Sprite circleFiller;
    public Sprite infoPanel;
    public Sprite lineFiller;
    public Sprite currencyIcon;

    [Header("Параметры")]
    public int level;
    public int cost;
    public int value;

    public BoostData UpdateParameters()
    {
        level = DataManager.instance.PlayerDatas.GetParameter(boostType);

        int costBase = DataManager.instance.PlayerDatas.GetParameter((BaseParameterType)(int)boostType + 1);
        int costPercent = DataManager.instance.PlayerDatas.GetParameter((BaseParameterType)(int)boostType + 2);
        int valueBase = DataManager.instance.PlayerDatas.GetParameter((BaseParameterType)(int)boostType + 3);
        int valuePercent = DataManager.instance.PlayerDatas.GetParameter((BaseParameterType)(int)boostType + 4);

        cost = Mathf.RoundToInt(costBase * Mathf.Pow((costPercent / 100f), level));
        value = Mathf.RoundToInt(valueBase * Mathf.Pow((valuePercent / 100f), level));

        //Debug.Log(string.Format("{0} - Level {1} - Cost: {2} -> {3}, Value: {4} -> {5}", boostType, level, costBase, cost, valueBase, value));

        return this;
    }
}
