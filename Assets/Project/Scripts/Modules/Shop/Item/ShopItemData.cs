using System;
using UnityEngine;

[Serializable]
public struct ShopItemData
{
    public string ID;
    public PlayerParameterType playerParameterType;
    public Sprite sprite;
    public int cost;
    public int value;
}