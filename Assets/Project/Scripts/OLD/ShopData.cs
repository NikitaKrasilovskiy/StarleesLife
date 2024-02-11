using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopData
{
    [Header("Èíòåðôåéñû")]
    public List<ShopBlockImage> shopBlockImages;
    public List<ShopLabelImage> shopLabelImages;

    [Header("Òîâàðû")]
    public List<ShopBlockData> shopBlockDatas;
}
