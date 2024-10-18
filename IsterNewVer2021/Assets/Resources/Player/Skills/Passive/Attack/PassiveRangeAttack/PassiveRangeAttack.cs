using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRangeAttack : AttackPassive
{
    public override int id { get { return 200; } }
    public float damage
    {
        get
        {
            float figure = 10.0f;
            //     if (_currentLevel == 0) figure = 10.0f;
            //else if (_currentLevel == 1) figure = 15.0f;
            //else if (_currentLevel == 2) figure = 20.0f;
            //else if (_currentLevel == 3) figure = 30.0f;

            return figure;
        }
    }

    public override void Init()
    {
        base.Init();

        //otherType = typeof(ActiveRangeAttack);

        
        skillName = "개화";
        skillDesc = "플레이어의 공격이 끝난 지점에 범위공격을 가한다.";

        soundName = "Skill_flower";
    }
}
