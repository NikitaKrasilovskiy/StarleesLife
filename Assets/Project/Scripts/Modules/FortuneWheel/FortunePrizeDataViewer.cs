using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FortunePrizeDataViewer : MonoBehaviour
{
    [Header("Приз")]
    [SerializeField] private FortunePrizeData prizeData;

    [Header("Поля")]
    [SerializeField] private TMP_Text prizeNameField;
    [SerializeField] private Image prizeImageField;
    [SerializeField] private TMP_Text prizeValueField;

    public FortunePrizeData PrizeData
    { 
        get => prizeData; 
        set
        {
            prizeData = value;
            if (prizeNameField) prizeNameField.text = value.prizeName;
            if (prizeImageField) prizeImageField.sprite = value.prizeSprite;
            if (prizeValueField) prizeValueField.text = value.value.ToString();
        } 
    }

    public void Activate(FortunePrizeData fortunePrizeData, bool showValue = false)
    {
        PrizeData = fortunePrizeData;
        prizeValueField.gameObject.SetActive(showValue && fortunePrizeData.value > 0);
        if (fortunePrizeData.value == 1) prizeValueField.text = "x1";
    }
}
