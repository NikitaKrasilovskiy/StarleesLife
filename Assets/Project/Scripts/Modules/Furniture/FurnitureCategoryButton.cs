using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureCategoryButton : MonoBehaviour
{
    [SerializeField] private FurnitureObjectType objectType;
    [SerializeField] private FurnitureManager furnitureManager;

    public void OnPressed()
    {
        furnitureManager.OpenCategory(objectType, "");
    }
}
