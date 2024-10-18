using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerF1AdditionalHP : TowerF1BuffItem
{
    private Damagable _damagable;
    private float _extraHP;

    //private float _coolTime;
    //private bool _isCoolTime;

    //public float currentCoolTime { get; set; }
    //public float totalCoolTime { get; set; }

    public override void Init()
    {
        base.Init();

        id = 100;
    }

    public override void BuffOn()
    {
        //_damagable = _buff.GetComponent<Damagable>();
        //_extraHP = 20.0f;

        //_isCoolTime = false;

        //totalCoolTime = 20.0f;

        //_damagable.AddExtraHP(_extraHP);
    }

    public override void BuffOff()
    {
        //_damagable.RemoveExtraHP(_extraHP);
    }

    //public void Update()
    //{
    //    if (IsCoolTime()) return;

    //    if (_damagable.extraCurrentHP <= _damagable.extraTotalHP - _extraHP)
    //        CoolTimeStart();
    //}

    //public void CoolTimeStart()
    //{
    //    _isCoolTime = true;
    //    currentCoolTime = 0.0f;

    //    _damagable.StartCoroutine(CoolTimeUpdate());
    //}

    //public IEnumerator CoolTimeUpdate()
    //{
    //    yield return new WaitForSeconds(totalCoolTime);

    //    _damagable.ExtraHeal(_extraHP);

    //    _isCoolTime = false;
    //}

    //public bool IsCoolTime()
    //{
    //    return _isCoolTime;
    //}

    //public void CoolTimeDown(float time)
    //{
    //}
}
