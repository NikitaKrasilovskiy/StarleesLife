using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureObjectButton : MonoBehaviour
{
    [Header("Данные")]
    [SerializeField] private FurnitureManager furnitureManager;
    [SerializeField] private FurnitureObjectData furnitureObjectData;
    [SerializeField] private FurnitureVariantData furnitureVariantData;

    [Header("Иконка")]
    [SerializeField] private Image iconField;

    private void Start()
    {
        if (!iconField) iconField = GetComponent<Image>();
    }

    public void Activate(FurnitureManager furnitureManager, FurnitureObjectData furnitureObjectData)
    {
        this.furnitureManager = furnitureManager;

        this.furnitureObjectData = furnitureObjectData;
        this.furnitureVariantData = furnitureObjectData.Variants[0];

        iconField.sprite = furnitureObjectData.Icon;
    }
    public void Activate(FurnitureManager furnitureManager, FurnitureObjectData furnitureObjectData, FurnitureVariantData furnitureVariantData)
    {
        this.furnitureManager = furnitureManager;

        this.furnitureObjectData = furnitureObjectData;
        this.furnitureVariantData = furnitureVariantData;

        iconField.sprite = furnitureVariantData.Icon;
    }

    public void OnButtonPressed()
    {
        string name = furnitureManager.windowType == FurnitureManager.FurnitureWindowType.ObjectSelector ? furnitureObjectData.Name : furnitureVariantData.Name;
        string price = furnitureManager.windowType == FurnitureManager.FurnitureWindowType.ObjectSelector ? furnitureObjectData.Price.ToString() : furnitureVariantData.Price.ToString();
        furnitureManager.priceText.text = RoomManager.Instance.GetFurniture(name) ? "Apply" : price;

        RoomManager.Instance.selectedObjectData = furnitureObjectData;
        RoomManager.Instance.selectedVariantData = furnitureVariantData;

        RoomManager.Instance.SpawnFurniture(RoomManager.Instance.selectedObjectType, furnitureVariantData);
    }
}
