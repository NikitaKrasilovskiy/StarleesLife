using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomFoodDay : MonoBehaviour
{
    public Sprite[] spritesFood, spritesEggs, spritesPizza, spritesPounded, spritesShawarma, spritesSoup, spritesIngridient, spritesDesignation;
    public Image[] imageFoodDay;
    public TextMeshProUGUI textFood;
    public GameObject objMenu, win, lose, gameFood;
    public List<GameObject> transObj;
    public int randomFoodDay;
    [HideInInspector] public int a, i;
    void OnEnable()
    {
        objMenu.SetActive(true);
        Rand();
        FoodDay();
        imageFoodDay[0].sprite = spritesFood[randomFoodDay];
        StartCoroutine(Wait());
    }
    void Update()
    {
        if (a >= 3)
        { win.SetActive(true); Invoke("OffOnPrefab", 2.0f); }

        if (i <= -2)
        { lose.SetActive(true); Invoke("OffOnPrefab", 2.0f); }
    }
    private void Rand()
    { randomFoodDay = UnityEngine.Random.Range(0, 5); }
    private void FoodDay()
    {
        if (randomFoodDay == 0)
        {
            textFood.text = "ßè÷íèöà";
            for (int i = 1; i <= 3; i++)
            { imageFoodDay[i].sprite = spritesEggs[i-1]; }
        }

        if (randomFoodDay == 1)
        {
            textFood.text = "Ïèööà";
            for (int i = 1; i <= 3; i++)
            { imageFoodDay[i].sprite = spritesPizza[i - 1]; }
        }

        if (randomFoodDay == 2)
        {
            textFood.text = "Ïþðå";
            for (int i = 1; i <= 3; i++)
            { imageFoodDay[i].sprite = spritesPounded[i - 1]; }
        }

        if (randomFoodDay == 3)
        {
            textFood.text = "Øàóðìà";
            for (int i = 1; i <= 3; i++)
            { imageFoodDay[i].sprite = spritesShawarma[i - 1]; }
        }

        if (randomFoodDay == 4)
        {
            textFood.text = "Ñóï";
            for (int i = 1; i <= 3; i++)
            { imageFoodDay[i].sprite = spritesSoup[i - 1]; }
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4f);
        objMenu.SetActive(false);

        var array = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        var random = new System.Random(DateTime.Now.Millisecond);
        array = array.OrderBy(x => random.Next()).ToArray();

        for (int i = 4; i <= 12; i++)
        { imageFoodDay[i].sprite = spritesIngridient[array[i-4]]; }
    }
    private void OffOnPrefab()
    {
        a = 0;
        i = 0;
        win.SetActive(false);
        lose.SetActive(false);
        gameFood.SetActive(false);
    }
}
