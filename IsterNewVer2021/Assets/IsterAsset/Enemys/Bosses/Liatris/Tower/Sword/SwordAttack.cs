using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour , IObjectCreator
{
    [SerializeField]
    private GameObject _damager;

    private Animator _anim;

    //[SerializeField]
    //private ParticleSystem _effect;

    [SerializeField]
    Transform _damagerPos;
    [SerializeField]
    SFXPlayer _sfx;

    // [SerializeField]
    // float _cameraShakingFigure;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }
    void Update()
    {
    }
    void OnDamager()
    {
        _sfx.PlaySFX("swordAttack");
        CreateObject();

       //if (_effect)
       //{
       //    _effect.gameObject.SetActive(true);
       //    _effect.Play();
       //}
       //
       ////  CameraShakeController.instance.CameraShake(_cameraShakingFigure);
       //_damager.gameObject.SetActive(true);
    }

    void OffDamager()
    {
        _damager.gameObject.SetActive(false);
    }
    void DestroyThisBullet()
    {

    }
    public GameObject effectPrefab { get { return _damager; } set { _damager = value; } }

    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate<GameObject>(effectPrefab);
        newBullet.transform.position = _damagerPos.position;

        return newBullet;
    }
}
