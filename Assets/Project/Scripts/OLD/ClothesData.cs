using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ClothesData
{
    [Header("������� ������")]
    public List<ThingClothesData> Outerwear;

    [Header("�����")]
    public List<ThingClothesData> Footwear;
}
