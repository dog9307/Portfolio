using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class StartSceneUI : MonoBehaviour
{
    public GameObject _optionMenu;
    public GameObject _graphicMenu;
    public GameObject _soundMenu;
    public GameObject _controllerMenu;
    public GameObject _creditMenu;

    //처음 열때
    public GameObject _gameStartButton, _optionfirstButton, _controllerFirstButton, _exitGameButton ;

    public GameObject _graphicStartButton, _soundStartButton;

    //해당 메뉴 닫았을 때.
    public GameObject _optionClosedButton;

    public GameObject _graphicClosedButton, _controllerClosedButton, _soundClosedButton;

    private GameObject _currentSelectMenu;

    [HideInInspector]
    public bool _firstStart;
    //다른 메뉴 열려있을 때.
    public bool _subMenuOn;
    public bool _optionSubMenuOn;

    public GameObject _currentSelect;

    private Button _btn;

    [SerializeField]
    private SFXPlayer _sfx;

    public AudioUIController _audioUI;

    private void Start()
    {
        BGMPlayer bgm = GetComponentInChildren<BGMPlayer>();
        if (bgm)
            bgm.PlaySound();

        _firstStart = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_gameStartButton);

        if (_audioUI)
            _audioUI.LoadAudio();
    }
    // Update is called once per frame
    void Update()
    {

        //if (_firstStart && !_subMenuOn)
        //{
        //    _firstStart = false;
        //    _btn = _gameStartButton.GetComponentInChildren<Button>();
        //    EventSystem.current.SetSelectedGameObject(null);
        //    EventSystem.current.SetSelectedGameObject(_gameStartButton);
        //    _btn.OnSelect(new BaseEventData(EventSystem.current));
        //}
        if (_subMenuOn && !_optionSubMenuOn)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OptionMenuOff();
            }
        }

       
        //if (EventSystem.current.currentSelectedGameObject == _graphicClosedButton)
        //{
        //    _currentSelectMenu = _graphicMenu;
        //    _currentSelectMenu.SetActive(true);
        //}
        //else if (EventSystem.current.currentSelectedGameObject == _optionfirstButton)
        //{
        //    // (_currentSelectMenu) _currentSelectMenu.SetActive(false);
        //}

        //_currentSelect = EventSystem.current.currentSelectedGameObject;
        //EventSystem.current.SetSelectedGameObject(_currentSelect);

    }

    public void OnMouseOver()
    {
        _currentSelect = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(_currentSelect);
    }

    public void StartButton()
    {
        _gameStartButton.GetComponent<Button>().interactable = false;
        _optionfirstButton.GetComponent<Button>().interactable = false;
        _controllerFirstButton.GetComponent<Button>().interactable = false;
        _exitGameButton.GetComponent<Button>().interactable = false;

        if (_sfx)
            Destroy(_sfx.gameObject);

        SceneLoader.instance.AddScene("PlayerMoveScene");
        SceneLoader.instance.AddScene("IngameUiScene");
        SceneLoader.instance.LoadScene();
    }
    public void OptionMenuOn()
    {
        _subMenuOn = true;
        _optionMenu.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(_graghicsFirstButton);
        _optionfirstButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_optionfirstButton);

        _currentSelectMenu = _graphicMenu;
        _currentSelectMenu.SetActive(true);
    }
    public void OptionMenuOff()
    {
        //EventSystem.current.SetSelectedGameObject(null);
        _subMenuOn = false;
        _optionMenu.SetActive(false);
        //EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(_graghicsClosedButton);
        _optionClosedButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_optionClosedButton);

        if (_currentSelectMenu)
        {
            _currentSelectMenu.SetActive(false);
            _currentSelectMenu = null;
        }
    }
    public void ControllerMenuOn()
    {
        //EventSystem.current.SetSelectedGameObject(null);
        _optionSubMenuOn = true;
        _controllerMenu.SetActive(true);
        _optionMenu.SetActive(false);
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(_graghicsFirstButton);
        _controllerFirstButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_controllerFirstButton);
    }
    public void ControllerMenuOff()
    {
        //EventSystem.current.SetSelectedGameObject(null);
        _optionSubMenuOn = false;
        _controllerMenu.SetActive(false);
        _optionMenu.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(_graghicsClosedButton);
        _controllerClosedButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_controllerClosedButton);
    }
    public void GraphicMenuOn()
    {
        SoundMenuOff();

        _optionSubMenuOn = true;
        _graphicMenu.SetActive(true);
        //_optionMenu.SetActive(false);
        //EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(_graghicsClosedButton);
        _graphicStartButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_graphicStartButton);

        _currentSelectMenu = _graphicMenu;
    }
    public void GraphicMenuOff()
    {
        _optionSubMenuOn = false;
        _graphicMenu.SetActive(false);
        //_optionMenu.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(_graghicsClosedButton);
        _graphicClosedButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_graphicClosedButton);
    }
    public void SoundMenuOn()
    {
        GraphicMenuOff();

        _optionSubMenuOn = true;
        _soundMenu.SetActive(true);
        //_optionMenu.SetActive(false);
        //EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(_graghicsClosedButton);
        _soundStartButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_soundStartButton);

        _currentSelectMenu = _soundMenu;
    }
    public void SoundMenuOff()
    {
        _optionSubMenuOn = false;
        _soundMenu.SetActive(false);
        //_optionMenu.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(_graghicsClosedButton);
        _soundClosedButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_soundClosedButton);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
