using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveYaran : ActiveBase
{
    public override void Init()
    {
        base.Init();

        _type = ACTIVE.YARAN;

        skillName = "�߶�";
        skillDesc = "�߶� ����������ų";
    }
}
