using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;
using Random = UnityEngine.Random;

public static class SoundEngine
{
    private static List<GameObject> activeAudioObjects;

    private static string GetAudioObjectName(string name)
    {
        return "Audio_" + name;
    }
    public static string GetRandomAudioName(List<string> names)
    {
        return names[Random.Range(0, names.Count)];
    }
    public static async UniTask<bool> PlayAudio(string name, bool loop = false, AudioDatas.AudioType audioType = AudioDatas.AudioType.Sound)
    {
        Debug.Log(GetAudioObjectName(name));
        AudioSource audioSource = new GameObject(GetAudioObjectName(name)).AddComponent<AudioSource>();
        audioSource.transform.position = Vector3.zero;
        audioSource.playOnAwake = false;
        audioSource.loop = loop;

        AudioClip audioClip = DataManager.instance.AudioDatas.GetAudio(name);
        if (audioClip == null || !audioClip)
        {
            Debug.Log("AudioClip is null: " + name);
            return false;
        }

        audioSource.clip = audioClip;
        audioSource.outputAudioMixerGroup = DataManager.instance.AudioDatas.GetAudioMixerGroup(audioType);
        audioSource.Play();

        //AddAudioSource(audioSource.gameObject);

        if (audioSource)
            await UniTask.WaitUntil(() => audioSource.isPlaying);
        try
        {
            if (audioSource)
                await UniTask.WaitUntil(() => audioSource && !audioSource.isPlaying);
            DestroyAudioSource(audioSource.name);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }

        return true;
    }

    public static void AddAudioSource(GameObject audioObject)
    {
        if (activeAudioObjects == null) activeAudioObjects = new List<GameObject>();
        activeAudioObjects.Add(audioObject);
    }
    public static void DestroyAudioSource(string audioName)
    {
        if (!audioName.Contains("Audio")) audioName = GetAudioObjectName(audioName);
        GameObject audioObject = GameObject.Find(audioName);
        GameObject.Destroy(audioObject);
    }
}

