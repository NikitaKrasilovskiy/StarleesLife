using System.Collections;
using System.Collections.Generic;
//using Firebase.Analytics;
using UnityEngine;
using UnityEngine.Purchasing;

public class IapManager : MonoBehaviour
{
    private string hardCoinsKey = "HardCoins_";
    
    private string crystal100 = "hard_coin_100"; 
    private string crystal500 = "hardcoin_500"; 
    private string crystal1200 = "hard_coin_1200";
    private string crystal2500 = "hard_coin_2500";  
    private string crystal6500 = "hard_coin_6500";
    private string crystal14000 = "hard_coin_14000";
    
    public void OnPurchaseCompleted(Product product)
    {
        if (product.definition.id == crystal100)
        {
            int count = 100;
            DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.HardCoins, count);
        }
        else if (product.definition.id == crystal500)
        {
            int count = 500;
            DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.HardCoins, count);
        }
        else if (product.definition.id == crystal1200)
        {
            int count = 1200;
            DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.HardCoins, count);
        }
        else if (product.definition.id == crystal2500)
        {
            int count = 2500;
            DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.HardCoins, count);
        }
        else if (product.definition.id == crystal6500)
        {
            int count = 6500;
            DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.HardCoins, count);
        }
        else if (product.definition.id == crystal14000)
        {
            int count = 14000;
            DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.HardCoins, count);
        }
        
        //FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventPurchase);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason purchaseFailureReason)
    {
        Debug.Log(product.definition.id + " - " + purchaseFailureReason.ToString());
    }
}
