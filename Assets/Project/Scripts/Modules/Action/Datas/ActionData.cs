using Goryned.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ActionData
{
    [Header("Äåéñòâèå")]
    public string actionName;
    public ActionType actionType;
    public Sprite actionSprite;
    public int startGrowLevel;
    public string gameName;

    [Header("Àíèìàöèè")]
    public AnimationClip enterClip;
    public AnimationClip mainClip;
    public AnimationClip exitClip;

    [Header("Ïàðàìåòðû")]
    public int actionCost;
    public float durationInMinutes;
    public float pauseInMinutes;

    public AnimatorOverrideController UpdateAnimatorOverrideController()
    {
        AnimatorOverrideController controller = DataManager.instance.CommonDatas.GirlAnimatorOverrideController;
        controller["MoodAction_Enter"] = enterClip;
        controller["MoodAction_Main"] = mainClip;
        controller["MoodAction_Exit"] = exitClip;
        return controller;
    }

    public ActionDuration GetActionDuration()
    {
        if (DataManager.instance.ActionDatas.defaultDuration >= 0) durationInMinutes = DataManager.instance.ActionDatas.defaultDuration;
        float fullRemainingDuration = durationInMinutes * 60 < GetMainDuration() ? GetMainDuration() : durationInMinutes * 60;
        float entersDuration = GetEnterDuration() + GetExitDuration();
        float mainClipRemainingDuration = fullRemainingDuration - entersDuration;
        int loopCount = Mathf.RoundToInt(mainClipRemainingDuration / GetMainDuration());

        float fullDuration = entersDuration + (GetMainDuration() * loopCount);
        float awaitDuration = fullDuration - (2 * GetExitDuration());

        ActionDuration actionDuration = new ActionDuration();
        actionDuration.fullDuration = fullDuration;
        actionDuration.awaitDuration = awaitDuration;
        actionDuration.withEnters = entersDuration > 0;
        Debug.Log(string.Format("{0} - {1}", actionType, JsonUtility.ToJson(actionDuration)));
        return actionDuration;
    }

    public float GetActionPercent(DateTime startTime, float actionDuration)
    {
        TimeSpan pastTime = DateTime.UtcNow - startTime;
        float actionPercent = (float)pastTime.TotalSeconds / actionDuration;
        return actionPercent;
    }
    public void SetTime(bool action)
    {
        if (DataManager.instance.ActionDatas.defaultPause >= 0) pauseInMinutes = DataManager.instance.ActionDatas.defaultPause;
        string startTimeKey = action ? DataManager.instance.ActionDatas.moodAction_ActionStartTime : DataManager.instance.ActionDatas.moodAction_PauseStartTime;
        string endTimeKey = action ? DataManager.instance.ActionDatas.moodAction_ActionEndTime : DataManager.instance.ActionDatas.moodAction_PauseEndTime;
        float addSeconds = action ? GetActionDuration().fullDuration : pauseInMinutes * 60;
        
        DateTimeManager.SaveDateTime(actionType.ToString() + startTimeKey);
        DateTimeManager.SaveDateTime(actionType.ToString() + endTimeKey, DateTimeManager.GetDateTime(addSeconds));
    }
    public float GetPercent(bool action)
    {
        string startTimeKey = action ? DataManager.instance.ActionDatas.moodAction_ActionStartTime : DataManager.instance.ActionDatas.moodAction_PauseStartTime;
        string endTimeKey = action ? DataManager.instance.ActionDatas.moodAction_ActionEndTime : DataManager.instance.ActionDatas.moodAction_PauseEndTime;

        DateTime startTime = DateTimeManager.GetDateTime(actionType.ToString() + startTimeKey);
        DateTime endTime = DateTimeManager.GetDateTime(actionType.ToString() + endTimeKey);

        TimeSpan pastTime = DateTime.UtcNow - startTime;
        TimeSpan fullTime = endTime - startTime;

        float percent = (float)(pastTime.TotalSeconds / fullTime.TotalSeconds);
        
        return percent;
    }
    public float GetEnterDuration()
    {
        return enterClip != null ? enterClip.length : 0;
    }
    public float GetMainDuration()
    {
        return mainClip != null ? mainClip.length : 0;
    }
    public float GetExitDuration()
    {
        return exitClip != null ? exitClip.length : 0;
    }

    public List<AnimationClip> GetAnimationClips()
    {
        List<AnimationClip> clips = new List<AnimationClip>();
        if (enterClip != null) clips.Add(enterClip);
        if (mainClip != null) clips.Add(mainClip);
        if (exitClip != null) clips.Add(exitClip);
        return clips;
    }
}

[Serializable]
public struct ActionDuration
{
    public float fullDuration;
    public float awaitDuration;
    public bool withEnters;
}
