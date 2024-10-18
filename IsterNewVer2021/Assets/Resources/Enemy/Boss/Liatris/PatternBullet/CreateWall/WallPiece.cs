using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPiece : MonoBehaviour
{
    [SerializeField]
    private int _hitCount;

    private int _currentHitCount;

    [SerializeField]
    Collider2D _collider;
    [SerializeField]
    GameObject _wallObject;

    [SerializeField]
    ParticleSystem _hitEffect;


    [SerializeField]
    ParticleSystem _breakEffect;

    [SerializeField]
    private SFXPlayer _sfx;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _currentHitCount = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            _currentHitCount++;

            if (_currentHitCount > _hitCount - 0.0000001f)
            {
                WallBraek();
            }

            else
            {
                if (_sfx)
                    _sfx.PlaySFX("wall_hit");

                if (_hitEffect)
                {
                    _hitEffect.gameObject.transform.position = collision.transform.position;
                    _hitEffect.Play();
                }
                CameraShakeController.instance.CameraShake(7.0f);
            }
        }    
    } 
    void WallBraek() 
    {
        _collider.isTrigger = true;

        if (_breakEffect)
            _breakEffect.Play();

        if (_sfx)
            _sfx.PlaySFX("wall_destroy");

        _wallObject.gameObject.SetActive(false);
    }

}
