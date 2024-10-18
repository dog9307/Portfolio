using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDoorEffectHelper : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] _particles;
    private int _currentEffect = 0;

    private static int NORMAL_EFFECT = 0;
    private static int BOSS_EFFECT   = 1;

    public void EffectOn()
    {
        _particles[_currentEffect].Play();
        //foreach (var particle in _particles)
        //    particle.Play();
    }

    public void EffectOff()
    {
        _particles[_currentEffect].Stop();
        //foreach (var particle in _particles)
        //    particle.Stop();w
    }

    public void SetToNormalEffect()
    {
        _currentEffect = NORMAL_EFFECT;
        _particles[NORMAL_EFFECT].gameObject.SetActive(true);
        _particles[BOSS_EFFECT].gameObject.SetActive(false);
    }

    public void SetToBossEffect()
    {
        _currentEffect = BOSS_EFFECT;
        _particles[NORMAL_EFFECT].gameObject.SetActive(false);
        _particles[BOSS_EFFECT].gameObject.SetActive(true);
    }
}
