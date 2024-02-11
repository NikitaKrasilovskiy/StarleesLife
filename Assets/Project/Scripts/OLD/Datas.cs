using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Datas", menuName = "StarleesLife/Datas", order = 10)]
public class Datas : ScriptableObjectInstaller<Datas>
{
    public GameData gameData;
    public ShopData shopData;

    public override void InstallBindings()
    {
        Container.BindInstance(gameData).AsSingle();
        Container.BindInstance(shopData).AsSingle();
    }
}