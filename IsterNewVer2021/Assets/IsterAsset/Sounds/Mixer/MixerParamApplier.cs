using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerParamApplier : MonoBehaviour
{
    [SerializeField]
    private string _paramName = "MasterVolume";
    public string paramName { get { return _paramName; } }
    [SerializeField]
    private AudioMixer _mixer;

    private float _startValue;
    public float currentValue { get { float value = 0.0f; _mixer.GetFloat(_paramName, out value); return value; } }

    void Awake()
    {
        if (!_mixer) return;

        float value = -60.0f;
        if (_mixer.GetFloat(_paramName, out value))
            _startValue = value;
    }

    public void ApplyParam(float value)
    {
        if (!_mixer) return;

        _mixer.SetFloat(_paramName, value);
    }

    public void ResetParam()
    {
        if (!_mixer) return;

        _mixer.SetFloat(_paramName, _startValue);
    }
}
