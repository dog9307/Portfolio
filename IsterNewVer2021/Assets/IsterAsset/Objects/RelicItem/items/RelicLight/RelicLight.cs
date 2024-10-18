using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicLight : RelicItemBase
{
    private BuffInfo _buff;

    public override void Init()
    {
        id = 100;

        _buff = GameObject.FindObjectOfType<PlayerMoveController>().GetComponent<BuffInfo>();
        if (_buff == null) return;

        _buff.darknessLightMultiplier = 2.0f;
    }

    public override void Release()
    {
        _buff = GameObject.FindObjectOfType<PlayerMoveController>().GetComponent<BuffInfo>();
        if (_buff == null) return;

        _buff.darknessLightMultiplier = 1.0f;
    }

    public override void UseItem()
    {
        RelicLightAffected relicLightTrigger = GameObject.FindObjectOfType<RelicLightAffected>();
        if (relicLightTrigger)
            relicLightTrigger.AffectRelicLight();
    }
}
