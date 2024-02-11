using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImage : MonoBehaviour
{
    public Sprite[] sprites, errorOk;
    public Image[] buttonImage, manicure;
    public GameObject obj, win, lose, gameManicure;
    [HideInInspector] public int a, i;
    void OnEnable()
    {
        for (int i = 0; i < manicure.Length; i++)
        { manicure[i].enabled = true; }
        
        ImageButton();

        StartCoroutine(Wait());
    }
    void Update()
    {
        if (a >= 3)
        { win.SetActive(true); Invoke("OffOnPrefab", 2.0f); }

        if (i <= -1)
        { lose.SetActive(true); Invoke("OffOnPrefab", 2.0f); }
    }
    private void ImageButton()
    {
        manicure[0].sprite = sprites[Random.Range(0, 3)];
        manicure[1].sprite = sprites[Random.Range(3, 6)];
        manicure[2].sprite = sprites[Random.Range(6, 9)];

        for (int i = 0; i < buttonImage.Length; i++)
        { buttonImage[i].sprite = sprites[i]; }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4f);
        obj.SetActive(true);

        for (int i = 0; i < manicure.Length; i++)
        { manicure[i].enabled = false; }
    }
    private void OffOnPrefab()
    {
        a = 0;
        i = 0;
        win.SetActive(false);
        lose.SetActive(false);
        obj.SetActive(false);
        gameManicure.SetActive(false);
    }
}
