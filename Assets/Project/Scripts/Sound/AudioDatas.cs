using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

[CreateAssetMenu(fileName = "AudioDatas", menuName = "StarleesLife/AudioDatas", order = 6)]
public class AudioDatas : ScriptableObjectInstaller<AudioDatas>
{
    public enum AudioType { Sound, Music };

    public override void InstallBindings()
    {
        Container.BindInstance(this).AsSingle();
    }

    [Header("Все звуки")]
    [SerializeField] private List<AudioClip> audio;

    [Header("Микшер")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup soundMixerGroup;
    [SerializeField] private AudioMixerGroup musicMixerGroup;

    public AudioClip GetAudio(string audioName)
    {
        return audio.Find(a => a.name == audioName);
    }

    public AudioMixerGroup GetAudioMixerGroup(AudioType audioType)
    {
        return audioType == AudioType.Sound ? soundMixerGroup : musicMixerGroup;
    }

    public void UpdateVolume(AudioType audioType, float volume)
    {
        string volumeParameter = audioType == AudioType.Sound ? "SoundVolume" : "MusicVolume";
        int convertedVolume = (int)((volume * 90) - 80);
        audioMixer.SetFloat(volumeParameter, convertedVolume);
    }
}
