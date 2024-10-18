using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMarkerHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;

             if (buffId == 0) temp = new PassiveMarkerDamageUp();
        else if (buffId == 1) temp = new PassiveMarkerTimeBomb();
        else if (buffId == 2) temp = new PassiveMarkerRageMode();
        else if (buffId == 3) temp = new PassiveMarkerDebuffRemover();
        else if (buffId == 4) temp = new PassiveMarkerDamageProportion();
        else if (buffId == 5) temp = new PassiveMarkerNewMarker();
        else if (buffId == 6) temp = new PassiveMarkerDebuffRemoveAll();

        if (temp != null)
        {
            temp.relativeSkillName = "PassiveMarker";
            temp.id = buffId;
        }

        return temp;
    }
}
