using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Goryned
{
    namespace UI
    {
        [Serializable]
        public struct UIElement
        {
            [Header("Поля")]
            [SerializeField] private TMP_Text textField;
            [SerializeField] private Image imageField;
            [SerializeField] private Button button;

            public void SetText(string strValue)
            {
                textField.text = strValue;
            }
            public void SetSprite(Sprite sprite)
            {
                imageField.sprite = sprite;
            }
            public void SetInteractable(bool interactable)
            {
                button.interactable = interactable;
            }

            public void Set(string value, bool interactable)
            {
                SetText(value);
                SetInteractable(interactable);
            }
        }
    }
}
