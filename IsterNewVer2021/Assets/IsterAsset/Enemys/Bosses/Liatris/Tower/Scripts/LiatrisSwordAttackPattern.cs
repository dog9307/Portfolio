using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiatrisSwordAttackPattern : BossMoveBase
{
    [SerializeField]
    LiatrisController _controller;

    //���� ���� �ð�
    public float _attackDelay;
    //Į ���� �ð�
    public float _attackEndDelay;
    //Į ������� �ð�
    public float _swordDisapperTime;

    float _currentTime;
    // float _dropTime; 

    Coroutine _coroutine;

    //Į ����
    [SerializeField]
    GameObject _swordAttackPrev;
    //Į
    [SerializeField]
    GameObject _swordObject;
    //Į ������.
    [SerializeField]
    SpriteRenderer _renderer;

    public override void PatternOn()
    {
        _currentTime = 0; 
        _movable._moveStart = false;
         _coroutine = StartCoroutine(SwordSpwanPrev());
    }
    public override void PatternOff()
    {
        _owner.movePatternEnd = true;
    }
    public override void PatternEnd()
    {
        base.PatternEnd();
        _swordAttackPrev.SetActive(false);
        _swordObject.SetActive(false);
        _controller._isSword = true;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    //Į ��ȯ ���� ����.
    IEnumerator SwordSpwanPrev()
    {
        _swordAttackPrev.SetActive(true);

        while (_currentTime < _attackDelay)
        {
            _currentTime += IsterTimeManager.bossDeltaTime;

            yield return null;

        }

        _swordAttackPrev.SetActive(false);

        yield return null;

        _currentTime = 0;

        _coroutine = StartCoroutine(SwordSpwan());
    }
    //Į ����
    IEnumerator SwordSpwan()
    {      
        _swordObject.SetActive(true);
        Color color = _renderer.color;
        color.a = 1.0f;

        _renderer.color = color;

        if (_controller)
        {
            _controller._isSword = false;
        }

        yield return null;

        _currentTime = 0;

        _coroutine = StartCoroutine(SwordReset());
    }
    //Į ���� Ÿ�̸�
    IEnumerator SwordReset()
    {
        while (_currentTime < _attackEndDelay)
        {
           _currentTime += IsterTimeManager.bossDeltaTime;
           yield return null;           
        }

        _currentTime = 0;

        _coroutine = StartCoroutine(SwordDisappear());
    }
    //Į ����(���ĺ���)
    IEnumerator SwordDisappear()
    {
        Color color = _renderer.color;
        color.a = Mathf.Lerp(1.0f, 0.0f, 0.0f);

        while (_currentTime < _swordDisapperTime)
        {
            _currentTime += IsterTimeManager.deltaTime;

            float ratio = _currentTime / _swordDisapperTime;

            color.a = Mathf.Lerp(1.0f, 0.0f, ratio);

            _renderer.color = color;

            yield return null;
        }

        _swordObject.SetActive(false);

        if (_controller)
        {
            _controller._isSword = true;
        }

        PatternOff();
    }    
}

