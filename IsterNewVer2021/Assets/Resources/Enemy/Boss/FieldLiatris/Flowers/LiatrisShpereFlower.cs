using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiatrisShpereFlower : LiatrisFlowers
{
    [SerializeField]
    private GameObject _shpere;

    [SerializeField]
    private ParticleSystem _shield;
    //스피어가 맞았다.
    [SerializeField]
    private bool _isHitShpere;

    private Coroutine _resetCoroutine;

    [SerializeField]
    private float _coolTime;
    private float _currentTimer;

    protected override void OnEnable()
    {
        _shield.Play();
        if (_shpere)
            _coolTime = _shpere.GetComponent<FieldFlowerSphere>()._coolTime; 
        _currentTimer = 0;
        base.OnEnable();
        _isHitShpere = false;
    }
   //protected override void Update()
   //{
   //    base.Update();
   //}
    // Update is called once per frame
    protected override void FlowerUpdate()
    {
        if (_isSpawned)
        {
            if (!_shpere.activeSelf) { _shpere.SetActive(true); _shpere.GetComponent<FieldFlowerSphere>()._isFire = false; }

            if (!_isHitShpere)
            {
                _damagable.isCanHurt = false;
            }
            else
            {
                _damagable.isCanHurt = true;
            }
        }
        else
        {
            _damagable.isCanHurt = false;
       
        }
        // if (_shpere.activeSelf)
        // {
        //     _shpere.SetActive(false);
        // }
    }
    protected override void FlowerDie()
    {
        _isHitShpere = false; 
        _shpere.SetActive(false); 
    }
    //protected override void Update()
    //{
    //    //base.Update()
    //    if (!_liatris.damagable.isDie)
    //    {
    //        if (!_damagable.isDie)
    //        {
    //            if (_isSpawned)
    //            {                
    //                if (!_shpere.activeSelf) _shpere.SetActive(true);
    //                else return;

    //                if (!_damagable.isCanHurt) _shield.Play();
    //                else _shield.Stop();
    //            }

    //            if (_isHitShpere)
    //            {
    //                Debug.Log("들어옴");
    //                _damagable.isCanHurt = true;
    //            }
    //            else
    //            {
    //                _damagable.isCanHurt = false;
    //            }


    //        }
    //        else
    //        {
    //            if (_shpere.activeSelf)
    //            {
    //                _shpere.SetActive(false);
    //            }
    //        }

    //        if (_isSpawning && !_isSpawned)
    //        {
    //            Spawning();
    //        }
    //    }      
    //}
    public void HitShpere()
    {
        _shield.Stop();
        _sfx.PlaySFX("cut_flowershield");
        _resetCoroutine = StartCoroutine(ResetHit());
        _isHitShpere = true;
    }
    IEnumerator ResetHit()
    {
        _currentTimer = 0;

        while (_currentTimer < _coolTime)
        {
            yield return null;

            _currentTimer += IsterTimeManager.bossDeltaTime;
        }
        _isHitShpere = false;
        _shield.Play();
        _resetCoroutine = null;
    }

}
