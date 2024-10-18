using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject _damager;

    [SerializeField]
    private GameObject _bullet;

    private Animator _anim;

    [SerializeField]
    private ParticleSystem _effect;

    void Start()
    {
        _damager.gameObject.SetActive(false);
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
    }
    void OnDamager()
    {
        _effect.Play();
        _damager.gameObject.SetActive(true);
    }

    void OffDamager()
    {
        _damager.gameObject.SetActive(false);
    }
    void DestroyThisBullet()
    {
        if (_bullet.activeSelf) _bullet.SetActive(false);
    }
}
