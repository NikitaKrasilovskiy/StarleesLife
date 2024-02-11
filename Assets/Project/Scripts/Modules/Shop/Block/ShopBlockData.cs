using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ShopBlockData
{
    [Header("����")]
    public string blockName;
    public ShopLabelColor labelColor;
    public ShopBlockColor blockColor;

    [Header("������")]
    public List<ShopItemData> itemDatas;
}