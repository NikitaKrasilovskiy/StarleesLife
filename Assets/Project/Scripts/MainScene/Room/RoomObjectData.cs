using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RoomObjectData
{
    public FurnitureObjectType Type;
    public Transform Holder;
    public Vector3 RoomRotationToObject;
    public List<GameObject> DisableChildren;
    public FurnitureCategoryButton CategoryButton;
}
