using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;

             if (buffId == 0) temp = new MagicArrowDamageUp();
        else if (buffId == 1) temp = new MagicArrowChargeUp();
        else if (buffId == 2) temp = new MagicArrowSpeedUpBomb();
        else if (buffId == 3) temp = new MagicArrowSpeedDownPenetration();
        else if (buffId == 4) temp = new MagicArrowRockman();
        else if (buffId == 5) temp = new MagicArrowLux();
        else if (buffId == 6) temp = new MagicArrowBombLeftSomething();
        else if (buffId == 7) temp = new MagicArrowDottedDamage();
        else if (buffId == 8) temp = new MagicArrowRockmanLaser();
        else if (buffId == 9) temp = new MagicArrowLuxLaser();

        if (temp != null)
        {
            temp.relativeSkillName = "MagicArrow";
            temp.id = buffId;
        }

        return temp;
    }
}
