using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CommonDatas", menuName = "StarleesLife/CommonDatas", order = 2)]
public class CommonDatas : ScriptableObjectInstaller<CommonDatas>
{
    public override void InstallBindings()
    {
        Container.BindInstance(this).AsSingle();
    }

    [Header("Äåâî÷êà")]
    public AnimatorOverrideController GirlAnimatorOverrideController;

    [Header("Ìèíèèãðà")]
    public string CurrentActiveGameName;
    public MiniGamesPanel.MiniGameState CurrentMiniGameState;
}