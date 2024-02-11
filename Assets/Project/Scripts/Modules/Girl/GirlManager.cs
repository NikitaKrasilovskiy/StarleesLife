using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlManager : MonoBehaviour
{
    public static GirlManager instance;

    [Header("Предметы девочки для действий")]
    [SerializeField] public List<GirlActionItemData> girlActionItems;

    [Header("Одежда девочки")]
    [SerializeField] public Transform body;
    [SerializeField] public Transform boots;
    [SerializeField] public Transform hair;
    [SerializeField] public Transform accessory;

    [Header("Эффект взросления")]
    [SerializeField] private ParticleSystem growParticleSystem;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ActivateItem(ActionType.None);
        ApplyClothes();
    }

    public void ActivateItem(ActionType actionType)
    {
        foreach (var actionDatas in girlActionItems)
        {
            foreach (var item in actionDatas.Items)
            {
                if (item)
                {
                    item.SetActive(actionDatas.ActionType == actionType && actionType != ActionType.None);
                }
            }
        }
    }

    public void ApplyClothes()
    {
        DataManager.instance.ClothesDatas.ApplyClothes(body, DataManager.instance.ClothesDatas.GetThingClothesData(body, DataManager.instance.ClothesDatas.bodySet1[0]));
        DataManager.instance.ClothesDatas.ApplyClothes(hair, DataManager.instance.ClothesDatas.GetThingClothesData(hair, DataManager.instance.ClothesDatas.hairStyles_1[0]));
        DataManager.instance.ClothesDatas.ApplyClothes(boots, DataManager.instance.ClothesDatas.GetThingClothesData(boots, DataManager.instance.ClothesDatas.baletki[0]));
        DataManager.instance.ClothesDatas.ApplyClothes(accessory, DataManager.instance.ClothesDatas.GetThingClothesData(accessory, new ThingClothesData()));
    }

    public void PlayParticleSystem()
    {
        if (!growParticleSystem.gameObject.activeSelf)
            growParticleSystem.gameObject.SetActive(true);
        growParticleSystem.Play();
    }
}