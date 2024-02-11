using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ThingClothesData
{
    public string Name;
    public Sprite Icon;
    public int Cost;
    public int GrowLevel;
    public Mesh Mesh;
    public Material[] Materials;
}