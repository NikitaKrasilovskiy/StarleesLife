using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClothesManager : MonoBehaviour
{
    [SerializeField] private GirlManager girlManager;

    [Header("Выбранная категория")]
    public ClothesType clothesType;
    public List<ThingClothesData> clothesDatas;

    [Header("Выбранная одежда")]
    public ThingClothesData clothesData;

    [Header("Кнопка одежды")]
    [SerializeField] private GameObject thingSelector;
    [SerializeField] public TMP_Text priceText;
    [SerializeField] private Transform horizontalGrid;
    [SerializeField] private GameObject clothesThingItemObject;

    [Header("Объекты")]
    [SerializeField] private GameObject mainCategories;
    [SerializeField] private GameObject bodyCategories;
    [SerializeField] private GameObject headCategories;
    [SerializeField] private GameObject hairCategories;
    [SerializeField] private GameObject bootsCategories;

    private Vector3 defaultCameraRotation;

    [Header("Ротации")]
    [SerializeField] private List<CameraRotationToClothes> toClothes;

    private void OnEnable()
    {
        SoundEngine.PlayAudio("windows");
        defaultCameraRotation = Camera.main.transform.eulerAngles;
        DeactivateAllCategories();
        mainCategories.SetActive(true);
        ClearGrid();
        
        if (PlayerPrefs.GetInt("Tutorial") == 23)
        {
            
        }
    }

    private void DeactivateAllCategories()
    {
        mainCategories.SetActive(false);
        bodyCategories.SetActive(false);
        headCategories.SetActive(false);
        hairCategories.SetActive(false);
        bootsCategories.SetActive(false);
    }

    private void OnDisable()
    {
        Camera.main.transform.transform.eulerAngles = defaultCameraRotation;
    }

    public void ShowGrid(ClothesType clothesType)
    {
        DeactivateAllCategories();
        ClearGrid();
        thingSelector.SetActive(true);
        this.clothesType = clothesType;
        clothesDatas = DataManager.instance.ClothesDatas.GetClothesList(clothesType);
        foreach (var clothesData in clothesDatas)
        {
            if (DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.GrowLevel) >= clothesData.GrowLevel)
            {
                ClothesThingItem clothesThingItem = Instantiate(clothesThingItemObject, horizontalGrid).GetComponent<ClothesThingItem>();
                clothesThingItem.Activate(this, GetTransform(clothesType), clothesData);
                if (clothesDatas.IndexOf(clothesData) == 0) clothesThingItem.OnThingPressed();
            }
        }
        Debug.Log("GetTransform(clothesType).position - " + GetTransform(clothesType).position);
        Camera.main.transform.eulerAngles = toClothes.Find(a => a.transform.name == GetTransform(clothesType).name).rotation;
    }

    public void OnAcceptButtonPressed()
    {
        if (DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.Stars) >= clothesData.Cost)
        {
            SoundEngine.PlayAudio("item_use");

            DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.Stars, -clothesData.Cost);
            SavedClothes savedClothes = new SavedClothes() { Name = clothesData.Name, ClothesType = clothesType, };
            PlayerPrefs.SetString(GetTransform(clothesType).name, JsonUtility.ToJson(savedClothes));
            PlayerPrefs.SetString(clothesData.Name, JsonUtility.ToJson(savedClothes));

            if (clothesData.Cost > 0) FindObjectOfType<MoodManager>().IncreaseMood();

            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }

    private void ClearGrid()
    {
        for (int i = horizontalGrid.childCount - 1; i >= 0; i--)
        {
            Destroy(horizontalGrid.GetChild(i).gameObject);
        }
        thingSelector.SetActive(false);
        GirlManager.instance.ApplyClothes();
    }

    private Transform GetTransform(ClothesType clothesType)
    {
        switch (clothesType)
        {
            case ClothesType.Head_HairStyle_1:
            case ClothesType.Head_HairStyle_2:
            case ClothesType.Head_HairStyle_3:
                return girlManager.hair;
            case ClothesType.Head_Accessories:
                return girlManager.accessory;
            case ClothesType.Body_Set1:
            case ClothesType.Body_Set2:
            case ClothesType.Body_Set3:
            case ClothesType.Body_Set4:
            case ClothesType.Body_Set5:
            case ClothesType.Body_Set6:
            case ClothesType.Body_Set7:
                return girlManager.body;
            case ClothesType.Legs_Baletki:
            case ClothesType.Legs_Sandali:
            case ClothesType.Legs_Tapochki:
                return girlManager.boots;
            default:
                return null;
        }
    }
}


[Serializable]
public struct CameraRotationToClothes
{
    public Transform transform;
    public Vector3 rotation;
}