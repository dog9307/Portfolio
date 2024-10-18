using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioProgressController : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private MixerParamApplier _paramApplier;

    [SerializeField]
    private float _defaultValue;
    [SerializeField]
    private float _minValue;
    [SerializeField]
    private float _maxValue;

    public void ResetValue()
    {
        _paramApplier.ResetParam();

        float ratio = (_paramApplier.currentValue - _minValue) / (_maxValue - _minValue);
        _slider.value = ratio * _slider.maxValue;
    }

    public void AddValue(float addition)
    {
        _slider.value = Mathf.Clamp(_slider.value + addition, _slider.minValue, _slider.maxValue);

        OnValueChanged();
    }

    public void OnValueChanged()
    {
        float ratio = _slider.value / _slider.maxValue;
        float value = Mathf.Lerp(_minValue, _maxValue, ratio);
        _paramApplier.ApplyParam(value);
    }

    public void SaveParam()
    {
        PlayerPrefs.SetFloat(_paramApplier.paramName, _paramApplier.currentValue);
    }

    public void LoadParam()
    {
        float value = PlayerPrefs.GetFloat(_paramApplier.paramName, _defaultValue);

        float ratio = (value - _minValue) / (_maxValue - _minValue);
        _slider.value = ratio * _slider.maxValue;
    }
}
