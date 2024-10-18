using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherSpiritResetter : DamagableStateResetter
{
    public override void StateReset()
    {
        MotherSpiritController _motherSpirit;
        _motherSpirit = GetComponent<MotherSpiritController>();

        _motherSpirit.StateResetWithHitted();
    }
}
