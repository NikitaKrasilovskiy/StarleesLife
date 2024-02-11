using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct ShopBlockImage
{
    public ShopBlockColor color;
    public Sprite sprite;
}

public enum ShopBlockColor
{
    Orange = 1,
    Pink = 2,
}