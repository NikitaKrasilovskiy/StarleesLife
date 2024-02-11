using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesCategoryButton : MonoBehaviour
{
    [SerializeField] private ClothesManager clothesManager;
    [SerializeField] private ClothesType clothesType;
    public void OnButtonPressed()
    {
        if (clothesManager == null) clothesManager = FindObjectOfType<ClothesManager>();
        clothesManager.ShowGrid(clothesType);
        SoundEngine.PlayAudio("click");
    }
}
