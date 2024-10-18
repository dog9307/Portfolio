using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualSword : PlayerComponent
{
    protected override void OnEnable()
    {
        PlayerAttacker attack = GetComponent<PlayerAttacker>();
        if (!attack) return;

        attack.isDualSword = true;
    }

    private void OnDestroy()
    {
        PlayerAttacker attack = GetComponent<PlayerAttacker>();
        if (!attack) return;

        attack.isDualSword = false;
    }
}
