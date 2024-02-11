using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour, IPointerClickHandler
{
    private int tutorialState;
    private int tutorialStarsOn;
    private bool tutorialStarsOn2;

    [SerializeField] private SoundButton _soundButton;
    [SerializeField] private TMP_Text tutorialHeadText;
    [SerializeField] private GameObject _stage01;
    [SerializeField] private GameObject _stage02;
    [SerializeField] private GameObject _upPanel;
    [SerializeField] private GameObject _upPanelTimeHand;
    [SerializeField] private GameObject _downPanelTimeHand;
    [SerializeField] private GameObject _growButton;
    [SerializeField] private GameObject _smileButton;
    [SerializeField] private GameObject _menuButton;
    [SerializeField] private GameObject _smileButton1;
    [SerializeField] private GameObject _smileButton2;
    [SerializeField] private GameObject _smileButton3;
    [SerializeField] private GameObject _smileButtonFx;
    [SerializeField] private GameObject _houseButton02;
    [SerializeField] private GameObject _houseDownMenu;
    [SerializeField] private GameObject _blackPanel;
    [SerializeField] private GameObject _upHeadPanel;
    [SerializeField] private GameObject _realDovnPanel;
    [SerializeField] private GameObject _realDovnZone;
    [SerializeField] private GameObject _allTutorial;
    [SerializeField] private GameObject _downHead;
    [SerializeField] private TMP_Text tutorialDownHeadText;
    [SerializeField] private Image _boostButtonImage;
    [SerializeField] private GameObject _dool;
    [SerializeField] private GameObject _actionFx;
    [SerializeField] private GameObject _menuButtonZone;
    [SerializeField] private GameObject _menuButtonBack;
    [SerializeField] private Image _actionButtonImage;
    [SerializeField] private GameObject _menuButtonAction;
    [SerializeField] private GameObject _fortuna;
    [SerializeField] private GameObject _badModul;
    [SerializeField] private GameObject _badModulButton;
    [SerializeField] private Image _dressButton;
    [SerializeField] private Image _badButtonImage;
    private bool tutorialEnd;
    private bool tutorialEnd1;
    private bool tutorialEnd2;
    private bool tutorialEnd3;
    private int badColor;
    
    void Start()
    {
        //tutorialState = 0;
        //PlayerPrefs.DeleteAll();
        _soundButton = _soundButton.GetComponent<SoundButton>();

        int _tutorialState = DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.Tutorial);
        tutorialState = _tutorialState;
        
        LoadTutorial();
    }
    
    void Update()
    {
        if (tutorialStarsOn == 1)
        {
            TimeButtonNext();
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (tutorialState)
        {
            //Первый Этап показывает на часы
            case 0:
                _stage01.SetActive(false);
                _stage02.SetActive(true);
                tutorialHeadText.text = "This is the time button that will help us grow.";
                _soundButton.PlaySound();
                break;
            case 1:
                
                break;
            case 2:
                tutorialState = 11;
                break;
            case 3:
                //Нажми на мебель
                tutorialHeadText.text = "Нажми на мебель";
                tutorialState = 4;
                _soundButton.PlaySound();
                break;
            case 10:
                break;
            case 15:
                MenuAll05Button();
                _soundButton.PlaySound();
                break;
            case 16:
                SaveHalf();
                _soundButton.PlaySound();
                break;
            case 17:
                tutorialState = 20;
                _soundButton.PlaySound();
                break;
            case 21:
                tutorialHeadText.text = "The older you become, the more activities and things are available to you.";
                tutorialState = 22;
                _soundButton.PlaySound();
                break;
            case 22:
                tutorialHeadText.text = "This is the Menu button, let's see what's in there.";
                _smileButton.SetActive(false);
                _menuButton.SetActive(true);
                _smileButton1.SetActive(false);
                _smileButton2.SetActive(false);
                _smileButton3.SetActive(false);
                _stage01.SetActive(false);
                tutorialState = 11;
                _soundButton.PlaySound();
                break;
            case 23:
                tutorialState = 24;
                _soundButton.PlaySound();
                break;
            case 25:
                tutorialState = 20;
                _downHead.SetActive(false);
                _dool.SetActive(false);
                _blackPanel.SetActive(false);
                _actionFx.SetActive(false);
                _menuButtonZone.SetActive(true);
                _menuButtonBack.GetComponent<Button>().interactable = true;
                _menuButtonAction.GetComponent<Button>().interactable = false;
                _actionButtonImage = _actionButtonImage.GetComponent<Image>();
                var tempColor = _actionButtonImage.color;
                tempColor.a = 0f;
                _actionButtonImage.color = tempColor;
                tutorialEnd = true;
                EndTutorial();
                _soundButton.PlaySound();
                break;
            case 26:
                
                break;
        }
    }

    private void SaveHalf()
    {
        tutorialState = 23;
        _stage01.SetActive(false);
        _houseButton02.SetActive(true);
        _houseDownMenu.SetActive(false);
        _stage01.SetActive(false);
        _stage02.SetActive(true);
        _smileButton.SetActive(true);
        _blackPanel.SetActive(false);
        _upHeadPanel.SetActive(false);
        _menuButton.SetActive(false);
        _realDovnPanel.SetActive(false);
        _allTutorial.SetActive(false);
        _realDovnZone.SetActive(true);
        DataManager.instance.PlayerDatas.UpdatePlayerParameter(PlayerParameterType.Tutorial, tutorialState);
        tutorialState = 24;
    }
    
    private void LoadHalf()
    {
        tutorialState = 24;
        _stage01.SetActive(false);
        _houseButton02.SetActive(true);
        _houseDownMenu.SetActive(false);
        _stage01.SetActive(false);
        _stage02.SetActive(true);
        _smileButton.SetActive(true);
        _blackPanel.SetActive(false);
        _upHeadPanel.SetActive(false);
        _menuButton.SetActive(false);
        _realDovnPanel.SetActive(false);
        _allTutorial.SetActive(false);
        _realDovnZone.SetActive(true);
    }
    
    
    private void LoadTutorial()
    {
        switch (tutorialState)
        {
            //Старт обучалки
            case 0:
                tutorialHeadText.text = "Hi! Let me teach you how to play!";
                break;
            case 23:
                LoadHalf();
                break;
            case 30:
                Destroy(gameObject);
                break;
        }
    }
    
    public void TimeButton()
    {
        if (tutorialStarsOn == 0)
        {
            tutorialHeadText.text = "Let's collect 500 time units for the next stage of growth.";
            _upPanel.SetActive(true);
            _upPanelTimeHand.SetActive(true);
            _downPanelTimeHand.SetActive(false);
            tutorialState = 10;
        }

        TimeButtonNext();
    }
    private void TimeButtonNext()
    {
        if (DataManager.instance.PlayerDatas.GetParameter(PlayerParameterType.Smiles) >= 500)
        {
            tutorialHeadText.text = "We have collected the necessary resources, tap the growth button.";
            tutorialStarsOn = 2;
            _smileButtonFx.SetActive(false);
            _growButton.SetActive(true);
            _upPanelTimeHand.SetActive(false);
        }
    }

    public void GrowTutorialButton()
    {
        tutorialHeadText.text = "You have matured by one month";
        _stage01.SetActive(true);
        _growButton.SetActive(false);
        tutorialState = 21;
    }
    
    public void HouseButton()
    {
        tutorialHeadText.text = "This is the entertainment room.";
        tutorialState = 11;
    }
    
    //Первая кнопка Меню
    public void MenuAll01Button()
    {
        tutorialHeadText.text = "This is the personal care room.";
    }
    //Вторая кнопка Меню
    public void MenuAll02Button()
    {
        tutorialHeadText.text = "This is the room for interior upgrades.";
    }
    //Третья кнопка Меню
    public void MenuAll03Button()
    {
        tutorialHeadText.text = "This is the room for getting the upgrades.";
    }
    
    //Третья кнопка Меню
    public void MenuAll04Button()
    {
        _stage01.SetActive(true);
        tutorialHeadText.text = "You will receive further hints as you enter the rooms from the menu.";
        tutorialState = 15;
    }
    
    public void MenuAll05Button()
    {
        tutorialHeadText.text = "Now you know the basics of the game! Good luck!";
        tutorialState = 16;
    }
    
    public void ArrowButton()
    {
        tutorialDownHeadText.text = "This upgrades the size of your wallet";
        DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.Smiles, 2000);
        DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.Stars, 2000);
    }
    
    public void Update03Button()
    {
        tutorialDownHeadText.text = "This upgrades the amount of time you get per tap";
    }
    
    public void Update01Button()
    {
        tutorialDownHeadText.text = "This upgrades the speed at witch you get stars while in game";
    }
    
    public void Update02Button()
    {
        tutorialDownHeadText.text = "This upgrades the speed at witch you get stars while not in game";
    }

    public void BoostButtonOff()
    {
        _boostButtonImage = _boostButtonImage.GetComponent<Image>();
        var tempColor = _boostButtonImage.color;
        tempColor.a = 0f;
        _boostButtonImage.color = tempColor;
        tutorialEnd1 = true;
        EndTutorial();
    }
    
    public void GameButton()
    {
        tutorialDownHeadText.text = "Let's play something";
    }
    
    public void GameButton2()
    {
        tutorialDownHeadText.text = "Play with the doll";
    }
    
    public void GameButton3()
    {
        tutorialDownHeadText.text = "When the girl finises playing, her mood will improve";
        tutorialState = 25;
    }
    
    public void ClouthButton()
    {
        tutorialDownHeadText.text = "Let's change";
    }
    
    public void Clouth2Button()
    {
        tutorialDownHeadText.text = "Choose the clothes icon";
    }
    
    public void DressButton()
    {
        tutorialHeadText.text = "Choose an item of clothing";
        DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.Stars, 2000);
    }
    
    public void DressOnBuyButton()
    {
        tutorialHeadText.text = "Confirm your choice";
    }
    
    public void RoomOn()
    {
        tutorialDownHeadText.text = "Let's design the room, tap on the bed";
        DataManager.instance.PlayerDatas.IncreasePlayerParameter(PlayerParameterType.Stars, 2000);
    }
    
    public void DressOn()
    {
        Debug.Log("HAHAHAH");
        _upPanel.SetActive(false);
        _dressButton = _dressButton.GetComponent<Image>();
        var tempColor = _dressButton.color;
        tempColor.a = 0f;
        _dressButton.color = tempColor;
        tutorialEnd2 = true;
        EndTutorial();
    }

    public void BadTutoeialOn()
    {
        _badModul.SetActive(true);
        _downHead.SetActive(true);
        RoomOn();
    }

    public void StarsOn()
    {
        tutorialHeadText.text = "These are stars, you gain them automatically.";
        Invoke("AfterStars", 1);
    }

    public void AfterStars()
    {
        tutorialHeadText.text = "With the help of them you will purchase furniture and clothes.";
    }
    
    public void BadButton()
    {
        tutorialHeadText.text = "Choose a bed";
    }
    
    public void ChouseBadButton()
    {
        tutorialHeadText.text = "Confirm the purchase";
    }
    
    public void BadColorButton()
    {
        if (badColor == 0)
        {
            tutorialHeadText.text = "Now choose its color";
            badColor++;
        }
        else if (badColor == 1)
        {
            _badModul.SetActive(false);
            _blackPanel.SetActive(false);
            _menuButtonZone.SetActive(true);
            _upHeadPanel.SetActive(false);
            _upPanel.SetActive(false);
            _menuButtonBack.GetComponent<Button>().interactable = true;
            _badModulButton.GetComponent<Button>().interactable = false;
            _badButtonImage = _badButtonImage.GetComponent<Image>();
            var tempColor = _badButtonImage.color;
            tempColor.a = 0f;
            _badButtonImage.color = tempColor;
            tutorialEnd3 = true;
            EndTutorial();
        }
    }
    
    public void BuyColorButton()
    {
        tutorialHeadText.text = "Confirm your choice";
    }
    
    private void EndTutorial()
    {
        if (tutorialEnd && tutorialEnd1 && tutorialEnd2 && tutorialEnd3)
        {
            Debug.Log("Ебать Закончили");
            _houseButton02.SetActive(false);
            tutorialState = 30;
            DataManager.instance.PlayerDatas.UpdatePlayerParameter(PlayerParameterType.Tutorial, tutorialState);
            Destroy(gameObject);
        }
    }
}

