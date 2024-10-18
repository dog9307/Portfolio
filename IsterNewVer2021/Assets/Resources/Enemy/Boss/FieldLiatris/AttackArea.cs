using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _areaAttack;

    [SerializeField]
    GameObject _damageArea;

    FieldLiatrisController _fieldLiatris;
    [SerializeField]
    SFXPlayer _sfx;
    public float _attackTime;
    float _currentTime;
    private void OnEnable()
    {
        //광역 공격 시작부분
        _fieldLiatris = FindObjectOfType<FieldLiatrisController>();
        _currentTime = 0;
        var ps = _areaAttack.main;
        ps.startLifetime = _attackTime;

        _areaAttack.Play();
        _sfx.PlaySFX("area_attack");
        _damageArea.SetActive(true);
    }
    private void OnDisable()
    {
        _fieldLiatris._SpecialPatternEnd = true;
        Destroy(this.gameObject);
    }
    private void Update()
    {
        //광역 공격 들어가는 부분
        if(_currentTime < _attackTime)
        {
            _currentTime += IsterTimeManager.bossDeltaTime;
        }
        else
        {
            _damageArea.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

}
