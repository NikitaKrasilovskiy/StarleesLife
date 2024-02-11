using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerDatas", menuName = "StarleesLife/PlayerDatas", order = 1)]
public class PlayerDatas : ScriptableObjectInstaller<PlayerDatas>
{
    public enum PlayerDataType { PlayerParameterType, BaseParameterType };
    public override void InstallBindings()
    {
        Container.BindInstance(this).AsSingle();
    }

    [Header("Данные игрока")]
    public string PlayerID;
    public string PlayerName;
    public string DeviceID;

    [Header("Статистика")]
    [SerializeField] private List<PlayerDataParameter> playerParametersList;
    private Dictionary<string, int> playerParameters;

    [Header("Базовые параметры")]
    [SerializeField] private List<PlayerDataParameter> baseParametersList;
    private Dictionary<string, int> baseParameters;

    #region Получение значений
    public Dictionary<string, int> GetDictionary(PlayerDataType playerDataType)
    {
        return playerDataType == PlayerDataType.PlayerParameterType ? playerParameters : baseParameters;
    }
    public int GetParameter(PlayerParameterType playerParameter)
    {
        return GetParameter(PlayerDataType.PlayerParameterType, playerParameter.ToString());
    }
    public int GetParameter(BaseParameterType baseParameter)
    {
        return GetParameter(PlayerDataType.BaseParameterType, baseParameter.ToString());
    }
    public int GetParameter(PlayerDataType playerDataType, string parameterType)
    {
        return GetDictionary(playerDataType).ContainsKey(parameterType) ? GetDictionary(playerDataType)[parameterType] : 0;
    }
    #endregion

    #region Изменение значений
    public void UpdateDictionary(PlayerDataType playerDataType, Dictionary<string, int> dictionary)
    {
        switch (playerDataType)
        {
            case PlayerDataType.PlayerParameterType:
                playerParameters = dictionary;
                playerParametersList = DictionaryToList(dictionary);
                break;
            case PlayerDataType.BaseParameterType:
                baseParameters = dictionary;
                baseParametersList = DictionaryToList(dictionary);
                break;
            default:
                break;
        }
    }
    public void UpdatePlayerParameter(PlayerParameterType playerParameter, int newValue)
    {
        playerParameters[playerParameter.ToString()] = newValue;
        UpdateDictionary(PlayerDataType.PlayerParameterType, playerParameters);
    }
    public void IncreasePlayerParameter(PlayerParameterType playerParameter, int addValue)
    {
        int newValue = GetParameter(playerParameter) + addValue;
        UpdatePlayerParameter(playerParameter, newValue);
    }
    #endregion

    private List<PlayerDataParameter> DictionaryToList(Dictionary<string, int> dictionary)
    {
        List<PlayerDataParameter> playerDataParameters = new List<PlayerDataParameter>();
        foreach (var key in dictionary.Keys)
        {
            playerDataParameters.Add(new PlayerDataParameter() { Name = key, Value = dictionary[key] });
        }
        return playerDataParameters;
    }
}

[Serializable]
public struct PlayerDataParameter
{
    public string Name;
    public int Value;
}