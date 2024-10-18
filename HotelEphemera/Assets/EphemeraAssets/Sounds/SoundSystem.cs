using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    NONE = -1,
    BGM,
    AMBIENT,
    SFX,
    END
}

public class SoundSystem : MonoBehaviour
{
    #region singleton
    static private SoundSystem _instance;
    static public SoundSystem instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = FindObjectOfType<SoundSystem>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "SoundSystem";
                _instance = container.AddComponent<SoundSystem>();
            }
        }
        DontDestroyOnLoad(SoundSystem.instance);

        AudioSourceSetting();
    }
    #endregion singleton

    [SerializeField]
    private AudioMixerGroup _bgmGroup;
    [SerializeField]
    [Range(0.0f, 1.0f)] private float _bgmDefaultVolume = 1.0f;
    private AudioSource _bgmSource;

    [SerializeField]
    private AudioMixerGroup _ambientGroup;
    [SerializeField]
    private AudioMixerGroup _secondAmbGroup;
    [SerializeField]
    [Range(0.0f, 1.0f)] private float _ambientDefaultVolume = 1.0f;
    private AudioSource _ambientSource;
    private List<AudioSource> _ambChannels = new List<AudioSource>();
    [SerializeField]
    private int _ambMaxChannel = 3;

    [SerializeField]
    private AudioMixerGroup _sfxGroup;
    [SerializeField]
    private int _sfxMaxChannel = 20;
    private List<AudioSource> _sfxChannels = new List<AudioSource>();

    private AudioClip _currentBGM;
    private AudioClip _nextBGM;

    private AudioClip _currentAmbient;
    private AudioClip _nextAmbient;

    private Coroutine _bgmChange;
    private Coroutine _ambChange;

    void AudioSourceSetting()
    {
        _bgmSource = gameObject.AddComponent<AudioSource>();
        SourceSetting(_bgmSource, SoundType.BGM);

        for (int i = 0; i < _ambMaxChannel; ++i)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            SourceSetting(newSource, SoundType.AMBIENT);

            _ambChannels.Add(newSource);
        }
        _ambChannels[1].outputAudioMixerGroup = _secondAmbGroup;

        for (int i = 0; i < _sfxMaxChannel; ++i)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            SourceSetting(newSource, SoundType.SFX);

            _sfxChannels.Add(newSource);
        }
    }

    public void SourceSetting(AudioSource source, SoundType type)
    {
        switch (type)
        {
            case SoundType.BGM:
                source.playOnAwake = false;
                source.outputAudioMixerGroup = _bgmGroup;
                source.loop = true;
                source.volume = 0.0f;
            break;

            case SoundType.AMBIENT:
                source.playOnAwake = false;
                source.outputAudioMixerGroup = _ambientGroup;
                source.loop = true;
                source.volume = 0.0f;
            break;

            case SoundType.SFX:
                source.playOnAwake = false;
                source.outputAudioMixerGroup = _sfxGroup;
                source.loop = false;
                source.volume = 1.0f;
            break;
        }
    }

    public void PlayBGM(AudioClip bgm, float volume = 1.0f, float fadeTime = 2.0f, bool isNameCheck = false)
    {
        if (_currentBGM)
        {
            if (bgm && isNameCheck)
            {
                if (_currentBGM.name == bgm.name)
                    return;
            }
        }

        _nextBGM = bgm;

        if (_bgmChange != null)
            StopCoroutine(_bgmChange);

        _bgmChange = StartCoroutine(SoundChange(_bgmSource, volume, fadeTime));
    }

    public void PlaySFX(AudioClip sfx, float pitch, float volume)
    {
        for (int i = 0; i < _sfxChannels.Count; ++i)
        {
            AudioSource source = _sfxChannels[i];
            if (source.isPlaying) continue;

            source.pitch = pitch;
            source.volume = volume;
            source.loop = false;
            source.PlayOneShot(sfx);
            break;
        }
    }

    public AudioSource PlaySFXLoop(AudioClip sfx, float pitch, float volume)
    {
        AudioSource loop = null;
        for (int i = 0; i < _sfxChannels.Count; ++i)
        {
            AudioSource source = _sfxChannels[i];
            if (source.isPlaying) continue;

            source.pitch = pitch;
            source.volume = 0.0f;
            source.clip = sfx;
            source.loop = true;
            source.Play();

            StartCoroutine(LoopSFXControl(source, volume, 0.0f));

            loop = source;
            break;
        }

        return loop;
    }

    IEnumerator LoopSFXControl(AudioSource targetSource, float toVolume, float fadeTime = 0.5f)
    {
        float fromVolume = targetSource.volume;
        float currentTime = 0.0f;
        while (currentTime < fadeTime)
        {
            float ratio = currentTime / fadeTime;
            float volume = Mathf.Lerp(fromVolume, toVolume, ratio);

            targetSource.volume = volume;

            yield return null;

            currentTime += TimeManager.originDeltaTime;
        }
        targetSource.volume = toVolume;

        if (Mathf.Abs(targetSource.volume) < float.Epsilon)
            targetSource.Stop();
    }

    public void StopLoopSFX(AudioSource source)
    {
        if (!source) return;

        source.loop = false;

        StartCoroutine(LoopSFXControl(source, 0.0f));

        //source.volume = 0.0f;
        //source.Stop();
    }

    public void StopLoopSFXImmediately(AudioSource source)
    {
        if (!source) return;

        source.loop = false;

        source.volume = 0.0f;
        source.Stop();
    }

    public void StopLoopSFXAll()
    {
        foreach (var s in _sfxChannels)
        {
            if (!s) continue;
            if (!s.isPlaying) continue;
            if (!s.loop) continue;

            StopLoopSFX(s);
        }
    }

    public void StopSFX(AudioClip sfx)
    {
        foreach (var channel in _sfxChannels)
        {
            if (channel.clip.name == sfx.name)
            {
                channel.Stop();
                break;
            }
        }
    }

    public void StopBGM(AudioClip bgm, float fadeTime = 0.5f)
    {
        //if (!_currentBGM) return;
        if (bgm)
        {
            //if (_currentBGM.name != bgm.name)
            //    return;
        }

        _nextBGM = null;

        if (_bgmChange != null)
            StopCoroutine(_bgmChange);

        _bgmChange = StartCoroutine(SoundChange(_bgmSource, 0.0f, fadeTime));
    }

    public void PlayAmbient(AudioClip amb, int channel = 0, float volume = 1.0f, float fadeTime = 0.5f)
    {
        if (channel >= _ambChannels.Count) return;

        _currentAmbient = _ambChannels[channel].clip;
        if (_currentAmbient)
        {
            if (amb)
            {
                //if (_currentAmbient.name == amb.name)
                //    return;
            }
        }

        _nextAmbient = amb;
        _ambientSource = _ambChannels[channel];

        if (_ambChange != null)
            StopCoroutine(_ambChange);

        _ambChange = StartCoroutine(SoundChange(_ambientSource, volume, fadeTime, false));
    }

    public void StopAmbient(AudioClip amb, int channel = 0, float fadeTime = 0.5f)
    {
        if (channel >= _ambChannels.Count) return;

        _currentAmbient = _ambChannels[channel].clip;
        if (!_currentAmbient) return;
        if (amb)
        {
            //if (_currentAmbient.name != amb.name)
            //    return;
        }

        _nextAmbient = null;
        _ambientSource = _ambChannels[channel];

        if (_ambChange != null)
            StopCoroutine(_ambChange);

        _ambChange = StartCoroutine(SoundChange(_ambientSource, 0.0f, fadeTime, false));
    }

    protected IEnumerator SoundChange(AudioSource source, float volume, float fadeTime, bool isBGM = true)
    {
        float finalVolume = 0.0f;
        float volumeDifference = Mathf.Abs(source.volume - finalVolume);
        float inverseFadeTime = (fadeTime == 0.0f ? 1.0f : 1f / fadeTime);
        float targetVolume = 0.0f;

        AudioClip currentSound = null;
        currentSound = (isBGM ? _currentBGM : _currentAmbient);

        AudioClip nextSound = null;
        nextSound = (isBGM ? _nextBGM : _nextAmbient);

        if (currentSound)
        {
            targetVolume = 0.0f;
            while (!Mathf.Approximately(source.volume, targetVolume))
            {
                float delta = Time.deltaTime * volumeDifference * inverseFadeTime;
                source.volume = Mathf.MoveTowards(source.volume, targetVolume, delta);
                yield return null;
            }
            source.Stop();
        }
        source.volume = targetVolume;
        source.clip = null;

        currentSound = nextSound;
        if (nextSound)
        {
            nextSound = null;

            source.clip = currentSound;
            source.Play();

            finalVolume = volume;
            //finalVolume = (isBGM ? _bgmDefaultVolume : _ambientDefaultVolume);
            volumeDifference = Mathf.Abs(source.volume - finalVolume);

            targetVolume = finalVolume;
            while (!Mathf.Approximately(source.volume, targetVolume))
            {
                float delta = Time.deltaTime * volumeDifference * inverseFadeTime;
                source.volume = Mathf.MoveTowards(source.volume, targetVolume, delta);
                yield return null;
            }
            source.volume = targetVolume;
        }

        if (isBGM)
        {
            _currentBGM = currentSound;
            _nextBGM = nextSound;

            _bgmChange = null;
        }
        else
        {
            _currentAmbient = currentSound;
            _nextAmbient = nextSound;

            _ambChange = null;
        }
    }

    public void ChangeMixerGroup(AudioMixerGroup group, SoundType type)
    {
        switch (type)
        {
            case SoundType.BGM:
                _bgmSource.outputAudioMixerGroup = group;
            break;

            case SoundType.AMBIENT:
                for (int i = 0; i < _ambChannels.Count; ++i)
                {
                    _ambChannels[i].outputAudioMixerGroup = group;
                }
            break;

            case SoundType.SFX:
                for (int i = 0; i < _sfxChannels.Count; ++i)
                {
                    _sfxChannels[i].outputAudioMixerGroup = group;
                }
            break;
        }
    }
}
