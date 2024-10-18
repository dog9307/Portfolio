using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffPassive : PassiveBase, IBuff
{
    public virtual float figure { get; set; }

    //public override void Init()
    //{
    //    base.Init();
    //}

    public override void UseSkill()
    {
        BuffOn();
    }

    public abstract void BuffOn();
    public abstract void BuffOff();

}
