using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearPillarController : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private Animator _anim;

    [HideInInspector]
    [SerializeField]
    private Collider2D _col;

    [SerializeField]
    private float _frames = 123.0f;
    [SerializeField]
    private float _frameRate = 60.0f;
    [SerializeField]
    private float _animSpeed = 0.5f;

    [SerializeField]
    private SFXPlayer _sfx;

    [SerializeField]
    private bool _isStartWithDisappear = false;

    [SerializeField]
    private SpriteRenderer[] _pillars;

    void OnEnable()
    {
        if (_isStartWithDisappear)
            Disappear(true);
    }

    public void Appear()
    {
        if (_sfx)
            _sfx.PlaySFX("appear");

        _col.enabled = true;

        _anim.ResetTrigger("disappear");
        _anim.SetTrigger("appear");

        foreach (var r in _pillars)
            r.enabled = true;

        _col.enabled = true;
        _anim.enabled = true;
    }

    public void Disappear(bool isSkip)
    {
        if (isSkip)
        {
            foreach (var r in _pillars)
                r.enabled = false;

            _col.enabled = false;
            _anim.enabled = false;
        }
        else
            StartCoroutine(WaitForDisappearEnd());
    }

    public void DisappearSFX()
    {
        if (_sfx)
            _sfx.PlaySFX("disappear");
    }

    IEnumerator WaitForDisappearEnd()
    {
        _anim.ResetTrigger("appear");
        _anim.SetTrigger("disappear");

        float totalTime = (_frames * (1.0f / _frameRate)) / _animSpeed;
        yield return new WaitForSeconds(totalTime);

        foreach (var r in _pillars)
            r.enabled = false;

        _col.enabled = false;
    }

    IEnumerator SkipAnim()
    {
        yield return new WaitForEndOfFrame();

        _anim.ForceStateNormalizedTime(0.99f);
    }
}
