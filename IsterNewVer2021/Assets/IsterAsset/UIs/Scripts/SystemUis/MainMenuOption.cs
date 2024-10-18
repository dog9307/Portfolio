using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuOption : MonoBehaviour
{
    public GameObject _graphicMenu;
    public GameObject _soundMenu;

    //처음 열때
    public GameObject _gameStartButton, _graphicfirstButton, _soundFirstPotion;

    //해당 메뉴 닫았을 때.
    public GameObject _graphicClosedButton, _soundClosedButton;

    [HideInInspector]
    public bool _pauseOn;
    //다른 메뉴 열려있을 때.
    public bool _subMenuOn;



    public GameObject _currentSelect;
    
    private void Start()
    {
        _pauseOn =true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!_subMenuOn)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {

            }

        }
         _currentSelect = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(_currentSelect);
    }
    public void GraphicMenuOn()
    {
        _subMenuOn = true;
        _pauseOn = false;
        _graphicMenu.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(_graghicsFirstButton);
        _graphicfirstButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_graphicfirstButton);
    }
    public void GraphicMenuOff()
    {
        //EventSystem.current.SetSelectedGameObject(null);
        _subMenuOn = false;
        _pauseOn = true;
        _graphicMenu.SetActive(false);
        //EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(_graghicsClosedButton);
        _graphicClosedButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_graphicClosedButton);
    }
    public void SoundMenuOn()
    {
        //EventSystem.current.SetSelectedGameObject(null);
        _subMenuOn = true;
        _pauseOn = false;
        _soundMenu.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(_graghicsClosedButton);
        _soundFirstPotion.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_soundFirstPotion);
    }
    public void SoundMenuOff()
    {       
        //EventSystem.current.SetSelectedGameObject(null);
        _subMenuOn = false;
        _pauseOn = true;
        _soundMenu.SetActive(false);
        //EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(_graghicsClosedButton);
        _soundClosedButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_soundClosedButton);
    }
}
