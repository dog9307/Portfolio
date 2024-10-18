using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : SoundPlayer
{
    [SerializeField]
    private SFXDictionary _sfxs = new SFXDictionary();

    private SFXSet _currentSFX;

    AudioClip SelectClip(SFXSet sfxSet)
    {
        int rnd = Random.Range(0, sfxSet.clips.Count);
        return sfxSet.clips[rnd];
    }

    public void PlaySFX(string name)
    {
        if (!_sfxs.ContainsKey(name)) return;

        _currentSFX = _sfxs[name];
        PlaySound();
    }

    public void PlaySFX(string name, float volume)
    {
        if (!_sfxs.ContainsKey(name)) return;

        _currentSFX = _sfxs[name];
        PlaySound(volume);
    }

    public AudioSource PlayLoopSFX(string name)
    {
        if (!_sfxs.ContainsKey(name)) return null;

        _currentSFX = _sfxs[name];
        return PlaySoundLoop();
    }

    public override void PlaySound()
    {
        if (_currentSFX == null) return;
        if (_currentSFX.clips == null) return;
        if (_currentSFX.clips.Count <= 0) return;

        AudioClip clip = SelectClip(_currentSFX);

        float pitch = 1.0f;
        if (_currentSFX.isRandomPitch)
            pitch = Random.Range(1.0f - _currentSFX.pitchRange, 1.0f + _currentSFX.pitchRange);

        float volume = _currentSFX.volume;

        SoundSystem.instance.PlaySFX(clip, pitch, volume);
        _currentSFX = null;
    }

    public override void PlaySound(float targetVolume)
    {
        if (_currentSFX == null) return;
        if (_currentSFX.clips == null) return;
        if (_currentSFX.clips.Count <= 0) return;

        AudioClip clip = SelectClip(_currentSFX);

        float pitch = 1.0f;
        if (_currentSFX.isRandomPitch)
            pitch = Random.Range(1.0f - _currentSFX.pitchRange, 1.0f + _currentSFX.pitchRange);

        float volume = targetVolume;

        SoundSystem.instance.PlaySFX(clip, pitch, volume);
        _currentSFX = null;
    }

    public override AudioSource PlaySoundLoop()
    {
        AudioSource loop = null;
        if (_currentSFX == null) return loop;
        if (_currentSFX.clips == null) return loop;
        if (_currentSFX.clips.Count <= 0) return loop;

        AudioClip clip = SelectClip(_currentSFX);

        float pitch = 1.0f;
        if (_currentSFX.isRandomPitch)
            pitch = Random.Range(1.0f - _currentSFX.pitchRange, 1.0f + _currentSFX.pitchRange);

        float volume = _currentSFX.volume;

        loop = SoundSystem.instance.PlaySFXLoop(clip, pitch, volume);
        _currentSFX = null;

        return loop;
    }

    public void StopSFX(string name)
    {
        if (!_sfxs.ContainsKey(name)) return;

        _currentSFX = _sfxs[name];
        SoundSystem.instance.StopSFX(_sfxs[name].clips[0]);
    }

    public void ChangeSFX(string name, AudioClip clip,  float volume)
    {
        if (!_sfxs.ContainsKey(name)) return;

        _sfxs[name].clips[0] = clip;
        _sfxs[name].volume = volume;
    }

    public override void StopSound() { }

    public override void StopSound(float fadeTime)
    {
    }
}

[System.Serializable]
public class SFXSet
{
    public List<AudioClip> clips;
    public bool isRandomPitch = false;
    public float pitchRange = 0.3f;
    [Range(0.0f, 1.0f)]public float volume = 1.0f;
}

[System.Serializable]
public class SFXDictionary : SerializableDictionary<string, SFXSet> { };
