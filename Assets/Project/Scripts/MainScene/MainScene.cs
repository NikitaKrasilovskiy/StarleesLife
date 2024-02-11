using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using Goryned.UI;

public class MainScene : MonoBehaviour
{
    public static MainScene instance;
    [SerializeField] public Animator girlAnimator;

    [Header("Скрипты")]
    [SerializeField] public SmilesManager smilesManager;
    [SerializeField] public StarsManager starsManager;

    private void Awake()
    {
        if (FindObjectOfType<DataManager>() == null) SceneManager.LoadScene(0);
        if (instance == null) instance = this;
        if (girlAnimator == null) girlAnimator = FindObjectOfType<Animator>();

        if (smilesManager == null) smilesManager = FindObjectOfType<SmilesManager>();
        if (starsManager == null) starsManager = FindObjectOfType<StarsManager>();
    }

    private void Start()
    {
        UIManager.Instance.OpenPanel(MainScenePanelType.HomePanel);
        DataManager.instance.StartUpdatingPlayerStatistics();
        SoundEngine.PlayAudio("Starlees_Life_OST", true, AudioDatas.AudioType.Music);
    }

    private void OnDisable()
    {
        SoundEngine.DestroyAudioSource("Starlees_Life_OST");
    }
}
