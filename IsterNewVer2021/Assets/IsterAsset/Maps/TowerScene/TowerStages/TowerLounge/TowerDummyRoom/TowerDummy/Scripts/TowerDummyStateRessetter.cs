using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDummyStateRessetter : DamagableStateResetter
{
    [SerializeField]
    private Animator _anim;

    public override void StateReset()
    {
        if (_anim)
            _anim.SetTrigger("hit");
    }
}
