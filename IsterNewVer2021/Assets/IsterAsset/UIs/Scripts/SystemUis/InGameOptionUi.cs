using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameOptionUi : MonoBehaviour 
{
    public GameObject _pauseMenu;
    public GameObject _graphicsMenu;
    public GameObject _soundsMenu;
    public GameObject _controllerMenu;
    public GameObject _backtoTitleMenu;

    //처음 열때
    public GameObject _pauseFirstButton, _graghicsFirstButton, _soundFirstButton, _controllerFirstButton;

    //해당 메뉴 닫았을 때.
    public GameObject _graghicsClosedButton, _soundClosedButton, _controllerClosedButton;

    private GameObject _currentSelectMenu;

    //다른 메뉴 열려있을 때.
    public bool _subMenuOn;
    [HideInInspector]
    public bool _firstOpen;

    public GameObject _currentSelect;

    private Button _btn;
    
    public AudioUIController _audioUI;

    private void Start()
    {
        _firstOpen = false;

        if (_audioUI)
            _audioUI.LoadAudio();
    }
    private bool _prevFreeze;
    // Update is called once per frame
    void Update()
    {
        //_currentSelect = EventSystem.current.currentSelectedGameObject;
        if (!UIManager.instance.pauseOn && UIManager.instance.IsCanSystemUIOn())
        {
            if (!_firstOpen)
            {
                if (KeyManager.instance.IsOnceKeyDown("menu", UIManager.instance.prevPlayerStop))
                {
                    _firstOpen = true;

                    UIManager.instance.pauseOn = true;
                    _pauseFirstButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
                    //_btn = _pauseFirstButton.GetComponentInChildren<Button>();
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(_pauseFirstButton);
                    //_btn.OnSelect(new BaseEventData(EventSystem.current));

                    Time.timeScale = 0.0f;
                }
            }
        }
        else if (!_subMenuOn && UIManager.instance.pauseOn)
        {
            if (KeyManager.instance.IsOnceKeyDown("menu", UIManager.instance.prevPlayerStop))
            {
                _firstOpen = false;
                UIManager.instance.pauseOn = false;
                ResumeButton();
                Time.timeScale = 1.0f;
            }
        }

        if (!UIManager.instance.pauseOn && !UIManager.instance.tabOn && !UIManager.instance.dialogueOn)
        {
            UIManager.instance.PlayerStop(_subMenuOn);
        }

        //if (EventSystem.current.currentSelectedGameObject == _graghicsClosedButton)
        //{
        //    _currentSelectMenu = _graphicsMenu;
        //    _currentSelectMenu.SetActive(true);
        //}
        //else if (EventSystem.current.currentSelectedGameObject == _pauseFirstButton)
        //{
        //    // (_currentSelectMenu) _currentSelectMenu.SetActive(false);
        //}
        //_currentSelect = EventSystem.current.currentSelectedGameObject;
        //EventSystem.current.SetSelectedGameObject(_currentSelect);
    }
    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    _currentSelect = EventSystem.current.currentSelectedGameObject;
    //    EventSystem.current.SetSelectedGameObject(_currentSelect);
    //}
    public void OnMouseOver()
    {
        _currentSelect = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(_currentSelect);
    }
    public void ResumeButton()
    {
        Time.timeScale = 1.0f;

        _firstOpen = false;
        if (_currentSelectMenu)
        {
            _currentSelectMenu.SetActive(false);
            _currentSelectMenu = null;
        }
        EventSystem.current.SetSelectedGameObject(null);
        UIManager.instance.pauseOn = false;
    }
    public void ReturnToTitleButton()
    {
        Time.timeScale = 1.0f;

        TutorialDestroyTrigger[] destroyTriggers = FindObjectsOfType<TutorialDestroyTrigger>();
        foreach (var trigger in destroyTriggers)
            trigger.isEnable = false;

        ResumeButton();
        SceneLoader.instance.AddScene("StartScene");
        SceneLoader.instance.LoadScene();
    }
    public void GraphicMenuOn()
    {
        SoundMenuOff();

        //EventSystem.current.SetSelectedGameObject(null);
        //UIManager.instance.pauseOn = false;
        _graphicsMenu.SetActive(true);
        _subMenuOn = true;
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(_graghicsFirstButton);
        _graghicsFirstButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_graghicsFirstButton);
    }
    public void GraphicMenuOff()
    {
        //EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(null);
        _subMenuOn = false;
        _graphicsMenu.SetActive(false);
        UIManager.instance.pauseOn = true;
        //EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(_graghicsClosedButton);
        _graghicsClosedButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_graghicsClosedButton);
    }
    public void ControllerMenuOn()
    {
        //EventSystem.current.SetSelectedGameObject(null);
        UIManager.instance.pauseOn = false;
        _controllerMenu.SetActive(true);
        _subMenuOn = true;
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(_graghicsFirstButton);
        _controllerFirstButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_controllerFirstButton);
    }
    public void ControllerMenuOff()
    {
        //EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(null);
        _subMenuOn = false;
        _controllerMenu.SetActive(false);
        UIManager.instance.pauseOn = true;
        //EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(_graghicsClosedButton);
        _controllerClosedButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_controllerClosedButton);
    }
    public void SoundMenuOn()
    {
        GraphicMenuOff();

        //EventSystem.current.SetSelectedGameObject(null);
        //UIManager.instance.pauseOn = false;
        _soundsMenu.SetActive(true);
        _subMenuOn = true;
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(_soundFirstButton);
        _soundFirstButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_soundFirstButton);
    }
    public void SoundMenuOff()
    {
        //EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(null);
        _subMenuOn = false;
        UIManager.instance.pauseOn = true;
        _soundsMenu.SetActive(false);
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(_soundClosedButton);
        _soundClosedButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_soundClosedButton);
    }

   
    
}
