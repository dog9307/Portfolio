using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBGMPlayer : SoundPlayer
{
    [HideInInspector]
    [SerializeField]
    private BGMPlayer _bgm;
    [SerializeField]
    [Range(0.0f, 1.0f)]private float _bgmVolume = 1.0f;
    public float bgmVolume { set { _bgmVolume = value; } }

    [HideInInspector]
    [SerializeField]
    private AmbientSoundPlayer _amb;
    [SerializeField]
    [Range(0.0f, 1.0f)] private float _ambVolume = 1.0f;
    public float ambVolume { set { _ambVolume = value; } }

    [HideInInspector]
    [SerializeField]
    private BGMPlayer _bossBGM;
    [SerializeField]
    [Range(0.0f, 1.0f)] private float _bossBgmVolume = 1.0f;

    public AudioClip bgm { get { return _bgm.bgm; } set { _bgm.bgm = value; } }
    public AudioClip amb { get { return _amb.amb; } set { _amb.amb = value; } }

    public BGMPlayer bossBGM { get { return _bossBGM; } set { _bossBGM = value; } }

    [SerializeField]
    private bool _isPlayWithStart = false;

    [SerializeField]
    private bool _isAmbPlayWithStart = false;

    private bool _isBgmBoss;

    void Start()
    {
        if (_isPlayWithStart)
            PlayBGM();
        if (_isAmbPlayWithStart)
            PlayAmbient();
    }

    public void PlayBGM()
    {
        if (_bgm)
        {
            _isBgmBoss = false;
            _bgm.PlaySound(_bgmVolume);
        }
    }

    public void PlayBGM(float bgmFade)
    {
        if (_bgm)
        {
            _isBgmBoss = false;
            _bgm.PlaySound(_bgmVolume, bgmFade);
        }
    }

    public void PlayAmbient()
    {
        if (_amb)
            _amb.PlaySound(_ambVolume);
    }

    public void PlayAmbient(int channel)
    {
        if (_amb)
            _amb.PlayAmbient(channel);
    }

    public void PlayAmbient(float fadeTime)
    {
        if (_amb)
            _amb.PlaySound(_ambVolume, fadeTime);
    }

    public void PlayBossBGM()
    {
        if (bossBGM)
        {
            _isBgmBoss = true;
            bossBGM.PlaySound(_bossBgmVolume);
        }
    }

    public override void PlaySound()
    {
        PlayBGM();
        PlayAmbient();
    }

    public void StopBGM()
    {
        if (_bgm)
            _bgm.StopSound();
    }

    public void StopAmbient()
    {
        if (_amb)
            _amb.StopSound();
    }

    public void StopAmbient(float fadeTime)
    {
        if (_amb)
            _amb.StopSound(fadeTime);
    }

    public void StopBossBGM()
    {
        if (bossBGM)
            bossBGM.StopSound();
    }

    public override void StopSound()
    {
        if (_isBgmBoss)
            StopBossBGM();
        else
            StopBGM();

        StopAmbient();
    }

    public override void StopSound(float fadeTime)
    {
        if (_isBgmBoss)
            StopBossBGM();
        else
            StopBGM();

        StopAmbient(fadeTime);
    }
}
