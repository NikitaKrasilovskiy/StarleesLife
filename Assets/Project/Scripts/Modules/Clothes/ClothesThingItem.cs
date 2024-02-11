using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothesThingItem : MonoBehaviour
{
    [Header("Одежда")]
    [SerializeField] private Transform clothesOject;
    [SerializeField] private ClothesManager clothesManager;
    [SerializeField] private ThingClothesData thingClothesData;

    [Header("Картинка")]
    [SerializeField] private Image thingIconField;

    public void Activate(ClothesManager manager, Transform clothesObjectTransform, ThingClothesData data)
    {
        clothesManager = manager;
        clothesOject = clothesObjectTransform;
        thingClothesData = data;
        thingIconField.sprite = data.Icon;
    }

    public void OnThingPressed()
    {
        SoundEngine.PlayAudio("item_change");
        clothesManager.clothesData = thingClothesData;
        if (PlayerPrefs.HasKey(thingClothesData.Name))
        {
            thingClothesData.Cost = 0;
        }
        clothesManager.priceText.text = thingClothesData.Cost.ToString();
        if (thingClothesData.Cost == 0) clothesManager.priceText.text = "Apply";
        DataManager.instance.ClothesDatas.ApplyClothes(clothesOject, thingClothesData);
    }
}
