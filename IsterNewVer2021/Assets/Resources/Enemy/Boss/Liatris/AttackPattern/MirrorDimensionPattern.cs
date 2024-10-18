using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorDimensionPattern : BossAttackBase
{
    public float _attackCount;
    public int _fireCounter;

    public float _attackDelay;

    public float _hideTime;

    float _currentCount;

    float _currentTime;

    public float _bulletDistance;

    Coroutine _coroutine;

    Vector3 _preScale;

    [SerializeField]
    private CutSceneController _cameraZoomOut;
    [SerializeField]
    private CutSceneController _cameraZoomIn;

    [SerializeField]
    private SpriteRenderer _renderer;

    public override void SetPatternId()
    {
        _patternID = 102;
    }
    public override void PatternStart()
    {
        _owner._isHide = true;
        base.PatternStart();
        _renderer = GetComponentInChildren<SpriteRenderer>();
    }
    public override void PatternOn()
    {
        _preScale = this.transform.localScale;
        _currentCount = 0;
        _currentTime = 0;
        _coroutine = StartCoroutine(HideBoss());
    }
    public override void PatternOff()
    {
        _owner.attackPatternEnd = true;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
    public override void PatternEnd()
    {
        base.PatternEnd();

        _owner._isHide = false;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(HideOff());
    }

    IEnumerator HideBoss()
    {
        _sfx.PlaySFX("disappear");

        if (_cameraZoomOut)
            _cameraZoomOut.StartCutScene();

        _attacker._attackStart = false;

        Vector3 startScale = _preScale;
        Vector3 endScale = startScale;
        endScale.x = 0.0f;
        while (_currentTime < _hideTime)
        {
            float ratio = _currentTime / _hideTime;

            Vector3 newScale = Vector3.Lerp(startScale, endScale, ratio);
            //transform.localScale = newScale;

            ApplyAlpha(1.0f - ratio);

            yield return null;

            _currentTime += IsterTimeManager.bossDeltaTime;
        }
        ApplyAlpha(0.0f);

        transform.localScale = endScale;
        _currentTime = 0;

        _sfx.PlaySFX("mirrorAttack");

        _coroutine = StartCoroutine(MirrorDimension());
    }
    IEnumerator MirrorDimension()
    {

        while (_currentCount < _attackCount)
        {
            while (_currentTime < _attackDelay)
            {
                _currentTime += IsterTimeManager.bossDeltaTime;
                yield return null;
            }

            MirrorDimensionBulletCreate();

            _currentTime = 0;
            _currentCount++;


            yield return null;
        }

        while (_currentTime < _attackDelay)
        {
            _currentTime += IsterTimeManager.bossDeltaTime;
            yield return null;
        }
    
        yield return null;
        _currentTime = 0;
        _coroutine = StartCoroutine(HideOff());
    }
    IEnumerator HideOff()
    {
        Vector3 startScale = _preScale;
        startScale.x = 0.0f;

        while (_currentTime < _hideTime)
        {
            float ratio = _currentTime / _hideTime;

            Vector3 newScale = Vector3.Lerp(startScale, _preScale, ratio);
            //transform.localScale = newScale;

            ApplyAlpha(ratio);

            yield return null;

            _currentTime += IsterTimeManager.bossDeltaTime;
        }
        ApplyAlpha(1.0f);


        transform.localScale = _preScale;
        _owner._isHide = false;

        PatternOff();

        if (_cameraZoomIn)
            _cameraZoomIn.StartCutScene();
    }

    void MirrorDimensionBulletCreate()
    {
        int num = Random.Range(0, _fireCounter);

        for (int i = 0; i < _fireCounter; i++)
        {
            Vector3 newPos = Vector3.zero;
            if (_fireCounter % 2 == 0)
            {
                newPos = new Vector3((transform.position.x -((_fireCounter / 2) * _bulletDistance) + (_bulletDistance * i)), transform.position.y, transform.position.z);
                //_bulletCreatePos.position = newPos;
                // _bulletPos = _owner.transform.position;
                // Vector3 newPos = _bulletPos - (_owner.transform.position + new Vector3(-((_fireCounter / 2) * _bulletDistance / 2), 0, 0));
                //_bulletCreatePos.position = (transform.position + new Vector3(-((_fireCounter / 2) * _bulletDistance / 2), transform.position.y, transform.position.z)); 
                //Vector3 newPos = (transform.position + new Vector3(-((_fireCounter / 2) * _bulletDistance / 2), transform.position.y, transform.position.z));
                //_bulletCreatePos.position = newPos;
            } 
            else
            {
                newPos = new Vector3(transform.position.x -((_fireCounter / 2) * _bulletDistance) + (_bulletDistance * i) , transform.position.y, transform.position.z);
                //_bulletCreatePos.position = newPos;                
                //_bulletPos = _owner.transform.position;
                //Vector3 newPos = _bulletPos - (_owner.transform.position + new Vector3(-((_fireCounter / 2) * _bulletDistance), 0, 0));
                // _bulletCreatePos.position = (transform.position + new Vector3(-((_fireCounter / 2) * _bulletDistance), transform.position.y, transform.position.z));
                //  Vector3 newPos = (transform.position + new Vector3(-((_fireCounter / 2) * _bulletDistance), transform.position.y, transform.position.z));
                //  _bulletCreatePos.position = newPos;
            }

            if (i == num) continue;
            else
            {
                GameObject newBullet = CreateObject();
                newBullet.transform.position = newPos;
                
            }
        }
    }

    void ApplyAlpha(float alpha)
    {
        Color color = _renderer.color;
        color.a = alpha;
        _renderer.color = color;
    }
}
