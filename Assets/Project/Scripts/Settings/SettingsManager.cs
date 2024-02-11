using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    private readonly string key = "SettingsDataKey";
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private SettingsData settingsData;

    public SettingsData SettingsData
    { 
        get
        {
            if (!PlayerPrefs.HasKey(key)) SettingsData = new SettingsData() { SoundValue = 0.8f, MusicValue = 0.8f, VibrationValue = 0.5f };
            string strData = PlayerPrefs.GetString(key);
            settingsData = JsonUtility.FromJson<SettingsData>(strData);
            return settingsData;
        }
        set
        {
            settingsData = value;
            string strData = JsonUtility.ToJson(value);
            PlayerPrefs.SetString(key, strData);
            Debug.Log("SET SETTINGSDATA = " + strData);
            DataManager.instance.AudioDatas.UpdateVolume(AudioDatas.AudioType.Sound, settingsData.SoundValue);
            DataManager.instance.AudioDatas.UpdateVolume(AudioDatas.AudioType.Music, settingsData.MusicValue);
        }
    }

    private void Awake()
    {
        SettingsData = SettingsData;
    }

    public void OnSoundValueChanged(float value)
    {
        settingsData.SoundValue = value;
        SettingsData = settingsData;
    }
    public void OnMusicValueChanged(float value)
    {
        settingsData.MusicValue = value;
        SettingsData = settingsData;
    }
    public void OnVibrationValueChanged(float value)
    {
        settingsData.VibrationValue = value;
        SettingsData = settingsData;
    }

    public void OnCloseButtonPressed()
    {
        settingsPanel.SetActive(false);
    }
}

[Serializable]
public struct SettingsData
{
    public float SoundValue;
    public float MusicValue;
    public float VibrationValue;
}