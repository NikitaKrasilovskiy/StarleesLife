using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct FurnitureObjectInfo
{
    public FurnitureObjectType ObjectType;
    public Transform Holder;
    public Vector3 RoomRotation;
    public List<GameObject> DisableChildren;
}
