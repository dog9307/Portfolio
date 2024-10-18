using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMChanger : MonoBehaviour
{
    [SerializeField]
    private AudioClip _bgm;
    [SerializeField]
    [Range(0.0f, 1.0f)] private float _bgmVolume = 1.0f;

    [SerializeField]
    private AudioClip _amb;
    [SerializeField]
    [Range(0.0f, 1.0f)] private float _ambVolume = 1.0f;

    private static StageBGMPlayer _stageBgmPlayer;

    public static bool isSkipOnce = false;

    [SerializeField]
    private bool _isResetable = true;

    public void BGMChange()
    {
        if (!_stageBgmPlayer)
            _stageBgmPlayer = FindObjectOfType<StageBGMPlayer>();

        if (SoundSystem.instance)
            SoundSystem.instance.PlayBGM(_bgm, _bgmVolume, 1.0f, true);

        if (SoundSystem.instance)
            SoundSystem.instance.PlayAmbient(_amb, 0, _ambVolume);
    }

    public void ReturnBGM()
    {
        if (!_isResetable) return;

        if (isSkipOnce)
        {
            isSkipOnce = false;
            return;
        }

        if (!_stageBgmPlayer)
            _stageBgmPlayer = FindObjectOfType<StageBGMPlayer>();

        _stageBgmPlayer.PlaySound();
    }
}
