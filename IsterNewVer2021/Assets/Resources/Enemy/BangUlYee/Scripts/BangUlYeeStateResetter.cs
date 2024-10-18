using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangUlYeeStateResetter : DamagableStateResetter
{
    [SerializeField]
    private BangUlYeeAttacker _attack;

    public override void StateReset()
    {
        _attack.AttackEnd();
    }
}
