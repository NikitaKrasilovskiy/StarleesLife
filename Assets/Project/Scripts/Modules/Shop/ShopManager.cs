using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [SerializeField] private Transform holder;
    [SerializeField] public GameObject shopBlockObject;
    [SerializeField] public GameObject shopItemObject;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        SoundEngine.PlayAudio("windows");
        RespawnItems();
    }
    private void RespawnItems()
    {
        for (int i = holder.childCount - 1; i >= 0; i--) Destroy(holder.GetChild(i).gameObject);

        foreach (var item in DataManager.instance.shopData.shopBlockDatas)
        {
            ShopBlock shopBlock = Instantiate(shopBlockObject, holder).GetComponent<ShopBlock>();
            shopBlock.ShopBlockData = item;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }


}
