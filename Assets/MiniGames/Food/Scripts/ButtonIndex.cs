using UnityEngine;
using UnityEngine.UI;

public class ButtonIndex : MonoBehaviour
{
    private RandomFoodDay scriptRandomFoodDay;
    void Start()
    { scriptRandomFoodDay = FindObjectOfType<RandomFoodDay>(); }
    public void Examination()
    {
        if (scriptRandomFoodDay.imageFoodDay[1].sprite == GetComponent<Image>().sprite ||
            scriptRandomFoodDay.imageFoodDay[2].sprite == GetComponent<Image>().sprite ||
            scriptRandomFoodDay.imageFoodDay[3].sprite == GetComponent<Image>().sprite)
        { scriptRandomFoodDay.a++; GetComponent<Image>().sprite = scriptRandomFoodDay.spritesDesignation[0]; }

        else { scriptRandomFoodDay.i--; GetComponent<Image>().sprite = scriptRandomFoodDay.spritesDesignation[1]; }
    }
}
