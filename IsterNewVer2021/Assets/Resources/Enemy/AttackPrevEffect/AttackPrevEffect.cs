using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackPrevEffect : MonoBehaviour
{
    //데미저 에어리어를 가진 오브젝트
    [SerializeField]
    protected GameObject _damager;
    public GameObject damager { get { return _damager; } set { _damager = value; } }

    public float _delayCount;
    protected float _currentCount;

    //데미지 영역
    protected SpriteRenderer _image;

    //데미지 소리.
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
