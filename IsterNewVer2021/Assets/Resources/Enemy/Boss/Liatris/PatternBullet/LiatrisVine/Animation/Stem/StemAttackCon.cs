using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemAttackCon : MonoBehaviour
{
    public GameObject _damager;

    public GameObject _dontMoveArea;
    [SerializeField]
    ParticleSystem _attackEffect;
    // Start is called before the first frame update
    public void DamageAreaOn()
    {
        _damager.SetActive(true);
        _dontMoveArea.SetActive(true);
        if (_attackEffect) _attackEffect.Play();
    }
    public void DamageAreaOff()
    {
        _damager.SetActive(false);
    }

    public void MoveAble()
    {
        _dontMoveArea.SetActive(false);
    }
}
