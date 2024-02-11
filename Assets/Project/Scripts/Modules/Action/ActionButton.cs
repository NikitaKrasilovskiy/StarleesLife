using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [Header("ActionManager")]
    [SerializeField] private ActionManager actionManager;

    [Header("Действие")]
    [SerializeField] private ActionCategoryData actionCategoryData;
    [SerializeField] private ActionData actionData;
    [SerializeField] private float percent;

    [Header("Поля")]
    [SerializeField] private Image mainImage;
    [SerializeField] private Image fillingImage;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text percentText;

    public void Activate(ActionManager actionManager, ActionCategoryData actionCategoryData, ActionData actionData)
    {
        this.actionManager = actionManager;
        this.actionCategoryData = actionCategoryData;
        this.actionData = actionData;

        SetImage(actionData.actionSprite == null ? actionCategoryData.CategoryIcon : actionData.actionSprite);
    }

    public void OnPressed()
    {
        SoundEngine.PlayAudio("click");
        if (string.IsNullOrEmpty(actionData.actionName)) actionManager.SpawnActionButtons(actionCategoryData.CategoryType);
        else actionManager.PlayAction(actionData);
    }

    private void SetImage(Sprite sprite, float percent = 1)
    {
        mainImage.sprite = sprite;
        fillingImage.sprite = sprite;
        fillingImage.fillAmount = percent;
    }

    public float Percent
    { 
        get => percent; 
        set
        {
            percent = value;
            fillingImage.fillAmount = value;
            button.interactable = value >= 1;
            if (percentText != null) percentText.text = Mathf.RoundToInt(value * 100).ToString();
        } 
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (actionData.actionType != ActionType.None)
        {
            bool activeAction = actionData.actionType == actionManager.actionData.actionType;
            Percent = actionData.GetPercent(activeAction);
            fillingImage.fillClockwise = activeAction;
        }
    }
}
