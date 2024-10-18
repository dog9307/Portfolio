using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackPrevEffect : MonoBehaviour
{
    //������ ���� ���� ������Ʈ
    [SerializeField]
    protected GameObject _damager;
    public GameObject damager { get { return _damager; } set { _damager = value; } }

    public float _delayCount;
    protected float _currentCount;

    //������ ����
    protected SpriteRenderer _image;

    //������ �Ҹ�.
    [SerializeField]
    protected SFXPlayer _sfx;

    protected Coroutine _coroutine;

    protected virtual void OnEnable()
    {
        _currentCount = 0;
        _image = GetComponentInChildren<SpriteRenderer>();
        _coroutine = StartCoroutine(DamagerOn());
    }

    protected abstract IEnumerator DamagerOn();
}
