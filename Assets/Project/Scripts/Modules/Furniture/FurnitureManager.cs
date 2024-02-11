using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
    public enum FurnitureWindowType { None, ObjectSelector, ColorSelector}

    [SerializeField] private GameObject mainCategoriesWindow;
    [Header("Селектор")]
    [SerializeField] public FurnitureWindowType windowType;
    [SerializeField] private GameObject selector;
    [SerializeField] public TMP_Text priceText;
    [SerializeField] private Transform horizontalGrid;
    [SerializeField] private GameObject furnitureButtonPrefab;

    private void OnEnable()
    {
        SoundEngine.PlayAudio("windows");
        ClearGrid();
    }
    public void OpenCategory(FurnitureObjectType type, string objectName)
    {
        ClearGrid();
        mainCategoriesWindow.SetActive(false);
        windowType = FurnitureWindowType.ObjectSelector;

        RoomManager.Instance.selectedObjectType = type;

        selector.SetActive(true);
        List<FurnitureObjectData> furnitureObjectDatas = DataManager.instance.FurnitureDatas.GetFurnitureObjectDatas(type);
        foreach (var item in furnitureObjectDatas)
        {
            if (DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.GrowLevel) >= item.GrowLevel)
            {
                FurnitureObjectButton furnitureObjectButton = Instantiate(furnitureButtonPrefab, horizontalGrid).GetComponent<FurnitureObjectButton>();
                furnitureObjectButton.Activate(this, item);
                if (furnitureObjectDatas.IndexOf(item) == 0) furnitureObjectButton.OnButtonPressed();
            }
        }
    }
    public void OpenObject(FurnitureObjectData furnitureObjectData)
    {
        ClearGrid();
        mainCategoriesWindow.SetActive(false);
        windowType = FurnitureWindowType.ColorSelector;
        RoomManager.Instance.selectedObjectData = furnitureObjectData;

        selector.SetActive(true);
        List<FurnitureVariantData> furnitureVariantDatas = furnitureObjectData.Variants;

        foreach (var item in furnitureVariantDatas)
        {
            FurnitureObjectButton furnitureObjectButton = Instantiate(furnitureButtonPrefab, horizontalGrid).GetComponent<FurnitureObjectButton>();
            furnitureObjectButton.Activate(this, furnitureObjectData, item);
            if (furnitureVariantDatas.IndexOf(item) == 0) furnitureObjectButton.OnButtonPressed();
        }
        RoomManager.Instance.RotateRoomTo(RoomManager.Instance.selectedObjectType);
    }
    public void OnAcceptButtonPressed()
    {
        string name = windowType == FurnitureWindowType.ObjectSelector ? RoomManager.Instance.selectedObjectData.Name : RoomManager.Instance.selectedVariantData.Name;
        int price = windowType == FurnitureWindowType.ObjectSelector ? RoomManager.Instance.selectedObjectData.Price : RoomManager.Instance.selectedVariantData.Price;
        price = RoomManager.Instance.GetFurniture(name) ? 0 : price;

        if (DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.Stars) >= price)
        {
            DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.Stars, -price);
            RoomManager.Instance.SaveFurniture(name);
            RoomManager.Instance.SaveFurnitureConfiguration(RoomManager.Instance.selectedObjectType, RoomManager.Instance.selectedObjectData.Name, RoomManager.Instance.selectedVariantData.Name);

            switch (windowType)
            {
                case FurnitureWindowType.None:
                    break;
                case FurnitureWindowType.ObjectSelector:
                    SoundEngine.PlayAudio("furniture_change");
                    OpenObject(RoomManager.Instance.selectedObjectData);
                    break;
                case FurnitureWindowType.ColorSelector:
                    SoundEngine.PlayAudio("furniture_color");
                    if (price > 0) FindObjectOfType<MoodManager>().IncreaseMood();
                    ClearGrid();
                    break;
                default:
                    ClearGrid();
                    break;
            }
        }
    }

    public void OnCloseButtonPressed()
    {
        switch (windowType)
        {
            case FurnitureWindowType.None:
                ClearGrid();
                break;
            case FurnitureWindowType.ObjectSelector:
                ClearGrid();
                break;
            case FurnitureWindowType.ColorSelector:
                OpenCategory(RoomManager.Instance.selectedObjectType, "");
                break;
            default:
                ClearGrid();
                break;
        }
    }

    private void ClearGrid()
    {
        RoomManager.Instance.selectedObjectData = new FurnitureObjectData();
        RoomManager.Instance.selectedVariantData = new FurnitureVariantData();
        for (int i = horizontalGrid.childCount - 1; i >= 0; i--)
        {
            Destroy(horizontalGrid.GetChild(i).gameObject);
        }
        selector.SetActive(false);
        RoomManager.Instance.UpdateFurniture();
        windowType = FurnitureWindowType.None;
        mainCategoriesWindow.SetActive(true);
    }
}
