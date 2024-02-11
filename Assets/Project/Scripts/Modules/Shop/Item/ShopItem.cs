using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [Header("Image Fields")]
    [SerializeField] private Image itemImage;
    [SerializeField] private Image costImage;
    [SerializeField] private Image iapImage;

    [Header("Text Fields")]
    [SerializeField] private TMP_Text itemValue;
    [SerializeField] private TMP_Text itemCost;

    [Header("ShopItemData")]
    [SerializeField] private ShopItemData shopItemData;
        
    private AdManager _adManager;
    //public ShopItemData ShopItemData => shopItemData;

    public void Activate(ShopItemData data)
    {
        ShopItemData = data;
    }

    public ShopItemData ShopItemData
    { 
        get => shopItemData; 
        set
        {
            shopItemData = value;
            itemImage.sprite = value.sprite;
            costImage.gameObject.SetActive(value.cost > 0);
            iapImage.GetComponent<IAPManager>()._iapButton.productId = ShopItemData.ID;
            if (value.playerParameterType == PlayerParameterType.HardCoins)
            {
                iapImage.gameObject.SetActive(true); 
            }
            //iapImage.gameObject.SetActive(value.playerParameterType == PlayerParameterType.HardCoins);
            itemValue.text = value.value >= 2 ? value.value.ToString() : string.Empty;
            itemCost.text = value.cost.ToString();
        } 
    }

    public void OnBuyButtonPressed()
    {
        switch (ShopItemData.playerParameterType)
        {
            case PlayerParameterType.StarsLimit:
            case PlayerParameterType.StarsGrow_Online:
            case PlayerParameterType.StarsGrow_Offline:
            case PlayerParameterType.SmilesForTap:
                if (DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.HardCoins) < ShopItemData.cost) return;
                DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.HardCoins, -ShopItemData.cost);
                DataManager.instance.PlayerDatas.IncreasePlayerParameter(ShopItemData.playerParameterType, ShopItemData.value);
                SoundEngine.PlayAudio("shop_upgrade_count");
                break;
            case PlayerParameterType.Smiles:
                if (DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.HardCoins) < ShopItemData.cost) return;
                DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.HardCoins, -ShopItemData.cost);
                MainScene.instance.smilesManager.IncreaseSmilesFor(ShopItemData.value);
                SoundEngine.PlayAudio("click");
                break;
            case PlayerParameterType.HardCoins:
                
                break;
            case PlayerParameterType.FortuneSpins:
                UIManager.Instance.OpenPanel(MainScenePanelType.FortunePanel);
                SoundEngine.PlayAudio("click");
                _adManager = FindObjectOfType<AdManager>();
                _adManager.FortuneSceneIndex(1);
                break;
            default:
                break;
        }
    }
}