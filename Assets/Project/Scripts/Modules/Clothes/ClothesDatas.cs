using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ClothesDatas", menuName = "StarleesLife/ClothesDatas", order = 4)]
public class ClothesDatas : ScriptableObjectInstaller<ClothesDatas>
{
    public override void InstallBindings()
    {
        Container.BindInstance(this).AsSingle();
    }

    [Header("Тело")]
    public List<ThingClothesData> bodySet1;
    public List<ThingClothesData> bodySet2;
    public List<ThingClothesData> bodySet3;
    public List<ThingClothesData> bodySet4;
    public List<ThingClothesData> bodySet5;
    public List<ThingClothesData> bodySet6;
    public List<ThingClothesData> bodySet7;

    [Header("Обувь")]
    public List<ThingClothesData> baletki;
    public List<ThingClothesData> sandali;
    public List<ThingClothesData> tapochki;

    [Header("Волосы")]
    public List<ThingClothesData> hairStyles_1;
    public List<ThingClothesData> hairStyles_2;
    public List<ThingClothesData> hairStyles_3;

    [Header("Аксессуары")]
    public List<ThingClothesData> accessories;

    public void ApplyClothes(Transform clothesObject, ThingClothesData thingClothesData)
    {
        Debug.Log(string.Format("ApplyClothes to {0} - {1}", clothesObject.name, JsonUtility.ToJson(thingClothesData)));
        if (thingClothesData.Materials == null) thingClothesData.Materials = new Material[0];
        if (clothesObject.GetComponent<SkinnedMeshRenderer>() != null)
        {
            clothesObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = thingClothesData.Mesh;
            clothesObject.GetComponent<SkinnedMeshRenderer>().sharedMaterials = thingClothesData.Materials;
        }
        else if (clothesObject.GetComponent<MeshFilter>() != null && clothesObject.GetComponent<MeshRenderer>() != null)
        {
            clothesObject.GetComponent<MeshFilter>().sharedMesh = thingClothesData.Mesh;
            clothesObject.GetComponent<MeshRenderer>().sharedMaterials = thingClothesData.Materials;
        }
    }

    public List<ThingClothesData> GetClothesList(ClothesType clothesType)
    {
        switch (clothesType)
        {
            case ClothesType.Head_HairStyle_1:
                return hairStyles_1;
            case ClothesType.Head_HairStyle_2:
                return hairStyles_2;
            case ClothesType.Head_HairStyle_3:
                return hairStyles_3;
            case ClothesType.Head_Accessories:
                return accessories;
            case ClothesType.Body_Set1:
                return bodySet1;
            case ClothesType.Body_Set2:
                return bodySet2;
            case ClothesType.Body_Set3:
                return bodySet3;
            case ClothesType.Body_Set4:
                return bodySet4;
            case ClothesType.Body_Set5:
                return bodySet5;
            case ClothesType.Body_Set6:
                return bodySet6;
            case ClothesType.Body_Set7:
                return bodySet7;
            case ClothesType.Legs_Baletki:
                return baletki;
            case ClothesType.Legs_Sandali:
                return sandali;
            case ClothesType.Legs_Tapochki:
                return tapochki;
            default:
                return new List<ThingClothesData>();
        }
    }


    public ThingClothesData GetThingClothesData(Transform holder, ThingClothesData defaultClothesData)
    {
        string holderName = holder.name;
        if (!PlayerPrefs.HasKey(holderName) || string.IsNullOrEmpty(PlayerPrefs.GetString(holderName)))
        {
            return defaultClothesData;
        }
        else
        {
            string data = PlayerPrefs.GetString(holderName);
            SavedClothes savedClothes = JsonUtility.FromJson<SavedClothes>(data);
            ThingClothesData thingClothesData = GetClothesList(savedClothes.ClothesType).Find(c => c.Name == savedClothes.Name);
            return thingClothesData;
        }
    }
    public List<Sprite> GetIconsByGrowLevel(int growLevel)
    {
        List<Sprite> icons = new List<Sprite>();

        foreach (ClothesType clothesType in Enum.GetValues(typeof(ClothesType)))
        {
            List<ThingClothesData> thingClothesDatas = GetClothesList(clothesType);
            foreach (var item in thingClothesDatas)
            {
                if (item.GrowLevel == growLevel) icons.Add(item.Icon);
            }
        }

        return icons;
    }
}
[Serializable]
public struct SavedClothes
{
    public ClothesType ClothesType;
    public string Name;
}