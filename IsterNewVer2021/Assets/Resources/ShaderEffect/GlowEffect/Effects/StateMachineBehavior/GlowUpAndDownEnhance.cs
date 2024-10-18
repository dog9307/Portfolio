using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowUpAndDownEnhance : StateMachineBehaviour
{
    private GlowUpAndDown _effect;

    private float _originMin;
    private float _originMax;
    private float _originFrequency;

    [SerializeField]
    private float _newMin = 0.0f;
    [SerializeField]
    private float _newMax = 0.0f;
    [SerializeField]
    private float _newFrequency = 0.0f;

    [SerializeField]
    private Color _newColor;
    private Color _startColor;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _effect = animator.GetComponent<GlowUpAndDown>();
        if (!_effect) return;

        _startColor = _effect.color;
        _effect.color = _newColor;

        _originMin = _effect.minIntensity;
        _originMax = _effect.maxIntensity;
        _originFrequency = _effect.frequency;

        _effect.minIntensity = _newMin;
        _effect.maxIntensity = _newMax;
        _effect.frequency = _newFrequency;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (!_effect) return;

        _effect.minIntensity = _originMin;
        _effect.maxIntensity = _originMax;
        _effect.frequency = _originFrequency;
        _effect.color = _startColor;
    }
}
