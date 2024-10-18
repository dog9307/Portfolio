using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveFortuneUser : SwordChangingUser
{
    protected override void Start()
    {
        base.Start();

        SwordChange();
        _attack.ChangeHelper(new PlayerArrowAttackHelper());
    }

    public override void UseSkill()
    {
    }
}
