using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PillarBulletDamager : MonoBehaviour
{
    [SerializeField]
    private GameObject _damager;

    [SerializeField]
    private GameObject _bullet;

    SpriteRenderer _renderer;
    private Animator _anim;

    [SerializeField]
    private ParticleSystem _effect;
    [SerializeField]
    private ParticleSystem _pieceEffect;

    [SerializeField]
    SFXPlayer _sfx;

    [SerializeField]
    PlayerMoveController _player;
   // [SerializeField]
   // float _cameraShakingFigure;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _damager.gameObject.SetActive(false);
        _anim = GetComponent<Animator>();
        _player = FindObjectOfType<PlayerMoveController>();
    }
    void Update()
    {
        if (_player.gameObject.GetComponent<Damagable>().isDie)
            DestroyThisBullet();
    }
    void OnDamager()
    {
        Color a = _renderer.color;
        a.a = 0;
        _renderer.color = a;

        if (_effect)
        {
            _effect.gameObject.SetActive(true);
            _effect.Play();
        }
        if (_pieceEffect)
        {
            _pieceEffect.gameObject.SetActive(true);
            _pieceEffect.Play();
        }
        if (_sfx) _sfx.PlaySFX("pillardrop");

      //  CameraShakeController.instance.CameraShake(_cameraShakingFigure);
        _damager.gameObject.SetActive(true);
    }

    void OffDamager()
    {
        _damager.gameObject.SetActive(false);
    }
    void DestroyThisBullet()
    {
        Destroy(this.gameObject);
    }
}
