using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject MiniGamesLevel2, textWin, textLose;
    public GameObject[] obj;
    public TextMeshProUGUI textErrors, textMoves;
    public int a, i;
    private RandObject randObject;
    private void Start()
    { randObject = FindObjectOfType<RandObject>(); }

    void Update()
    {
        textErrors.text = (3 + i).ToString();
        textMoves.text = (9 - a).ToString();

        if (a >= 9)
        {
            textWin.SetActive(true);
            Invoke("OffOnPrefab", 2.0f);
        }

        if (i <= -3)
        {
            textLose.SetActive(true);
            Invoke("OffOnPrefab", 2.0f);
        }
    }
    private void OffOnPrefab()
    {
        a = 0;
        i = 0;
        textLose.SetActive(false);
        textWin.SetActive(false);

        for (int i = 0; i < obj.Length; i++)
        { obj[i].SetActive(true); }

        randObject.NextStart();
        MiniGamesLevel2.SetActive(false);
    }
}