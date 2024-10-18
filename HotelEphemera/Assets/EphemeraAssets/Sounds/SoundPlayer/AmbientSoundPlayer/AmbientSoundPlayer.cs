using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundPlayer : SoundPlayer
{
    [SerializeField]
    private AudioClip _amb;
    public AudioClip amb { get { return _amb; } set { _amb = value; } }

    [SerializeField]
    private int _channel = 0;

    public override void PlaySound()
    {
        if (SoundSystem.instance)
            SoundSystem.instance.PlayAmbient(_amb);
    }

    public void PlaySound(float volume)
    {
        if (SoundSystem.instance)
            SoundSystem.instance.PlayAmbient(_amb, _channel, volume);
    }

    public void PlayAmbient(int channel)
    {
        if (SoundSystem.instance)
            SoundSystem.instance.PlayAmbient(_amb, channel);
    }

    public void PlaySound(float volume, float fadeTime)
    {
        if (SoundSystem.instance)
            SoundSystem.instance.PlayAmbient(_amb, _channel, volume, fadeTime);
    }

    public override void StopSound()
    {
        if (SoundSystem.instance)
            SoundSystem.instance.StopAmbient(_amb);
    }

    public override void StopSound(float fadeTime)
    {
        if (SoundSystem.instance)
            SoundSystem.instance.StopAmbient(_amb, 0, fadeTime);
    }
}
