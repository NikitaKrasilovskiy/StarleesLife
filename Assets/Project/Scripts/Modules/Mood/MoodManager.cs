using Cysharp.Threading.Tasks;
using Goryned.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MoodManager : MonoBehaviour
{
    [Header("Настроение")]
    [SerializeField] private int mood;
    [SerializeField] public MoodData moodData;

    [Header("Смайлик")]
    [SerializeField] private Transform smileHolder;
    [SerializeField] private GameObject smileObject;
    [SerializeField] private Image smileImage;

    [Header("IDLE Анимации")]
    [SerializeField] private List<string> idleAnimNames;

    private Coroutine moodFallingCoroutine;

    private int onlineMoodFallingTime { get => DataManager.instance.PlayerDatas.GetParameter(BaseParameterType.MoodFallingTime_Online); }
    private int offlineMoodFallingTime { get => DataManager.instance.PlayerDatas.GetParameter(BaseParameterType.MoodFallingTime_Offline); }


    public int Mood
    { 
        get
        {
            mood = DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.Mood);
            return mood;
        } 
        set
        {
            int oldMood = mood;
            mood = Mathf.Clamp(value, 1, 5);
            ChangeMood(mood, oldMood);
        } 
    }

    private void Awake()
    {
        idleAnimNames = DataManager.instance.MoodDatas.GetMoodAnimNames();
    }

    private void Start()
    {
        FixMood(true);

        Mood = Mood;
    }

    private void Update()
    {
        //Debug.Log("Anim - " + Goryned.Core.System.GetAnimationClipName(MainScene.instance.girlAnimator));
    }

    public async void ChangeMood(int newMood, int oldMood)
    {
        Debug.Log(string.Format("Change Mood: {0} -> {1}", oldMood, newMood));
        float moodDiff = newMood - oldMood;
        DataManager.instance.PlayerDatas.UpdatePlayerParameter(PlayerParameterType.Mood, newMood);
        moodData = DataManager.instance.MoodDatas.Moods.Find(d => d.Mood == newMood);
        MoodData oldData = DataManager.instance.MoodDatas.Moods.Find(d => d.Mood == oldMood);
        await UniTask.WaitUntil(() => idleAnimNames.Contains(Goryned.Core.System.GetAnimationClipName(MainScene.instance.girlAnimator)));
        MainScene.instance.girlAnimator.runtimeAnimatorController = moodData.UpdateAnimatorOverrideController(moodDiff >= 0);
        if (moodData.HasTransition(moodDiff >= 0)) MainScene.instance.girlAnimator.SetTrigger("ChangeMood");
        ShowSmile(moodData);
        Debug.Log(string.Format("Mood: {0} -> {1}", oldMood, newMood));


        if (moodDiff != 0)
        {
            SoundEngine.PlayAudio("smile");

            //if (newMood < 3) SoundEngine.PlayRandomEffect(new List<string>() { "girl_nervous_1", "girl_nervous_2" });
            //else if (newMood == 3) SoundEngine.PlayEffect("girl_dance");
            //else SoundEngine.PlayRandomEffect(new List<string>() { "girl_joy_1", "girl_joy_2" });
        }

        RestartMoodFalling();
    }

    private async void ShowSmile(MoodData moodData, float duration = 3f)
    {
        for (int i = smileHolder.childCount; i > 0; i--)
        {
            Destroy(smileHolder.GetChild(i - 1).gameObject);
        }
        smileObject = Instantiate(moodData.SmileObject, smileHolder);
        await UniTask.Delay(TimeSpan.FromSeconds(duration));
        Destroy(smileObject);
        /*
        if (smileImage == null)
        {
            for (int i = smileHolder.childCount; i > 0; i--)
            {
                Destroy(smileHolder.GetChild(i - 1).gameObject);
            }
            smileObject = Instantiate(moodData.SmileObject, smileHolder);
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            Destroy(smileObject);
        }
        else
        {
            Debug.Log("Activate Smile");
            smileObject.SetActive(true);
            smileImage.sprite = moodData.SmileSprite;
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            smileObject.SetActive(false);
            Debug.Log("DeActivate Smile");
        }
        */
    }

    public void IncreaseMood(bool plus = true, int value = 1)
    {
        Debug.Log(string.Format("Increase = {0} = {1}", plus, value));
        Mood = plus ? Mood + value : Mood - value;
    }

    public void TestChangeMood()
    {
        Mood = Mood + UnityEngine.Random.Range(-1, +2);
    }

    #region Автопадение настроения
    private void RestartMoodFalling()
    {
        if (moodFallingCoroutine != null) StopCoroutine(moodFallingCoroutine);
        moodFallingCoroutine = StartCoroutine(MoodFalling());
    }
    private IEnumerator MoodFalling()
    {
        yield return new WaitForSeconds(onlineMoodFallingTime);
        if (idleAnimNames.Contains(Goryned.Core.System.GetAnimationClipName(MainScene.instance.girlAnimator))) IncreaseMood(false, 1);
        else RestartMoodFalling();
    }
    #endregion

    #region Настроение после оффлайна
    private void FixMood(bool start)
    {
        string ExitTimeKey = "Mood-ExitTime";
        if (start)
        {
            DateTime exitTime = DateTimeManager.GetDateTime(ExitTimeKey);
            TimeSpan interval = DateTimeManager.GetInterval(exitTime);
            int decreaser = (int)interval.TotalSeconds / offlineMoodFallingTime;
            IncreaseMood(false, decreaser);

            Debug.Log(string.Format("{0} -> {1} = {2} ���. => {3}", exitTime, DateTime.UtcNow, interval, decreaser));

            FixMood(false);
        }
        else
        {
            DateTimeManager.SaveDateTime(ExitTimeKey);
            Debug.Log("Saved = " + DateTimeManager.GetDateTime(ExitTimeKey));
        }
    }
    private void OnApplicationQuit()
    {
        FixMood(false);
    }
    #endregion
}
