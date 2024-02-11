using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    public string soundName = "click";
    public bool loop = false;
    public AudioDatas.AudioType audioType;
    public void PlaySound()
    {
        SoundEngine.PlayAudio(soundName, loop, audioType);
    }
}
