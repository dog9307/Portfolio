using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossBase : EnemyBase
{
    PlayerMoveController _player;
    public PlayerMoveController player { get { return _player; }}
    
    [HideInInspector]
    public int _phaseCount;

    [HideInInspector]
    public bool _isWaveBoss;

    protected override void Init()
    {
        _player = FindObjectOfType<PlayerMoveController>();
        _phaseCount = 0;
    }
    protected override void StateControll()
    {

    }
    public virtual void Start()
    {
    }
}
