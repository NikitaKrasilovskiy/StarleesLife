using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiniGamesPanel : MonoBehaviour
{
    public enum MiniGameState { None, Defeat, Victory }

    public static MiniGamesPanel instance;

    public List<GameObject> miniGames;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        miniGames.ForEach(x => x.SetActive(false));
    }
    private void OnDisable()
    {
        miniGames.ForEach(x => x.SetActive(false));
    }

    public bool CheckMiniGame(string gameName)
    {
        return miniGames.Find(g => g.name == gameName);
    }

    public void ActivateGame(string gameName)
    {
        miniGames.Find(g => g.name == gameName).SetActive(true);
    }

    public void SetVictory()
    {
        SetState(MiniGameState.Victory);
    }

    public void SetDefeat()
    {
        SetState(MiniGameState.Defeat);
    }

    public void SetState(MiniGameState miniGameState)
    {
        DataManager.instance.CommonDatas.CurrentMiniGameState = miniGameState;
    }
}
