using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldLiatris : BossMain
{   
    public int _phase;

    [HideInInspector]
    public int _maxPhase;
    [HideInInspector]
    public float _speedAddition;
    [HideInInspector]
    public float _bulletCountAddition;

    [SerializeField]
    FieldLiatrisController _liatris;

    protected override void Start()
    {
        base.Start();
        _liatris = GetComponent<FieldLiatrisController>();
        _phase = 1;
        _maxPhase = _liatris.shieldCount + 1;
    }
    protected override void Update()
    {
        _phase = _maxPhase - _liatris.shieldCount;        
    }
    

}
