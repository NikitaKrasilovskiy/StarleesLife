using Goryned.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyViewer : MonoBehaviour
{
    [Header("Параметры")]
    [SerializeField] private PlayerParameterType parameterType;
    [SerializeField] private bool withReduce = true;
    [SerializeField] private int digits = 1;

    [Header("Поля")]
    [SerializeField] private TMP_Text textField;
    [SerializeField] private Image imageField;

    [Header("Значение")]
    [SerializeField] private int value;

    public int Value
    { 
        get
        {
            value = DataManager.instance.PlayerDatas.GetParameter(parameterType);
            return value;
        }
        set
        {
            this.value = value;
            DataManager.instance.PlayerDatas.UpdatePlayerParameter(parameterType, value);

            string strValue = withReduce ? Data.ReduceInt(value, digits) : value.ToString();
            if (textField) textField.text = strValue;
        } 
    }

    private void Awake()
    {

    }

    private void Start()
    {
        Value = Value;
    }

    private void Update()
    {
        Value = Value;
    }
}
