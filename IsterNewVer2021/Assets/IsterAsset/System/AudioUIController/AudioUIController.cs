using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioUIController : MonoBehaviour
{
    [SerializeField]
    private AudioProgressController[] _cons;

    [SerializeField]
    private GameObject _masterVolumeObject;
    [SerializeField]
    private GameObject _bgmVolumeObject;
    [SerializeField]
    private GameObject _sfxVolumeObject;

    [SerializeField]
    private InGameOptionUi _ingameOption;

    [SerializeField]
    private StartSceneUI _startScene;

    private bool _isReturn = false;

    private const int MASTER    = 0;
    private const int BGM       = 1;
    private const int SFX       = 2;

    private void OnEnable()
    {
        _isReturn = false;
    }

    public void LoadAudio()
    {
        foreach (var p in _cons)
            p.LoadParam();
    }

    public void ResetAll()
    {
        foreach (var p in _cons)
            p.ResetValue();
    }

    private void OnDisable()
    {
        foreach (var p in _cons)
            p.SaveParam();
    }

    void Update()
    {
        CloseMenu();
        ReturnAudio();

        if (EventSystem.current.currentSelectedGameObject == _masterVolumeObject)
        {
            MasterVolumeControl();
        }
        else if (EventSystem.current.currentSelectedGameObject == _bgmVolumeObject)
        {
            BGMVolumeControl();
        }
        else if (EventSystem.current.currentSelectedGameObject == _sfxVolumeObject)
        {
            SFXVolumeControl();
        }
    }

    void AddValue(AudioProgressController current)
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            current.AddValue(1.0f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            current.AddValue(-1.0f);
        }
    }

    public void MasterVolumeControl()
    {
        AddValue(_cons[MASTER]);
    }

    public void BGMVolumeControl()
    {
        AddValue(_cons[BGM]);
    }

    public void SFXVolumeControl()
    {
        AddValue(_cons[SFX]);
    }

    public void CloseMenu()
    {
        if (_ingameOption)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _ingameOption._subMenuOn)
            {
                if (!_isReturn)
                {
                    _ingameOption.SoundMenuOff();
                }
                else
                {
                    _ingameOption.SoundMenuOff();
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
                    _startScene.SoundMenuOff();
                }
                else
                {
                    _startScene.SoundMenuOff();
                    _isReturn = false;
                }
            }
        }
    }

    public void ReturnAudio()
    {
        if (_ingameOption)
        {
            if (Input.GetKeyDown(KeyCode.Return) && _ingameOption._subMenuOn)
            {
                _isReturn = true;
            }
        }
        else if (_startScene)
        {
            if (Input.GetKeyDown(KeyCode.Return) && _startScene._optionSubMenuOn)
            {
                _isReturn = true;
            }
        }
    }
}
