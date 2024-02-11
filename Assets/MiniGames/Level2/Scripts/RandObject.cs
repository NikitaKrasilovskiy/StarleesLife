using System.Collections.Generic;
using UnityEngine;

public class RandObject : MonoBehaviour
{
    public SpriteRenderer[] objectSprite;
    public Sprite[] spritesCategories, spriteAuto, spriteBed, spriteCupboard, spriteFridge, spriteHouse;
    public List<GameObject> trans;
    public MouseMove[] scr;
    private int randomDown, index1, index2, index3, index4, index5, index6;
    private bool _1 = true, _2 = true, _3 = true, _4 = true;
    void OnEnable()
    {
        _1 = true; _2 = true; _3 = true; _4 = true;

        RandomDown();
        objectSprite[0].sprite = spritesCategories[randomDown];
        index1 = randomDown;

        RandomDown();
        Proverka1();
        objectSprite[1].sprite = spritesCategories[randomDown];
        index2 = randomDown;

        RandomDown();
        Proverka2();
        objectSprite[2].sprite = spritesCategories[randomDown];
        index3 = randomDown;

        Cat1();

        Cat2();

        Cat3();

        Mix();
    }
    private void RandomDown()
    { randomDown = Random.Range(0, 5); }
    private void Proverka1()
    {
        if (randomDown == index1)
        {
            RandomDown();
            Proverka1();
        }
    }
    private void Proverka2()
    {
        if (randomDown == index1 || randomDown == index2)
        {
            RandomDown();
            Proverka2();
        }
    }
    private void Proverka3()
    {
        if (randomDown == index4)
        {
            RandomDown();
            Proverka3();
        }
    }
    private void Proverka4()
    {
        if (randomDown == index4 || randomDown == index5)
        {
            RandomDown();
            Proverka4();
        }
    }
    private void Cat1()
    {
        if(index1 == 0 && _1 == true)
        {
            for (int j = 0; j < 3; j++)
            {
                RandomDown();
                if (j == 0)
                {
                    index4 = randomDown;
                    objectSprite[j + 3].sprite = spriteHouse[index4];
                }
                else if (j == 1)
                {
                    Proverka3();
                    index5 = randomDown;
                    objectSprite[j + 3].sprite = spriteHouse[index5];
                }
                else if (j == 2)
                {
                    Proverka4();
                    index6 = randomDown;
                    objectSprite[j + 3].sprite = spriteHouse[index6];
                }
            }
            _1 = false;
        }
        else if (index1 == 1 && _1 == true)
        {
            for (int j = 0; j < 3; j++)
            {
                RandomDown();
                if (j == 0)
                {
                    index4 = randomDown;
                    objectSprite[j + 3].sprite = spriteAuto[index4];
                }
                if (j == 1)
                {
                    Proverka3();
                    index5 = randomDown;
                    objectSprite[j + 3].sprite = spriteAuto[index5];
                }
                if (j == 2)
                {
                    Proverka4();
                    index6 = randomDown;
                    objectSprite[j + 3].sprite = spriteAuto[index6];
                }
            }
            _1 = false;
        }
        else if (index1 == 2 && _1 == true)
        {
            for (int j = 0; j < 3; j++)
            {
                RandomDown();
                if (j == 0)
                {
                    index4 = randomDown;
                    objectSprite[j + 3].sprite = spriteBed[index4];
                }
                if (j == 1)
                {
                    Proverka3();
                    index5 = randomDown;
                    objectSprite[j + 3].sprite = spriteBed[index5];
                }
                if (j == 2)
                {
                    Proverka4();
                    index6 = randomDown;
                    objectSprite[j + 3].sprite = spriteBed[index6];
                }
            }
            _1 = false;
        }
        else if (index1 == 3 && _1 == true)
        {
            for (int j = 0; j < 3; j++)
            {
                RandomDown();
                if (j == 0)
                {
                    index4 = randomDown;
                    objectSprite[j + 3].sprite = spriteCupboard[index4];
                }
                if (j == 1)
                {
                    Proverka3();
                    index5 = randomDown;
                    objectSprite[j + 3].sprite = spriteCupboard[index5];
                }
                if (j == 2)
                {
                    Proverka4();
                    index6 = randomDown;
                    objectSprite[j + 3].sprite = spriteCupboard[index6];
                }
            }
            _1 = false;
        }
        else if (index1 == 4 && _1 == true)
        {
            for (int j = 0; j < 3; j++)
            {
                RandomDown();
                if (j == 0)
                {
                    index4 = randomDown;
                    objectSprite[j + 3].sprite = spriteFridge[index4];
                }
                if (j == 1)
                {
                    Proverka3();
                    index5 = randomDown;
                    objectSprite[j + 3].sprite = spriteFridge[index5];
                }
                if (j == 2)
                {
                    Proverka4();
                    index6 = randomDown;
                    objectSprite[j + 3].sprite = spriteFridge[index6];
                }
            }
            _1 = false;
        }
    }
    private void Cat2()
    {
        if (index2 == 0 && _2 == true)
        {
            for (int j = 0; j < 3; j++)
            {
                RandomDown();
                if (j == 0)
                {
                    index4 = randomDown;
                    objectSprite[j + 6].sprite = spriteHouse[index4];
                }
                else if (j == 1)
                {
                    Proverka3();
                    index5 = randomDown;
                    objectSprite[j + 6].sprite = spriteHouse[index5];
                }
                else if (j == 2)
                {
                    Proverka4();
                    index6 = randomDown;
                    objectSprite[j + 6].sprite = spriteHouse[index6];
                }
            }
            _2 = false;
        }
        else if (index2 == 1 && _2 == true)
        {
            for (int j = 0; j < 3; j++)
            {
                RandomDown();
                if (j == 0)
                {
                    index4 = randomDown;
                    objectSprite[j + 6].sprite = spriteAuto[index4];
                }
                if (j == 1)
                {
                    Proverka3();
                    index5 = randomDown;
                    objectSprite[j + 6].sprite = spriteAuto[index5];
                }
                if (j == 2)
                {
                    Proverka4();
                    index6 = randomDown;
                    objectSprite[j + 6].sprite = spriteAuto[index6];
                }
            }
            _2 = false;
        }
        else if (index2 == 2 && _2 == true)
        {
            for (int j = 0; j < 3; j++)
            {
                RandomDown();
                if (j == 0)
                {
                    index4 = randomDown;
                    objectSprite[j + 6].sprite = spriteBed[index4];
                }
                if (j == 1)
                {
                    Proverka3();
                    index5 = randomDown;
                    objectSprite[j + 6].sprite = spriteBed[index5];
                }
                if (j == 2)
                {
                    Proverka4();
                    index6 = randomDown;
                    objectSprite[j + 6].sprite = spriteBed[index6];
                }
            }
            _2 = false;
        }
        else if (index2 == 3 && _2 == true)
        {
            for (int j = 0; j < 3; j++)
            {
                RandomDown();
                if (j == 0)
                {
                    index4 = randomDown;
                    objectSprite[j + 6].sprite = spriteCupboard[index4];
                }
                if (j == 1)
                {
                    Proverka3();
                    index5 = randomDown;
                    objectSprite[j + 6].sprite = spriteCupboard[index5];
                }
                if (j == 2)
                {
                    Proverka4();
                    index6 = randomDown;
                    objectSprite[j + 6].sprite = spriteCupboard[index6];
                }
            }
            _2 = false;
        }
        else if (index2 == 4 && _2 == true)
        {
            for (int j = 0; j < 3; j++)
            {
                RandomDown();
                if (j == 0)
                {
                    index4 = randomDown;
                    objectSprite[j + 6].sprite = spriteFridge[index4];
                }
                if (j == 1)
                {
                    Proverka3();
                    index5 = randomDown;
                    objectSprite[j + 6].sprite = spriteFridge[index5];
                }
                if (j == 2)
                {
                    Proverka4();
                    index6 = randomDown;
                    objectSprite[j + 6].sprite = spriteFridge[index6];
                }
            }
            _2 = false;
        }
    }
    private void Cat3()
    {
        if (index3 == 0 && _3 == true)
        {
            for (int j = 0; j < 3; j++)
            {
                RandomDown();
                if (j == 0)
                {
                    index4 = randomDown;
                    objectSprite[j + 9].sprite = spriteHouse[index4];
                }
                else if (j == 1)
                {
                    Proverka3();
                    index5 = randomDown;
                    objectSprite[j + 9].sprite = spriteHouse[index5];
                }
                else if (j == 2)
                {
                    Proverka4();
                    index6 = randomDown;
                    objectSprite[j + 9].sprite = spriteHouse[index6];
                }
            }
            _3 = false;
        }
        else if (index3 == 1 && _3 == true)
        {
            for (int j = 0; j < 3; j++)
            {
                RandomDown();
                if (j == 0)
                {
                    index4 = randomDown;
                    objectSprite[j + 9].sprite = spriteAuto[index4];
                }
                if (j == 1)
                {
                    Proverka3();
                    index5 = randomDown;
                    objectSprite[j + 9].sprite = spriteAuto[index5];
                }
                if (j == 2)
                {
                    Proverka4();
                    index6 = randomDown;
                    objectSprite[j + 9].sprite = spriteAuto[index6];
                }
            }
            _3 = false;
        }
        else if (index3 == 2 && _3 == true)
        {
            for (int j = 0; j < 3; j++)
            {
                RandomDown();
                if (j == 0)
                {
                    index4 = randomDown;
                    objectSprite[j + 9].sprite = spriteBed[index4];
                }
                if (j == 1)
                {
                    Proverka3();
                    index5 = randomDown;
                    objectSprite[j + 9].sprite = spriteBed[index5];
                }
                if (j == 2)
                {
                    Proverka4();
                    index6 = randomDown;
                    objectSprite[j + 9].sprite = spriteBed[index6];
                }
            }
            _3 = false;
        }
        else if (index3 == 3 && _3 == true)
        {
            for (int j = 0; j < 3; j++)
            {
                RandomDown();
                if (j == 0)
                {
                    index4 = randomDown;
                    objectSprite[j + 9].sprite = spriteCupboard[index4];
                }
                if (j == 1)
                {
                    Proverka3();
                    index5 = randomDown;
                    objectSprite[j + 9].sprite = spriteCupboard[index5];
                }
                if (j == 2)
                {
                    Proverka4();
                    index6 = randomDown;
                    objectSprite[j + 9].sprite = spriteCupboard[index6];
                }
            }
            _3 = false;
        }
        else if (index3 == 4 && _3 == true)
        {
            for (int j = 0; j < 3; j++)
            {
                RandomDown();
                if (j == 0)
                {
                    index4 = randomDown;
                    objectSprite[j + 9].sprite = spriteFridge[index4];
                }
                if (j == 1)
                {
                    Proverka3();
                    index5 = randomDown;
                    objectSprite[j + 9].sprite = spriteFridge[index5];
                }
                if (j == 2)
                {
                    Proverka4();
                    index6 = randomDown;
                    objectSprite[j + 9].sprite = spriteFridge[index6];
                }
            }
            _3 = false;
        }
    }
    private void Mix()
    {
        if (_1 == false && _2 == false && _3 == false && _4 == true)
        {
            for (var i = 0; i < trans.Count; i++)
            {
                var tempPosition = trans[i].transform.position;
                var secondCard = trans[Random.Range(0, trans.Count)];

                trans[i].transform.position = secondCard.transform.position;
                secondCard.transform.position = tempPosition;

                trans.Remove(trans[i]);
                trans.Remove(secondCard);
            }

            for (int p = 0; p < scr.Length; p++)
            { scr[p].enabled = true; }

            _4 = false;
        }
    }
    public void NextStart()
    {
        for (int p = 0; p < scr.Length; p++)
        { scr[p].enabled = false; }
    }
}