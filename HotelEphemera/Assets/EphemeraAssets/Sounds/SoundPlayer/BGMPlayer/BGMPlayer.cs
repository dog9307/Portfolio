using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : SoundPlayer
{
    [SerializeField]
    private AudioClip _bgm;
    public AudioClip bgm { get { return _bgm; } set { _bgm = value; } }

    [SerializeField]
    private float _fadeInTime = 2.0f;

    [SerializeField]
    private bool _isPrevNameCheck = false;

    public override void PlaySound()
    {
        if (SoundSystem.instance)
            SoundSystem.instance.PlayBGM(_bgm);
    }

    public override void PlaySound(float volume)
    {
        if (SoundSystem.instance)
            SoundSystem.instance.PlayBGM(_bgm, volume, _fadeInTime, _isPrevNameCheck);
    }

    public void PlaySound(float volume, float bgmFade)
    {
        if (SoundSystem.instance)
            SoundSystem.instance.PlayBGM(_bgm, volume, bgmFade);
    }

    public override void StopSound()
    {
        if (SoundSystem.instance)
            SoundSystem.instance.StopBGM(_bgm);
    }

    public override void StopSound(float fadeTime)
    {
        if (SoundSystem.instance)
            SoundSystem.instance.StopBGM(_bgm, fadeTime);
    }
}
