using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEffectManager : MonoBehaviour
{
    [SerializeField]
    private RoomEffectDictionary _effects;
    private ParticleSystem _currentEffect;

    public void TurnOnEffect(string effectName, bool isPrevTurnOff = false)
    {
        if (isPrevTurnOff && _currentEffect)
        {
            if (_currentEffect.isPlaying)
                _currentEffect.Stop();
        }

        if (!_effects.ContainsKey(effectName)) return;

        _currentEffect = _effects[effectName];
        _currentEffect.Play();
    }

    public void TurnOffEffect()
    {
        _currentEffect?.Stop();
        _currentEffect = null;
    }

    public void EffectPlayOneShot(string effectName)
    {
        if (!_effects.ContainsKey(effectName)) return;

        _effects[effectName].Play();
    }
}

[System.Serializable]
public class RoomEffectDictionary : SerializableDictionary<string, ParticleSystem> { };
