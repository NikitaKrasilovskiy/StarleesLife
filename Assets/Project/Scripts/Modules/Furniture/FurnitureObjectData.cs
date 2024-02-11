using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct FurnitureObjectData
{
    public string Name;
    public int Price;
    public Sprite Icon;
    public int GrowLevel;

    public List<FurnitureVariantData> Variants;
}

[Serializable]
public struct FurnitureVariantData
{
    public string Name;
    public int Price;
    public Sprite Icon;
    public GameObject Model;
}
