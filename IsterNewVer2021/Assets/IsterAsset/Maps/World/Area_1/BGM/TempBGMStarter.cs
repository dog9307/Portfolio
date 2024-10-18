using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TempBGMStarter : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _mixer;
    [SerializeField]
    private float _ratio = 1.5f;
    [SerializeField]
    private float _volume = 1.0f;

    [SerializeField]
    private AudioSource _audio;

    public void StartBGM()
    {
        if (_mixer)
        {
            _mixer.SetFloat("BGM_Pitch", _ratio);
            _mixer.SetFloat("BGM_PitchShifter", 1.0f / _ratio);
        }

        _audio.volume = _volume;
        _audio.Play();
    }

    public void StopBGM()
    {
        if (_audio.isPlaying)
            StartCoroutine(Stopping());
    }

    IEnumerator Stopping()
    {
        float currentTime = 0.0f;
        float totalTime = 1.0f;
        float startVolume = _audio.volume;
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;
            _audio.volume = Mathf.Lerp(startVolume, 0.0f, ratio);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        _audio.volume = 0.0f;
        _audio.Stop();
    }
}
