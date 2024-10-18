using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DIFFICULTLY
{
}

public class BossMain : MonoBehaviour
{
    //고정형인가
    //public bool _isFixBoss;
    //페이즈랑 난이도 관련

    //체력관련
    public int _bossHp;
    [SerializeField]
    Damagable _damagable;
    public Damagable damagable { get { return _damagable; } }
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _damagable = GetComponent<Damagable>();
        _damagable.totalHP = _bossHp;
        _damagable.currentHP = _bossHp;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
