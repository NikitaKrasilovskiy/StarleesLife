using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct FortunePrizeData
{
    public string prizeName;
    public FortunePrizeType prizeType;
    public Sprite prizeSprite;
    public int value;
    public int percent;
}

public enum FortunePrizeType
{
    None = 0,
    IncreaseStarsLimitLevel = 1,
    IncreaseStarsGrow_OnlineLevel = 2,
    IncreaseStarsGrow_OfflineLevel = 3,
    IncreaseSmilesForTapLevel = 4,
    HardCoins = 5,
    FreeSpin = 6,
    AutoClickerSmiles = 7,
    IncreaseSmilesForClick = 8
}