using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CounterAttackUserHelperBase
{
    public PassiveCounterAttackUser owner { get; set; }

    protected GameObject _currentTarget;
    public GameObject currentTarget { get { return _currentTarget; } }
    //protected GameObject _prevTarget;

    public float maxDistance { get { return owner.maxDistance; } }

    public abstract void Init();
    public abstract void Release();

    public abstract bool IsCanCounterAttack();
    
    public abstract void UseSkill(GameObject newBullet, Vector2 dir);
}
