using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBlock : MonoBehaviour
{
    [Header("Text Fields")]
    [SerializeField] private TMP_Text labelText;

    [Header("Image Fields")]
    [SerializeField] private Image labelImage;
    [SerializeField] private Image blockImage;

    [Header("GridLayoutGroup")]
    [SerializeField] private GridLayoutGroup grid;
    private readonly Dictionary<int, Vector2> cellSizes = new Dictionary<int, Vector2>()
    {
        { 0, new Vector2(320, 320)},
        { 1, new Vector2(600, 600)},
        { 2, new Vector2(450, 450)},
        { 3, new Vector2(320, 450)},
        { 4, new Vector2(400, 320)},
        { 5, new Vector2(320, 320)},
        { 6, new Vector2(320, 320)},
    };

    private ShopBlockData shopBlockData;

    public ShopBlockData ShopBlockData
    { 
        get => shopBlockData; 
        set
        {
            shopBlockData = value;
            labelText.text = value.blockName;
            labelImage.sprite = DataManager.instance.shopData.shopLabelImages.Find(i => i.color == value.labelColor).sprite;
            blockImage.sprite = DataManager.instance.shopData.shopBlockImages.Find(i => i.color == value.blockColor).sprite;
            SpawnItems();
        } 
    }

    private void SpawnItems()
    {
        ClearGrid();
        int count = cellSizes.ContainsKey(ShopBlockData.itemDatas.Count) ? ShopBlockData.itemDatas.Count : 0;
        grid.cellSize = cellSizes[count];
        foreach (var item in ShopBlockData.itemDatas)
        {
            Instantiate(ShopManager.Instance.shopItemObject, grid.transform).GetComponent<ShopItem>().Activate(item);
        }
    }

    private void ClearGrid()
    {
        var children = new List<GameObject>();
        foreach (Transform child in grid.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }
}
