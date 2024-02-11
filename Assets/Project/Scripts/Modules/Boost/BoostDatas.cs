using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BoostDatas", menuName = "StarleesLife/BoostDatas", order = 4)]
public class BoostDatas : ScriptableObjectInstaller<BoostDatas>
{
    public override void InstallBindings()
    {
        Container.BindInstance(this).AsSingle();
    }

    public List<BoostData> boostDatas;
}