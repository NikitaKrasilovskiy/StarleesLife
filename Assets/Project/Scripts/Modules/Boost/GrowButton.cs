using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GrowButton : MonoBehaviour
{
    [Header("Константы")]
    private readonly PlayerParameterType parameterType = PlayerParameterType.GrowLevel;
    private readonly int stepsForGrowth = 12;
    private readonly int startGrowth = 3;


    [Header("Поля")]
    [SerializeField] private Image smilesFiller;
    [SerializeField] private Image growLevelFiller;
    [SerializeField] private Button growButton;
    [SerializeField] private TMP_Text growthText;
    [SerializeField] private GrowLevelPrizes growLevelPrizes;
    private List<Image> images;

    [Header("Данные")]
    [SerializeField] private BoostData boostData;
    [SerializeField] private int growLevel;
    [SerializeField] private int growth;

    public int GrowLevel
    {
        get
        {
            growLevel = DataManager.instance.PlayerDatas.GetParameter(parameterType);
            return growLevel;
        }
        set
        {
            growLevel = value;
            DataManager.instance.PlayerDatas.UpdatePlayerParameter(parameterType, value);

            Growth = value / stepsForGrowth;
            growLevelFiller.fillAmount = (value % stepsForGrowth) / (float)stepsForGrowth;
            if (growLevelFiller.fillAmount == 0 || growLevelFiller.fillAmount == 1)
            {
                GirlManager.instance.PlayParticleSystem();
            }
        }
    }
    public int Growth
    {
        get => growth;
        set
        {
            growth = value + startGrowth;
            growthText.text = growth.ToString();
        }
    }

    private void Awake()
    {
        images = transform.GetComponentsInChildren<Image>().ToList<Image>();
        images.Remove(GetComponent<Image>());
        growLevelPrizes.gameObject.SetActive(false);
    }
    public void Start()
    {

    }
    private void OnEnable()
    {
        UpdateInformation();
    }

    public void Update()
    {
        CheckAvailable();
    }
    private void UpdateInformation()
    {
        boostData = DataManager.instance.CreateBoostData(parameterType);
        GrowLevel = boostData.level;
        //Debug.Log(JsonUtility.ToJson(boostData));
    }
    private void CheckAvailable()
    {
        float percentOfSmiles = DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.Smiles) / (float)boostData.value;
        smilesFiller.fillAmount = percentOfSmiles;
        //bool enoughSmiles = DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.Smiles) >= boostData.value;
        //bool enoughStars = DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.Stars) >= boostData.cost;
        //bool available = enoughSmiles && enoughStars;
        bool available = percentOfSmiles >= 1;

        growButton.interactable = available;
        growthText.color = available ? Color.green : Color.red;
        //Color imageColor = available ? Color.white : new Color(0.5f, 0.5f, 0.5f);
        //images.ForEach(image => image.color = imageColor);
    }

    public void OnGrowButtonPressed()
    {
        DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.Smiles, -boostData.value);
        //DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.Stars, -boostData.cost);
        DataManager.instance.PlayerDatas.IncreasePlayerParameter(parameterType, 1);
        UpdateInformation();
        SoundEngine.PlayAudio("lelel_up");

        List<Sprite> sprites = DataManager.instance.GetIconsByGrowLevel(DataManager.instance.PlayerDatas.GetParameter(parameterType));
        if (sprites != null && sprites.Count > 0) growLevelPrizes.Activate(sprites);
    }
}
