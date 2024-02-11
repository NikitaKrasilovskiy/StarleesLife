using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct ShopLabelImage
{
    public ShopLabelColor color;
    public Sprite sprite;
}

public enum ShopLabelColor
{
    Blue = 1,
    Pink = 2,
}