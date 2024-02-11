using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ActionsDatas", menuName = "StarleesLife/ActionsDatas", order = 3)]
public class ActionsDatas : ScriptableObjectInstaller<ActionsDatas>
{
    public override void InstallBindings()
    {
        Container.BindInstance(this).AsSingle();
    }

    [Header("Îáùèå ïàðàìåòðû")]
    public float defaultDuration;
    public float defaultPause;

    [Header("Äåéñòâèÿ")]
    public List<ActionCategoryData> ActionCategories;

    [Header("Êëþ÷è äëÿ ñîõðàíåíèÿ")]
    public readonly string moodAction_ActionStartTime = "_ActionStartTime";
    public readonly string moodAction_ActionEndTime = "_ActionEndTime";
    public readonly string moodAction_PauseStartTime = "_PauseStartTime";
    public readonly string moodAction_PauseEndTime = "_PauseEndTime";

    public List<Sprite> GetIconsByGrowLevel(int growLevel)
    {
        List<Sprite> icons = new List<Sprite>();

        foreach(var category in ActionCategories)
        {
            foreach (var item in category.Actions)
            {
                if (item.startGrowLevel == growLevel) icons.Add(item.actionSprite);
            }
        }

        return icons;
    }
}