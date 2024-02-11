using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MenuButton : MonoBehaviour
{
    [Header("Поля")]
    [SerializeField] private Image imageField;
    [SerializeField] private List<MenuButton> menuButtons;

    [Header("Параметры")]
    [SerializeField] private MenuButtonType type;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject children;

    private void Awake()
    {
        if (imageField == null) imageField = GetComponent<Image>();
    }

    void Start()
    {
    }

    private void OnEnable()
    {
        SetSelected(false);
    }
    private void OnDisable()
    {
        SetSelected(false);
    }

    void Update()
    {

    }

    private void SetSelected(bool selected)
    {
        imageField.sprite = selected ? sprites[1] : sprites[0];
        if (children != null) children.SetActive(selected);
        transform.localScale = Vector3.one * (selected ? 1.2f : 1f);
    }

    public void OnPressed()
    {
        menuButtons.ForEach(button => button.SetSelected(false));
        SetSelected(true);
    }
}

public enum MenuButtonType
{
    None = 0,
    Actions = 1,
    Clothes = 2,
    Furniture = 3,
    Upgrades = 4,
}