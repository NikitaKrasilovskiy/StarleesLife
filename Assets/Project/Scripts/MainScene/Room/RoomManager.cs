using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }

    [Header("Комната")]
    [SerializeField] private Transform room;

    [Header("Объекты комнаты")]
    [SerializeField] private List<RoomObjectData> roomObjectDatas;

    [Header("Текущие данные")]
    [SerializeField] public FurnitureObjectType selectedObjectType;
    [SerializeField] public FurnitureObjectData selectedObjectData;
    [SerializeField] public FurnitureVariantData selectedVariantData;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateFurniture();
        ResetRotation();
        RotateRoomTo(FurnitureObjectType.Bed);
    }

    public void UpdateFurniture()
    {
        foreach (FurnitureObjectType furnitureObjectType in Enum.GetValues(typeof(FurnitureObjectType)))
        {
            string[] names = GetFurnitureConfiguration(furnitureObjectType);
            FurnitureVariantData furnitureVariantData = DataManager.instance.FurnitureDatas.GetFurnitureVariantData(furnitureObjectType, names[0], names[1]);
            SpawnFurniture(furnitureObjectType, furnitureVariantData, false);
        }
    }

    public RoomObjectData GetRoomObjectData(FurnitureObjectType furnitureObjectType)
    {
        return roomObjectDatas.Find(x => x.Type == furnitureObjectType);
    }
    private void ClearHolder(FurnitureObjectType furnitureObjectType)
    {
        Transform holder = GetRoomObjectData(furnitureObjectType).Holder;
        for (int i = holder.childCount - 1; i >= 0; i--) Destroy(holder.GetChild(i).gameObject);
    }

    public void RotateRoomTo(FurnitureObjectType furnitureObjectType)
    {
        ResetRotation();
        RoomObjectData roomObjectData = GetRoomObjectData(furnitureObjectType);
        room.localEulerAngles = roomObjectData.RoomRotationToObject;
        roomObjectData.DisableChildren.ForEach(t => t.gameObject.SetActive(false));
    }

    private void ResetRotation()
    {
        roomObjectDatas.ForEach(x => x.DisableChildren.ForEach(t => t.gameObject.SetActive(true)));
        room.localEulerAngles = Vector3.zero;
    }

    public void SpawnFurniture(FurnitureObjectType furnitureObjectType, FurnitureVariantData furnitureVariantData, bool withRotation = true)
    {
        Debug.Log(string.Format("Spawn Furniture: {0} - {1}", furnitureObjectType, furnitureVariantData.Name));
        RoomObjectData roomObjectData = GetRoomObjectData(furnitureObjectType);
        ClearHolder(furnitureObjectType);
        GameObject gameObject = Instantiate(furnitureVariantData.Model, roomObjectData.Holder);
        if (withRotation) RotateRoomTo(furnitureObjectType);
    }

    public void SaveFurniture(string name)
    {
        JsonDateTime jsonDateTime = DateTime.UtcNow;
        PlayerPrefs.SetString(name, JsonUtility.ToJson(jsonDateTime));
    }
    public bool GetFurniture(string name)
    {
        return PlayerPrefs.HasKey(name);
    }

    public void SaveFurnitureConfiguration(FurnitureObjectType furnitureObjectType, string objectName, string variantName)
    {
        PlayerPrefs.SetString(furnitureObjectType.ToString(), objectName + '=' + variantName);
    }
    public string[] GetFurnitureConfiguration(FurnitureObjectType furnitureObjectType)
    {
        if (!PlayerPrefs.HasKey(furnitureObjectType.ToString())) SaveFurnitureConfiguration(furnitureObjectType, "", "");
        string data = PlayerPrefs.GetString(furnitureObjectType.ToString());
        string[] names = data.Contains("=") && data.Length > 2 ? data.Split('=') : new string[2];
        return names;
    }
}
