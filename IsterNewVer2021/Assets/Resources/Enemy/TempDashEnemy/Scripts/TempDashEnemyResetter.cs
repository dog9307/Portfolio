using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TempDashEnemyResetter : DamagableStateResetter
{
    [SerializeField]
    private TempDashEnemyAttacker _attack;

    public override void StateReset()
    {
        _attack.AttackEnd();
        _attack.Stop();

        _attack.gameObject.layer = LayerMask.NameToLayer("Enemys");
    }
}
