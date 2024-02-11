using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance { get; private set; }
    public enum ActionButtonType { Category, Action };

    [SerializeField] public ActionData actionData;
    [SerializeField] private Transform buttonsHolder;
    [SerializeField] private GameObject actionButtonPrefab;
    [HideInInspector] public bool isActionActive;
    private string currentClipName;

    private void Start()
    {
        actionData = new ActionData();
        isActionActive = false;
        //Clear();
    }

    private void OnEnable()
    {
        SoundEngine.PlayAudio("windows");
        
        int _tutorialState = DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.Tutorial);
        if (_tutorialState == 23)
        {
            //SpawnActionButtons(ActionCategoryType.None);
        }
        else
        {
            SpawnActionButtons(ActionCategoryType.None);
        }
    }

    public void SpawnTutorial()
    {
        SpawnActionButtons(ActionCategoryType.Games);
    }
    
    public void SpawnActionButtons(ActionCategoryType categoryType)
    {
        Clear();

        List<ActionCategoryData> actionCategoryDatas = categoryType == ActionCategoryType.None
            ? DataManager.instance.ActionDatas.ActionCategories
            : new List<ActionCategoryData>();

        ActionCategoryData actionCategoryData = categoryType == ActionCategoryType.None
            ? new ActionCategoryData()
            : DataManager.instance.ActionDatas.ActionCategories.Find(a => a.CategoryType == categoryType);

        List<ActionData> actionDatas = categoryType == ActionCategoryType.None
            ? new List<ActionData>()
            : actionCategoryData.Actions;

        int count = categoryType == ActionCategoryType.None ? actionCategoryDatas.Count : actionDatas.Count;

        for (int i = 0; i < count; i++)
        {
            ActionCategoryData categoryData = categoryType == ActionCategoryType.None ? actionCategoryDatas[i] : actionCategoryData;
            ActionData actionData = categoryType == ActionCategoryType.None ? new ActionData() : actionDatas[i];

            if (categoryType != ActionCategoryType.None && DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.GrowLevel) < actionData.startGrowLevel) continue;

            ActionButton actionButton = Instantiate(actionButtonPrefab, buttonsHolder).GetComponent<ActionButton>();
            actionButton.Activate(this, categoryData, actionData);
        }
    }
    private void Clear()
    {
        for (int i = buttonsHolder.childCount - 1; i >= 0; i--)
        {
            Destroy(buttonsHolder.GetChild(i).gameObject);
        }
    }

    public async void PlayAction(ActionData newActionData)
    {
        if (actionData.actionType == ActionType.None)
        {
            actionData = newActionData;

            DataManager.instance.CommonDatas.CurrentActiveGameName = String.Empty;
            DataManager.instance.CommonDatas.CurrentMiniGameState = MiniGamesPanel.MiniGameState.None;
            Debug.Log("CheckMiniGame(actionData.gameName) - " + actionData.gameName + " - " + CheckMiniGame(actionData.gameName));

            if (CheckMiniGame(actionData.gameName))
            {
                MiniGamesPanel.instance.ActivateGame(actionData.gameName);
                await UniTask.WaitUntil(() => DataManager.instance.CommonDatas.CurrentMiniGameState != MiniGamesPanel.MiniGameState.None);
                await UniTask.Delay(TimeSpan.FromSeconds(2f));
            }
            else
            {
                DataManager.instance.CommonDatas.CurrentMiniGameState = MiniGamesPanel.MiniGameState.Victory;
            }

            await UniTask.WaitUntil(() => DataManager.instance.CommonDatas.CurrentMiniGameState != MiniGamesPanel.MiniGameState.None);
            UIManager.Instance.CloseMiniGamePanel();

            if (DataManager.instance.CommonDatas.CurrentMiniGameState == MiniGamesPanel.MiniGameState.Victory)
            {
                PlaySound(actionData.actionType);
                actionData.SetTime(true);
                isActionActive = true;

                MainScene.instance.girlAnimator.runtimeAnimatorController = actionData.UpdateAnimatorOverrideController();
                ActionDuration actionDuration = actionData.GetActionDuration();

                GirlManager.instance.ActivateItem(actionData.actionType);

                MainScene.instance.girlAnimator.SetBool("WithEnters", actionDuration.withEnters);
                MainScene.instance.girlAnimator.SetTrigger("PlayMoodAction");

                await UniTask.Delay(TimeSpan.FromSeconds(actionDuration.awaitDuration));
                MainScene.instance.girlAnimator.SetTrigger("QuitMoodAction");
                if (actionData.actionType == ActionType.Sport_RopeJumping || actionData.actionType == ActionType.Games_Ball)
                {
                    GirlManager.instance.girlActionItems.Find(o => o.ActionType == actionData.actionType).Items[0].GetComponent<Animator>().SetTrigger("Quit");
                }

                List<AnimationClip> clips = newActionData.GetAnimationClips();
                await UniTask.WaitUntil(() => !clips.Contains(Goryned.Core.System.GetAnimationClip(MainScene.instance.girlAnimator)));
                FindObjectOfType<MoodManager>().IncreaseMood();
                SoundEngine.DestroyAudioSource(currentClipName);
            }

            GirlManager.instance.ActivateItem(ActionType.None);
            currentClipName = string.Empty;

            actionData.SetTime(false);
            isActionActive = false;
            actionData = new ActionData();
        }
        else
        {
            Debug.Log(string.Format("Активна анимация: {0} - {1}", actionData.actionType, actionData.GetPercent(true)));
        }
    }

    private bool CheckMiniGame(string miniGameName)
    {
        if (!string.IsNullOrEmpty(miniGameName))
        {
            UIManager.Instance.OpenPanel(MainScenePanelType.MiniGamePanel);
            bool available = MiniGamesPanel.instance.CheckMiniGame(miniGameName);
            MiniGamesPanel.instance.gameObject.SetActive(available);
            return available;
        }
        return false;
    }

    private void PlaySound(ActionType actionType)
    {
        currentClipName = string.Empty;
        switch (actionType)
        {
            case ActionType.None:
                break;
            case ActionType.Care_ToothBrushing:
                currentClipName = SoundEngine.GetRandomAudioName(new List<string>() { "girl_teeth_1", "girl_teeth_2" });
                break;
            case ActionType.Care_HairCombing:
                currentClipName = SoundEngine.GetRandomAudioName(new List<string>() { "girl_comb_1", "girl_comb_2" });
                break;
            case ActionType.Care_FaceWashing:
                currentClipName = SoundEngine.GetRandomAudioName(new List<string>() { "girl_wash_1", "girl_wash_2" });
                break;
            case ActionType.Games_Doll:
                currentClipName = SoundEngine.GetRandomAudioName(new List<string>() { "girl_doll_1", "girl_doll_2" });
                break;
            case ActionType.Games_Piano:
                currentClipName = "girl_piano";
                break;
            case ActionType.Games_Ball:
                currentClipName = SoundEngine.GetRandomAudioName(new List<string>() { "girl_jerks_1", "girl_jerks_2" });
                break;
            case ActionType.Games_Soon:
                break;
            case ActionType.Learning_Book:
                currentClipName = SoundEngine.GetRandomAudioName(new List<string>() { "girl_book_1", "girl_book_2" });
                break;
            case ActionType.Learning_Counting:
                currentClipName = SoundEngine.GetRandomAudioName(new List<string>() { "girl_bills_1", "girl_bills_2" });
                break;
            case ActionType.Learning_Writing:
                currentClipName = SoundEngine.GetRandomAudioName(new List<string>() { "girl_book_1", "girl_book_2" });
                break;
            case ActionType.Learning_Painting:
                currentClipName = SoundEngine.GetRandomAudioName(new List<string>() { "girl_book_1", "girl_book_2" });
                break;
            case ActionType.Sport_RopeJumping:
                currentClipName = SoundEngine.GetRandomAudioName(new List<string>() { "girl_jerks_1", "girl_jerks_2" });
                break;
            case ActionType.Sport_Gymnastic:
                currentClipName = SoundEngine.GetRandomAudioName(new List<string>() { "girl_jerks_1", "girl_jerks_2" });
                break;
            case ActionType.Sport_PingPong:
                currentClipName = SoundEngine.GetRandomAudioName(new List<string>() { "girl_jerks_1", "girl_jerks_2" });
                break;
            case ActionType.Sport_Ganteli:
                currentClipName = SoundEngine.GetRandomAudioName(new List<string>() { "girl_jerks_1", "girl_jerks_2" });
                break;
            default:
                break;
        }
        if (!string.IsNullOrEmpty(currentClipName)) SoundEngine.PlayAudio(currentClipName, true);
    }

    private void ActionNotAvailable(GameObject button, int startGrowLevel)
    {
        Destroy(button);
    }
}
