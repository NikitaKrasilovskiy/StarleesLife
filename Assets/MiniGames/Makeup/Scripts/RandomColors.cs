using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RandomColors : MonoBehaviour
{
    public Sprite[] spritesColors, spriteOkError;
    public Image[] imageGirl, imageColors;
    public GameObject gameMakeup, buttons, win, lose;
    public GameObject[] others;
    public List<GameObject> transObj;
    public int randomColors;
    [HideInInspector] public int a, i;
    void OnEnable()
    {
        for (int i = 0; i < others.Length; i++)
        { others[i]. SetActive(true); }
            
        RandColors();
        StartCoroutine(Wait());
    }
    void Update()
    {
        if ( a >= 3 && i == -1 || a >= 4 )
        { win.SetActive(true); Invoke("OffOnPrefab", 2.0f); }

        if (i <= -2)
        { lose.SetActive(true); Invoke("OffOnPrefab", 2.0f); }
    }
    private void RandColors()
    {
        for (int i = 0; i < imageGirl.Length; i++)
        { imageGirl[i].sprite = spritesColors[UnityEngine.Random.Range(0, spritesColors.Length)]; }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4f);
        buttons.SetActive(true);

        for (int i = 0; i < imageColors.Length; i++)
        { imageColors[i].sprite = spritesColors[i]; }

        var array = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        var random = new System.Random(DateTime.Now.Millisecond);
        array = array.OrderBy(x => random.Next()).ToArray();

        for (int i = 0; i < imageColors.Length; i++)
        { imageColors[i].sprite = spritesColors[array[i]]; }
    }
    private void OffOnPrefab()
    {
        a = 0;
        i = 0;
        win.SetActive(false);
        lose.SetActive(false);
        buttons.SetActive(false);
        gameMakeup.SetActive(false);
    }
}
