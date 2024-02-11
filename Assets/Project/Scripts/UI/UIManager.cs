using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private List<MainScenePanel> mainScenePanels;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenPanel(MainScenePanelType panelType)
    {
        foreach (var item in mainScenePanels)
        {
            item.panel.SetActive(item.panelType == panelType || item.panelType == MainScenePanelType.HomePanel);
        }
    }

    public void CloseMiniGamePanel()
    {
        mainScenePanels.Find(p => p.panelType == MainScenePanelType.MiniGamePanel).panel.SetActive(false);
    }
    public void OpenPanel(string panelName)
    {
        OpenPanel((MainScenePanelType)Enum.Parse(typeof(MainScenePanelType), panelName));
    }
}

[Serializable]
public struct MainScenePanel
{
    public MainScenePanelType panelType;
    public GameObject panel;
}

public enum MainScenePanelType
{
    HomePanel = 1,
    FortunePanel = 2,
    ShopPanel = 3,
    SettingsPanel = 4,
    MiniGamePanel = 5,
}