using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ShopBlockData
{
    [Header("Блок")]
    public string blockName;
    public ShopLabelColor labelColor;
    public ShopBlockColor blockColor;

    [Header("Товары")]
    public List<ShopItemData> itemDatas;
}