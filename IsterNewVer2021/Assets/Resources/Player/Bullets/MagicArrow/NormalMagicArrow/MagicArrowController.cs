using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowController : BulletMovable
{
    public Vector2 dir { get; set; }

    public bool isDamaging { get { return (_rigid.velocity.magnitude >= MIN_MOVE_DISTANCE); } }

    [SerializeField]
    private ParticleSystem _normalEffect;
    [SerializeField]
    private ParticleSystem _destroyEffect;
    [SerializeField]
    private GameObject _orb;

    [SerializeField]
    private SFXPlayer _sfx;

    [SerializeField]
    private float _destroyTime = 2.0f;
    private Coroutine _destroyCoroutine;

    void Start()
    {
        _destroyCoroutine = StartCoroutine(WaitForDestroy());
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(_destroyTime);
        _destroyCoroutine = null;
        DestroyBullet();
    }

    public void Init()
    {
    }

    protected override void ComputeVelocity()
    {
        _targetVelocity = dir * speed;
    }

    public override void DestroyBullet()
    {
        _speed = 0.0f;

        _normalEffect.Stop();
        _orb.SetActive(false);
        _destroyEffect.gameObject.SetActive(true);
        _destroyEffect.Play();

        MagicArrowBombCreator bomb = GetComponent<MagicArrowBombCreator>();
        if (bomb)
            bomb.CreateObject();

        if (_sfx)
            _sfx.PlaySFX("destroy");

        if (_destroyCoroutine != null)
            StopCoroutine(_destroyCoroutine);
    }
}
