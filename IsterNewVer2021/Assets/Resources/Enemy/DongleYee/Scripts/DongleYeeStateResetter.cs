using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DongleYeeStateResetter : DamagableStateResetter
{
    [SerializeField]
    private DongleYeeAttacker _attack;

    public override void StateReset()
    {
        _attack.AttackEnd();
        _attack.Stop();
    }
}
