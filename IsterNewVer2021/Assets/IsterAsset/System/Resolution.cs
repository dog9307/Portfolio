using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public struct ScreenSize{

    public int _Width;
    public int _height;
}
public class Resolution : MonoBehaviour
{
    [SerializeField]
    InGameOptionUi _ingameOption;

    [SerializeField]
    StartSceneUI _startScene;
    //[SerializeField]
    //List<Button> _resolutionButtons;
    [SerializeField]
    GameObject _resolutionButton;
    [SerializeField]
    GameObject _screenModeButton;

    [SerializeField]
    Text _resolutionText;

    [SerializeField]
    List<ScreenSize> _screenSize;
    int _currentSizeIndex;
    int _prevSizeIndex;

    [SerializeField]
    Image _windowscreenCheck;
    [SerializeField]
    Image _fullscreenCheck;

    [SerializeField]
    Image _selectMarker;

    EventSystem _event;

    private int _prevX, _prevY;
    private int _currentX, _currentY;
    private bool _prevFull;
    private bool _currentFull;

    private bool _isReturn;

    private void OnEnable()
    {
        _isReturn = false;

        _currentSizeIndex = PlayerPrefs.GetInt("ResolutionNum", _screenSize.Count - 1);
        _currentX = _screenSize[_currentSizeIndex]._Width;
        _currentY = _screenSize[_currentSizeIndex]._height;
        _currentFull = DataManager.instance.switchInfos["FullScreen"];

        _prevX = _currentX;
        _prevY = _currentY;
        _prevFull = _currentFull;
        _prevSizeIndex = PlayerPrefs.GetInt("ResolutionNum", _screenSize.Count - 1);
    }
    private void Start()
    {
        if (_ingameOption)
        {
            _ingameOption = GetComponentInParent<InGameOptionUi>();
        }

        if (_startScene)
        {
            _startScene = GetComponentInParent<StartSceneUI>();
        }

        _currentSizeIndex = PlayerPrefs.GetInt("ResolutionNum", _screenSize.Count - 1);
        _currentX = _screenSize[_currentSizeIndex]._Width;
        _currentY = _screenSize[_currentSizeIndex]._height;
        _currentFull = DataManager.instance.switchInfos["FullScreen"];
    }

    private void Update()
    {
        CloseMenu();
        ReturnResolution();
       
        if (EventSystem.current.currentSelectedGameObject == _resolutionButton)
        {
            ResolutionSetter();
        }
        if (EventSystem.current.currentSelectedGameObject == _screenModeButton)
        {
            ScreenModeChange();
        }



        if (_resolutionButton)
        {
            _resolutionText.text = (_currentX.ToString() + " x " + _currentY.ToString());
        }
        

        _currentX = _screenSize[_currentSizeIndex]._Width;
        _currentY = _screenSize[_currentSizeIndex]._height;
        _currentFull = DataManager.instance.switchInfos["FullScreen"];

        _fullscreenCheck.gameObject.SetActive(_currentFull);
        _windowscreenCheck.gameObject.SetActive(!_currentFull);
    }
    public void CloseMenu()
    {
        if (_ingameOption)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _ingameOption._subMenuOn)
            {
                if (!_isReturn)
                {
                    PlayerPrefs.SetInt("ResolutionNum", _prevSizeIndex);
                    _currentX = _screenSize[_prevSizeIndex]._Width;
                    _currentY = _screenSize[_prevSizeIndex]._height;
                    _currentFull = _prevFull;
                    SetResolution(_prevX, _prevY, _prevFull);
                    _ingameOption.GraphicMenuOff();
                }
                else
                {
                    _ingameOption.GraphicMenuOff();
                    _isReturn = false;
                }
            }
        }
        else if (_startScene)
        {

            if (Input.GetKeyDown(KeyCode.Escape) && _startScene._optionSubMenuOn)
            {
                if (!_isReturn)
                {
                    PlayerPrefs.SetInt("ResolutionNum", _prevSizeIndex);
                    _currentX = _screenSize[_prevSizeIndex]._Width;
                    _currentY = _screenSize[_prevSizeIndex]._height;
                    _currentFull = _prevFull;
                    SetResolution(_prevX, _prevY, _prevFull);
                    _startScene.GraphicMenuOff();
                }
                else
                {
                    _startScene.GraphicMenuOff();
                    _isReturn = false;
                }
            }
        }
    }
    public void ReturnResolution()
    {
        if (_ingameOption && EventSystem.current.currentSelectedGameObject != _screenModeButton)
        {
            if (Input.GetKeyDown(KeyCode.Return) && _ingameOption._subMenuOn)
            {

                _isReturn = true;
                PlayerPrefs.SetInt("ResolutionNum", _currentSizeIndex);
                SetResolution(_currentX, _currentY, _currentFull);
            }
        }
        else if (_startScene)
        {
            if (Input.GetKeyDown(KeyCode.Return) && _startScene._optionSubMenuOn)
            {

                _isReturn = true;
                PlayerPrefs.SetInt("ResolutionNum", _currentSizeIndex);
                SetResolution(_currentX, _currentY, _currentFull);
            }
        }
    }
    public void ResolutionSetter()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            ResoultionUp();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ResoultionDown();
        }
    } 
    public void ScreenModeChange()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            SetResolutionFullChange();
    }

    public void ResoultionUp()
    {
        _currentSizeIndex++;
        if (_currentSizeIndex >= _screenSize.Count)
        {
            _currentSizeIndex = 0;
        }
    }
    public void ResoultionDown()
    {
        _currentSizeIndex--;
        if (_currentSizeIndex < 0)
        {
            _currentSizeIndex = _screenSize.Count - 1;
        }
    }
    public void SetResolution1920_1080()
    {
        SetResolution(1920, 1080, DataManager.instance.switchInfos["FullScreen"]);
    }
    public void SetResolution1600_1050()
    {
        SetResolution(1600, 1050, DataManager.instance.switchInfos["FullScreen"]);
    }
    public void SetResolution1600_900()
    {
        SetResolution(1600, 900, DataManager.instance.switchInfos["FullScreen"]);
    }
    public void SetResolution1440_900()
    {
        SetResolution(1440, 900, DataManager.instance.switchInfos["FullScreen"]);
    }
    public void SetResolution1360_768()
    {
        SetResolution(1360, 768, DataManager.instance.switchInfos["FullScreen"]);
    }
    public void SetResolution1280_720()
    {
        SetResolution(1280, 720, DataManager.instance.switchInfos["FullScreen"]);
    }

    public void SetResolutionFullChange()
    {       
        PlayerPrefs.SetString("FullScreen", DataManager.instance.switchInfos["FullScreen"].ToString());
        DataManager.instance.switchInfos["FullScreen"] = !DataManager.instance.switchInfos["FullScreen"];


        SetResolution(DataManager.instance.dataInfosInt["ScreenX"], DataManager.instance.dataInfosInt["ScreenY"], DataManager.instance.switchInfos["FullScreen"]);
    }
    public void SetResolution(int width, int height, bool fullscreen)
    {
        DataManager.instance.dataInfosInt["ScreenX"] = width;
        DataManager.instance.dataInfosInt["ScreenY"] = height;
        DataManager.instance.switchInfos["FullScreen"] = fullscreen;
        PlayerPrefs.SetInt("ScreenX", width);
        PlayerPrefs.SetInt("ScreenY", height);
        PlayerPrefs.SetString("FullScreen", DataManager.instance.switchInfos["FullScreen"].ToString());
        Screen.SetResolution(DataManager.instance.dataInfosInt["ScreenX"], DataManager.instance.dataInfosInt["ScreenY"], DataManager.instance.switchInfos["FullScreen"]);
    }

}
