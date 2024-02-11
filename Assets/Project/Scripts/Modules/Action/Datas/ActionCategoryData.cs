using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ActionCategoryData
{
    [Header("Êàòåãîðèÿ")]
    public string CategoryName;
    public ActionCategoryType CategoryType;
    public Sprite CategoryIcon;

    [Header("Äåéñòâèÿ")]
    public List<ActionData> Actions;
}