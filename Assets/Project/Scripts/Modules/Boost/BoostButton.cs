using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoostButton : MonoBehaviour
{
    [Header("BoostData")]
    [SerializeField] private PlayerParameterType boostType;
    [SerializeField] private BoostData boostData;

    [Header("����")]
    [SerializeField] private Button button;
    [SerializeField] private Image circleBackground;
    [SerializeField] private Image circleFiller;
    [SerializeField] private Image lineFiller;
    [SerializeField] private Image currencyIcon;
    [SerializeField] private TMP_Text infoTextField;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        SoundEngine.PlayAudio("windows");
        Activate();
    }

    void Update()
    {
        float balance = DataManager.instance.PlayerDatas.GetParameter(boostData.currencyType);
        float cost = boostData.cost;
        Debug.Log(string.Format("{0} - {1}/{2} = {3}", boostType, balance, cost, balance / cost));
        SetProgress(balance / cost);
    }

    private void Activate()
    {
        boostData = DataManager.instance.BoostDatas.boostDatas.Find(t => t.boostType == boostType);

        circleBackground.sprite = boostData.circleBackground;
        circleFiller.sprite = boostData.circleFiller;
        lineFiller.sprite = boostData.lineFiller;
        currencyIcon.sprite = boostData.currencyIcon;

        UpdateParameters();
    }

    private void UpdateParameters()
    {
        boostData = boostData.UpdateParameters();

        infoTextField.text = string.Format("{0}\n{1}", boostData.boostName, boostData.value);
    }

    private void SetProgress(float progress)
    {
        circleFiller.fillAmount = progress;
        lineFiller.fillAmount = progress;
        button.interactable = progress >= 1 && DataManager.instance.PlayerDatas.GetParameter(boostData.currencyType) >= boostData.cost;
    }

    public void OnBoostButtonPressed()
    {
        Debug.Log("OnBoostButtonPressed - " + JsonUtility.ToJson(boostData));
        DataManager.instance.PlayerDatas.IncreasePlayerParameter(boostData.currencyType, -boostData.cost);
        DataManager.instance.PlayerDatas.IncreasePlayerParameter(boostData.boostType, 1);
        UpdateParameters();
        SoundEngine.PlayAudio("shop_upgrade_done");
    }
}
