using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMirage : AttackPassive
{
    public override int id { get { return 202; } }
    public float damage
    {
        get { return 8.0f; }
    }

    public float damageMultiplier
    {
        get
        {
            float figure = 0.5f;
            //     if (_currentLevel == 0) figure = 0.5f;
            //else if (_currentLevel == 1) figure = 0.6f;
            //else if (_currentLevel == 2) figure = 0.7f;
            //else if (_currentLevel == 3) figure = 1.0f;

            return figure;
        }
    }

    public override void Init()
    {
        base.Init();

        _cost = 2;

        skillName = "마지막 흉내쟁이";
        skillDesc = "플레이어가 했던 공격을 그대로 따라하는 잔상을 생성한다.";
    }
}
