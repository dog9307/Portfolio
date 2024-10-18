using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundPlayer : MonoBehaviour
{
    public abstract void PlaySound();
    public virtual void PlaySound(float targetVolume) { }
    public abstract void StopSound();
    public abstract void StopSound(float fadeTime);

    public virtual AudioSource PlaySoundLoop() { return null; }
}
