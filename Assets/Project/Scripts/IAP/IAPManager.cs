using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    public IAPButton _iapButton;
    [SerializeField] private ShopItem _shopItem;
    

    void OnEnable()
    {
        //_iapButton.productId = _shopItem.GetComponent<ShopItem>().ShopItemData.ID;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //_shopItem.GetComponent<ShopItem>().ShopItemData.ID;
    }
}
