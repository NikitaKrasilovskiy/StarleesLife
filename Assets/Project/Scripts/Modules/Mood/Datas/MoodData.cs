using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MoodData
{
    [Header("����������")]
    public int Mood;

    [Header("������")]
    public Sprite SmileSprite;
    public GameObject SmileObject;

    [Header("��������")]
    public AnimationClip idleAnimationClip;
    public AnimationClip enterFromDownClip;
    public AnimationClip enterFromUpClip;

    public AnimatorOverrideController UpdateAnimatorOverrideController(bool fromDown)
    {
        AnimatorOverrideController controller = DataManager.instance.CommonDatas.GirlAnimatorOverrideController;
        controller["Idle"] = idleAnimationClip;
        controller["IDLE"] = idleAnimationClip;
        controller["Idle_Transition"] = fromDown ? enterFromDownClip : enterFromUpClip;
        return controller;
    }

    public bool HasTransition(bool fromDown)
    {
        AnimationClip animationClip = fromDown ? enterFromDownClip : enterFromUpClip;
        return animationClip != null;
    }
}
