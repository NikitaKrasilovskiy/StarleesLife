using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowLevelPrizes : MonoBehaviour
{
    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private Transform holder;

    public void Activate(List<Sprite> sprites)
    {
        for (int i = holder.childCount; i > 0; i--)
        {
            Destroy(holder.GetChild(i - 1).gameObject);
        }
        this.gameObject.SetActive(true);
        foreach (var sprite in sprites)
        {
            Image image = Instantiate(imagePrefab, holder).GetComponent<Image>();
            image.sprite = sprite;
        }
    }
}
