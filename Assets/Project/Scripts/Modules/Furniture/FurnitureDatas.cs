using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "FurnitureDatas", menuName = "StarleesLife/FurnitureDatas", order = 5)]
public class FurnitureDatas : ScriptableObjectInstaller<FurnitureDatas>
{
    public override void InstallBindings()
    {
        Container.BindInstance(this).AsSingle();
    }

    [Header("Кровати - Bed")]
    [SerializeField] private List<FurnitureObjectData> beds;

    [Header("Книжный шкаф - Bookcase")]
    [SerializeField] private List<FurnitureObjectData> bookcases;

    [Header("Шкаф - Chest")]
    [SerializeField] private List<FurnitureObjectData> chests;

    [Header("Игровая зона - PlayZone")]
    [SerializeField] private List<FurnitureObjectData> playZones;

    [Header("Стена - Wall")]
    [SerializeField] private List<FurnitureObjectData> walls;

    [Header("Стол - Table")]
    [SerializeField] private List<FurnitureObjectData> tables;

    public List<FurnitureObjectData> GetFurnitureObjectDatas(FurnitureObjectType type)
    {
        switch (type)
        {
            case FurnitureObjectType.Bed:
                return beds;
            case FurnitureObjectType.Bookcase:
                return bookcases;
            case FurnitureObjectType.Chest:
                return chests;
            case FurnitureObjectType.Playzone:
                return playZones;
            case FurnitureObjectType.Wall:
                return walls;
            case FurnitureObjectType.Table:
                return tables;
            default:
                return new List<FurnitureObjectData>();
        }
    }
    public FurnitureObjectData GetFurnitureObjectData(FurnitureObjectType furnitureObjectType, string objectName)
    {
        FurnitureObjectData furnitureObjectData = GetFurnitureObjectDatas(furnitureObjectType).Find(f => f.Name == objectName);
        furnitureObjectData = !string.IsNullOrEmpty(furnitureObjectData.Name) && furnitureObjectData.Name == objectName ? furnitureObjectData : GetFurnitureObjectDatas(furnitureObjectType)[0];
        return furnitureObjectData;
    }
    public FurnitureVariantData GetFurnitureVariantData(FurnitureObjectType furnitureObjectType, string objectName, string variantName)
    {
        Debug.Log(string.Format("GetFurnitureVariantData - {0} - {1} - {2}", furnitureObjectType, objectName, variantName));
        FurnitureObjectData furnitureObjectData = GetFurnitureObjectData(furnitureObjectType, objectName);
        FurnitureVariantData furnitureVariantData = furnitureObjectData.Variants.Find(f => f.Name == variantName);
        furnitureVariantData = !string.IsNullOrEmpty(furnitureVariantData.Name) && furnitureVariantData.Name == variantName ? furnitureVariantData : GetFurnitureObjectData(furnitureObjectType, objectName).Variants[0];
        return furnitureVariantData;
    }


    public List<Sprite> GetIconsByGrowLevel(int growLevel)
    {
        List<Sprite> icons = new List<Sprite>();

        foreach (FurnitureObjectType objectType in Enum.GetValues(typeof(FurnitureObjectType)))
        {
            List<FurnitureObjectData> furnitureObjectDatas = GetFurnitureObjectDatas(objectType);
            foreach (var item in furnitureObjectDatas)
            {
                if (item.GrowLevel == growLevel) icons.Add(item.Icon);
            }
        }

        return icons;
    }
}