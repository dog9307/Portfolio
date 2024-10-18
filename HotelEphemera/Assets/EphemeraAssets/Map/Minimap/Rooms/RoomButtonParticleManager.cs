using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomButtonParticleManager : MonoBehaviour
{
    //��ư ����Ʈ���
    [SerializeField]
    private ButtonEffectDictionary _effects;
    //����� ����Ʈ (����)
    private ParticleSystem _currentEffect;


    // Start is called before the first frame update
    public void TurnOnButtonEffect(string effectName,Vector3 position, bool isPrevTurnOff = false)
    {
        if (isPrevTurnOff && _currentEffect)
        {
            if (_currentEffect.isPlaying)
                _currentEffect.Stop();
        }
        if (!_effects.ContainsKey(effectName)) return;

        _currentEffect = _effects[effectName];
        this.transform.position = position;
        _currentEffect.Play();
    }

    public void TurnOffEffect()
    {
        _currentEffect?.Stop();
        _currentEffect = null;
    }
}

[System.Serializable]
public class ButtonEffectDictionary : SerializableDictionary<string, ParticleSystem> { };