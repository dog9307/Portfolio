using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveReversePower : BuffPassive
{
    public override int id { get { return 204; } }
    public override float figure
    {
        get
        {
            // test
            return 0;
        }
        set { }
    }

    public override void Init()
    {
        base.Init();

        //otherType = typeof(ActiveReversePower);
        
        skillName = "반전강화 : A";
        skillDesc = "플레이어의 공격이 적중할 경우, 다음에 사용할 액티브 스킬이 강화된다.";
    }

    public override void BuffOn()
    {
        BuffInfo buff = owner.GetComponent<BuffInfo>();
        buff.isPassiveReversePowerOn = true;
    }

    public override void BuffOff()
    {
        BuffInfo buff = owner.GetComponent<BuffInfo>();
        buff.isPassiveReversePowerOn = false;
    }
}
