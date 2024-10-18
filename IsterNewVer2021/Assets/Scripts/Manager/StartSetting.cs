using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StartSetting : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _mixer;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        bool isFullScreen = System.Convert.ToBoolean(PlayerPrefs.GetString("FullScreen", "true"));
        Screen.SetResolution(PlayerPrefs.GetInt("ScreenX", 1920), PlayerPrefs.GetInt("ScreenY", 1080), isFullScreen);

        //for (int i = 0; i < SoundSystem.instance._effectSound.Length; i++)
        //{
        //    SoundSystem.instance._effectSound[i]._audioSource.volume = PlayerPrefs.GetFloat("EffectVolume", 1.0f);
        //}
        //SoundSystem.instance._bgmSound.volume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.0f);
        DataManager.instance.dataInfosfloat["MasterVolume"] = masterVolume;

        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", -4.0f);
        DataManager.instance.dataInfosfloat["BgmVolume"] = bgmVolume;

        float sfxVolume = PlayerPrefs.GetFloat("BGMVolume", -9.0f);
        DataManager.instance.dataInfosfloat["EffectVolume"] = sfxVolume;

        DataManager.instance.dataInfosfloat["Brightness"] = PlayerPrefs.GetFloat("Brightness", 1.0f);

        if (_mixer)
        {
            _mixer.SetFloat("MasterVolume", masterVolume);
            _mixer.SetFloat("BGMVolume", bgmVolume);
            _mixer.SetFloat("SFXVolume", sfxVolume);
        }
    }
    private void Start()
    {
        //SoundSystem.instance.PlayBgmSound("stage1bgm");
    }
}
