using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[System.Serializable]
public class UIOption
{
    public string _name;
    public Slider _slider;
    public Button _button;
    public float _currentValue;
}
public class OptionUI : MonoBehaviour
{
    [SerializeField]
    InGameOptionUi _ingameOption;
    [SerializeField]
    StartSceneUI _startScene;
    public UIOption[] _uiOption;

    float _maxVolume;
    int _sliderCount;

    private void OnEnable()
    {
        if (_ingameOption)
        {
            _ingameOption = GetComponentInParent<InGameOptionUi>();
        }

        if (_startScene)
        {
            _startScene = GetComponentInParent<StartSceneUI>();
        }
    }
    private void Start()
    {
        
        _sliderCount = 0;
        _maxVolume = 1.0f;

        if (_uiOption.Length != 0)
        {
            for (int i = 0; i < _uiOption.Length; i++)
            {
                if (_uiOption[i]._slider != null)
                {
                    if (DataManager.instance.dataInfosfloat.ContainsKey(_uiOption[i]._name))
                        _uiOption[i]._slider.value = DataManager.instance.dataInfosfloat[_uiOption[i]._name];
                    else
                        _uiOption[i]._slider.value = _maxVolume;
                }
                else continue;
            }
        }
    }
    private void Update()
    {
        if (_ingameOption)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _ingameOption._subMenuOn)
            {
                _ingameOption.SoundMenuOff();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || KeyManager.instance.IsOnceKeyDown("up"))
            {
                _sliderCount -= 1;
                if (_sliderCount < 0)
                {
                    _sliderCount = _uiOption.Length - 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || KeyManager.instance.IsOnceKeyDown("down"))
            {
                _sliderCount += 1;
                if (_sliderCount > _uiOption.Length - 1)
                {
                    _sliderCount = 0;
                }
            }

            if ((Input.GetKeyDown(KeyCode.LeftArrow) || KeyManager.instance.IsOnceKeyDown("left")) || (Input.GetKeyDown(KeyCode.RightArrow) || KeyManager.instance.IsOnceKeyDown("right")))
            {
                if (_uiOption[_sliderCount]._slider != null) this.BroadcastMessage(_uiOption[_sliderCount]._name);
            }
        }
        else if (_startScene)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _startScene._optionSubMenuOn)
            {
                _startScene.SoundMenuOff();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || KeyManager.instance.IsOnceKeyDown("up"))
            {
                _sliderCount -= 1;
                if (_sliderCount < 0)
                {
                    _sliderCount = _uiOption.Length - 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || KeyManager.instance.IsOnceKeyDown("down"))
            {
                _sliderCount += 1;
                if (_sliderCount > _uiOption.Length - 1)
                {
                    _sliderCount = 0;
                }
            }

            if ((Input.GetKeyDown(KeyCode.LeftArrow) || KeyManager.instance.IsOnceKeyDown("left")) || (Input.GetKeyDown(KeyCode.RightArrow) || KeyManager.instance.IsOnceKeyDown("right")))
            {
                if (_uiOption[_sliderCount]._slider != null) this.BroadcastMessage(_uiOption[_sliderCount]._name);
            }
        }

        if (_uiOption.Length != 0)
        {
            for (int i = 0; i < _uiOption.Length; i++)
            {
                if (_uiOption[i]._slider != null)
                {
                    _uiOption[i]._slider.maxValue = _maxVolume;

                    if (_ingameOption)
                    {
                        if (_ingameOption._soundsMenu.activeSelf)
                        {
                            _uiOption[i]._slider.value = DataManager.instance.dataInfosfloat[_uiOption[i]._name];
                            PlayerPrefs.SetFloat(_uiOption[i]._name, _uiOption[i]._slider.value);
                        }
                    }
                    else if (_startScene)
                    {
                        if (_startScene._soundMenu.activeSelf)
                        {
                            _uiOption[i]._slider.value = DataManager.instance.dataInfosfloat[_uiOption[i]._name];
                            PlayerPrefs.SetFloat(_uiOption[i]._name, _uiOption[i]._slider.value);
                        }
                    }
                }
                else continue;
            }
        }

        


    }
    void BgmVolume()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || KeyManager.instance.IsOnceKeyDown("left")) && DataManager.instance.dataInfosfloat["BgmVolume"] > 0)
        {
            SetOption("BgmVolume", 0.1f, false);

        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || KeyManager.instance.IsOnceKeyDown("right")) && DataManager.instance.dataInfosfloat["BgmVolume"] < 1)
        {
            SetOption("BgmVolume", 0.1f, true );
        }
    }
    void EffectVolume()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || KeyManager.instance.IsOnceKeyDown("left")) && DataManager.instance.dataInfosfloat["EffectVolume"] > 0)
        {
            SetOption("EffectVolume", 0.1f, false);
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || KeyManager.instance.IsOnceKeyDown("right")) && DataManager.instance.dataInfosfloat["EffectVolume"] < 1)
        {
            SetOption("EffectVolume", 0.1f, true);
        }
    }
    void MasterVolume()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || KeyManager.instance.IsOnceKeyDown("left")) && DataManager.instance.dataInfosfloat["MasterVolume"] > 0)
        {
            SetOption("MasterVolume", 0.1f, false);
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || KeyManager.instance.IsOnceKeyDown("right")) && DataManager.instance.dataInfosfloat["MasterVolume"] < 1)
        {
            SetOption("MasterVolume", 0.1f, true);
        }
    }
    void Brightness()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || KeyManager.instance.IsOnceKeyDown("left")) && DataManager.instance.dataInfosfloat["Brightness"] > 0)
        {
            SetOption("Brightness", 0.1f, false);
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || KeyManager.instance.IsOnceKeyDown("right")) && DataManager.instance.dataInfosfloat["Brightness"] < 1)
        {
            SetOption("Brightness", 0.1f, true);
        }
    }
    public void SetOption(string name, float parameter, bool increase)
    {
        if (increase && DataManager.instance.dataInfosfloat[name] < 1.0f)
        {
            DataManager.instance.dataInfosfloat[name] = DataManager.instance.dataInfosfloat[name] + parameter;     
            PlayerPrefs.SetFloat("name", DataManager.instance.dataInfosfloat[name]);

        }
        else if(!increase && DataManager.instance.dataInfosfloat[name] > 0.0f)
        {
            DataManager.instance.dataInfosfloat[name] = DataManager.instance.dataInfosfloat[name] - parameter;
            PlayerPrefs.SetFloat("name", DataManager.instance.dataInfosfloat[name]);
        }
    }
    public void ResetAllParameter()
    {
        DataManager.instance.dataInfosfloat["BgmVolume"] = 1.0f;
        DataManager.instance.dataInfosfloat["EffectVolume"] = 1.0f;
        DataManager.instance.dataInfosfloat["MasterVolume"] =1.0f;
        DataManager.instance.dataInfosfloat["Brightness"] =1.0f;
        PlayerPrefs.SetFloat("BgmVolume", DataManager.instance.dataInfosfloat["BgmVolume"]);
        PlayerPrefs.SetFloat("EffectVolume", DataManager.instance.dataInfosfloat["EffectVolume"]);
        PlayerPrefs.SetFloat("MasterVolume", DataManager.instance.dataInfosfloat["MasterVolume"]);
        PlayerPrefs.SetFloat("Brightness", DataManager.instance.dataInfosfloat["Brightness"]);

        if (_uiOption.Length != 0)
        {
            for (int i = 0; i < _uiOption.Length; i++)
            {
                if (_uiOption[i]._slider != null) _uiOption[i]._slider.value = _maxVolume;
                else continue;
            }
        }

    }
    //InGameOptionUi _ingameOption;

    //public Slider _bgmSlider, _effectSlider, _masterSlider, _brightnessSlider;

    //float _maxVolume;

    ////Start is called before the first frame update
    //void Start()
    //{
    //    _ingameOption = GetComponentInParent<InGameOptionUi>();

    //    _maxVolume = 100.0f;

    //    //초기화
    //    _bgmSlider.maxValue = _effectSlider.maxValue = _masterSlider.maxValue = _brightnessSlider.maxValue = _maxVolume;

    //    SetSliderValue();

    //    //if(_sliders.Count !=0)
    //    //{
    //    //    for (int i = 0; i < _sliders.Count; i++)
    //    //    {
    //    //        _sliders[i]._slider.maxValue = _maxVolume;
    //    //        _sliders[i]._slider.minValue = 0.0f;
    //    //        //저장된 값이 있다면 불러오고 100을 곱해준다 ( 계산이 값/100 이니)
    //    //        if (DataManager.instance.dataInfosfloat.ContainsKey(_sliders[i]._name))
    //    //            _sliders[i]._currentValue = DataManager.instance.dataInfosfloat[_sliders[i]._name]*100;
    //    //        //없다면 값/100 = 1 로 초기화 해준다.
    //    //        else
    //    //            _sliders[i]._currentValue = _maxVolume;
    //    //        _sliders[i]._slider.value = _sliders[i]._currentValue;
    //    //    }
    //    //}
    //}
    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        _ingameOption.SoundMenuOff();
    //    }     
    //    //if(this.gameObject.activeSelf)
    //    //{
    //    //    DataManager.instance.dataInfosfloat["BgmVolume"] = _bgmSlider.value / _bgmSlider.maxValue;
    //    //    DataManager.instance.dataInfosfloat["MasterVolume"] = _masterSlider.value / _masterSlider.maxValue;
    //    //    DataManager.instance.dataInfosfloat["EffectVolume"] = _effectSlider.value / _effectSlider.maxValue;
    //    //    DataManager.instance.dataInfosfloat["Brightness"] = _brightnessSlider.value / _brightnessSlider.maxValue;
    //    //
    //    //    PlayerPrefs.SetFloat("BgmVolume", _bgmSlider.value);
    //    //    PlayerPrefs.SetFloat("MasterVolume", _masterSlider.value);
    //    //    PlayerPrefs.SetFloat("EffectVolume", _effectSlider.value);
    //    //    PlayerPrefs.SetFloat("Brightness", _brightnessSlider.value);
    //    //}
    //}
    //public void BgmSoundControl()
    //{
    //    if ((Input.GetKey(KeyCode.RightArrow) || KeyManager.instance.IsStayKeyDown("right")) && _bgmSlider.value < _maxVolume)
    //    {
    //        _bgmSlider.value += 1.0f * Time.deltaTime;
    //    }
    //    else if (Input.GetKey(KeyCode.LeftArrow) || KeyManager.instance.IsStayKeyDown("left") && _bgmSlider.value > 0.0f)
    //    {
    //        _bgmSlider.value -= 1.0f * Time.deltaTime;
    //    }
    //    //PlayerPrefs.SetFloat("BgmVolume", _bgmSlider.value);
    //}
    //public void EffectSoundControl()
    //{
    //    if ((Input.GetKey(KeyCode.RightArrow) || KeyManager.instance.IsStayKeyDown("right")) && _effectSlider.value < _maxVolume)
    //    {
    //        _effectSlider.value += 1.0f * Time.deltaTime;
    //    }
    //    else if (Input.GetKey(KeyCode.LeftArrow) || KeyManager.instance.IsStayKeyDown("left") && _effectSlider.value > 0.0f)
    //    {
    //        _effectSlider.value -= 1.0f * Time.deltaTime;
    //    }
    //    //PlayerPrefs.SetFloat("EffectVolume", _effectSlider.value);
    //}
    //public void MasterSoundControl()
    //{
    //    if ((Input.GetKey(KeyCode.RightArrow) || KeyManager.instance.IsStayKeyDown("right")) && _masterSlider.value < _maxVolume)
    //    {
    //        _masterSlider.value += 1.0f * Time.deltaTime;
    //    }
    //    else if (Input.GetKey(KeyCode.LeftArrow) || KeyManager.instance.IsStayKeyDown("left") && _masterSlider.value > 0.0f)
    //    {
    //        _masterSlider.value -= 1.0f * Time.deltaTime;
    //    }
    //    //PlayerPrefs.SetFloat("MasterVolume", _masterSlider.value);
    //}
    //public void BrigthnessControl()
    //{
    //    if ((Input.GetKey(KeyCode.RightArrow) || KeyManager.instance.IsStayKeyDown("right")) && _brightnessSlider.value < _maxVolume)
    //    {
    //        _brightnessSlider.value += 1.0f * Time.deltaTime;
    //    }
    //    else if (Input.GetKey(KeyCode.LeftArrow) || KeyManager.instance.IsStayKeyDown("left") && _brightnessSlider.value > 0.0f)
    //    {
    //        _brightnessSlider.value -= 1.0f * Time.deltaTime;
    //    }
    //    //PlayerPrefs.SetFloat("Brightness", _brightnessSlider.value);
    //}
    ////public void SoundControl()
    ////{
    ////    if (EventSystem.current.currentSelectedGameObject.tag == "UI")
    ////    {
    ////        if ((Input.GetKey(KeyCode.RightArrow) || KeyManager.instance.IsStayKeyDown("right")) && EventSystem.current.currentSelectedGameObject.GetComponent<Slider>().value < _maxVolume)
    ////        {
    ////            EventSystem.current.currentSelectedGameObject.GetComponent<Slider>().value += 1.0f;
    ////        }
    ////        else if ((Input.GetKey(KeyCode.LeftArrow) || KeyManager.instance.IsStayKeyDown("left")) && EventSystem.current.currentSelectedGameObject.GetComponent<Slider>().value > 0.0f)
    ////        {
    ////            EventSystem.current.currentSelectedGameObject.GetComponent<Slider>().value -= 1.0f;
    ////        }
    ////        //DataManager.instance.dataInfosfloat[EventSystem.current.currentSelectedGameObject.GetComponent<SliderBar>()._name] = EventSystem.current.currentSelectedGameObject.GetComponent<SliderBar>()._currentValue / _sliders[i]._slider.maxValue;
    ////        //
    ////        //PlayerPrefs.SetFloat(_sliders[i]._name, DataManager.instance.dataInfosfloat[_sliders[i]._name]);
    ////   }
    ////   //if (_sliders.Count != 0)
    ////   //{
    ////   //    for (int i = 0; i < _sliders.Count; i++)
    ////   //    {
    ////   //        if (EventSystem.current.currentSelectedGameObject.tag == "UI")
    ////   //        {
    ////   //            if ((Input.GetKey(KeyCode.RightArrow) || KeyManager.instance.IsStayKeyDown("right")) && _sliders[i]._currentValue < _sliders[i]._slider.maxValue)
    ////   //            {
    ////   //                _sliders[i]._currentValue += 1.0f;
    ////   //                DataManager.instance.dataInfosfloat[_sliders[i]._name] = _sliders[i]._currentValue / _sliders[i]._slider.maxValue;
    ////   //            }
    ////   //            else if (Input.GetKey(KeyCode.LeftArrow) || KeyManager.instance.IsStayKeyDown("left") && _sliders[i]._currentValue > _sliders[i]._slider.minValue)
    ////   //            {
    ////   //                _sliders[i]._currentValue -= 1.0f;
    ////   //                DataManager.instance.dataInfosfloat[_sliders[i]._name] = _sliders[i]._currentValue / _sliders[i]._slider.maxValue;
    ////   //            }
    ////   //        }
    ////   //        PlayerPrefs.SetFloat(_sliders[i]._name, DataManager.instance.dataInfosfloat[_sliders[i]._name]);
    ////   //    }
    ////   //}
    ////    //if (_sliderBar.Length > 0)
    ////    //{
    ////    //    int i = 0;
    ////    //    if (i < _sliderBar.Length)
    ////    //    {
    ////    //        if ((Input.GetKey(KeyCode.RightArrow) || KeyManager.instance.IsStayKeyDown("right"))&& _sliderBar[i]._volume < 1.0f)
    ////    //        {
    ////    //            _sliderBar[i]._volume += 0.01f;
    ////    //            DataManager.instance.dataInfosfloat[_sliderBar[i]._name] = _sliderBar[i]._volume;
    ////    //        }
    ////    //        else if (Input.GetKey(KeyCode.LeftArrow) || KeyManager.instance.IsStayKeyDown("left") && _sliderBar[i]._volume > 0.0f)
    ////    //        {
    ////    //            _sliderBar[i]._volume -= 0.01f;
    ////    //            DataManager.instance.dataInfosfloat[_sliderBar[i]._name] = _sliderBar[i]._volume;
    ////    //        }
    ////    //    }
    ////    //}
    ////}
    //public void ResetOption()
    //{
    //    _bgmSlider.value = 100.0f;
    //    DataManager.instance.dataInfosfloat["BgmVolume"] = 1.0f;
    //    _effectSlider.value = 100.0f;
    //    DataManager.instance.dataInfosfloat["EffectVolume"] = 1.0f;
    //    _masterSlider.value = 100.0f;
    //    DataManager.instance.dataInfosfloat["MasterVolume"] = 1.0f;
    //    _brightnessSlider.value = 100.0f;
    //    DataManager.instance.dataInfosfloat["Brightness"] = 1.0f;

    //    PlayerPrefs.SetFloat("BgmVolume", 1.0f);
    //    PlayerPrefs.SetFloat("EffectVolume", 1.0f);
    //    PlayerPrefs.SetFloat("MasterVolume", 1.0f);
    //    PlayerPrefs.SetFloat("Brightness", 1.0f);
    //}

    //void SetSliderValue()
    //{
    //    if (DataManager.instance.dataInfosfloat.ContainsKey("BgmVolume"))
    //        _bgmSlider.value = DataManager.instance.dataInfosfloat["BgmVolume"] * 100;
    //    else
    //        _bgmSlider.value = _maxVolume;

    //    if (DataManager.instance.dataInfosfloat.ContainsKey("EffectVolume"))
    //        _effectSlider.value = DataManager.instance.dataInfosfloat["EffectVolume"] * 100;
    //    else
    //        _effectSlider.value = _maxVolume;

    //    if (DataManager.instance.dataInfosfloat.ContainsKey("MasterVolume"))
    //        _masterSlider.value = DataManager.instance.dataInfosfloat["MasterVolume"] * 100;
    //    else
    //        _masterSlider.value = _maxVolume;

    //    if (DataManager.instance.dataInfosfloat.ContainsKey("Brightness"))
    //        _brightnessSlider.value = DataManager.instance.dataInfosfloat["Brightness"] * 100;
    //    else
    //        _brightnessSlider.value = _maxVolume;

    //    _bgmSlider.onValueChanged.AddListener(delegate { BgmSoundControl(); });
    //    _effectSlider.onValueChanged.AddListener(delegate { EffectSoundControl(); });
    //    _masterSlider.onValueChanged.AddListener(delegate { MasterSoundControl(); });
    //    _brightnessSlider.onValueChanged.AddListener(delegate { BrigthnessControl(); });
    //}
}
