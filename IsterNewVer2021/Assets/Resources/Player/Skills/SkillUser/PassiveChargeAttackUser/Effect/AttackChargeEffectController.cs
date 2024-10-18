using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChargeEffectController : MonoBehaviour
{
    [SerializeField]
    private PassiveChargeAttackUser _owner;

    [SerializeField]
    private List<GameObject> _effects;

    private void OnEnable()
    {
        foreach (GameObject effect in _effects)
            effect.SetActive(false);
    }

    public void EffectOn(int index)
    {
        if (index >= _effects.Count) return;

        _effects[index].SetActive(true);
        ParticleSystem effect = _effects[index].GetComponent<ParticleSystem>();
        if (!effect.isPlaying)
            effect.Play();
        effect.transform.parent = null;
    }

    public void EffectOff()
    {
        foreach (var e in _effects)
        {
            ParticleSystem effect = e.GetComponent<ParticleSystem>();
            effect.Stop();
            effect.transform.parent = transform;
            effect.transform.localPosition = Vector3.zero;
        }
    }
}
