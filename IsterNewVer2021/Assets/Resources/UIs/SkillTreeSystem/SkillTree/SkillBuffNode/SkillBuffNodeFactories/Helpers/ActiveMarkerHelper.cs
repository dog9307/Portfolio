using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMarkerHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;

             if (buffId == 0) temp = new ActiveMarkerStack();
        else if (buffId == 1) temp = new ActiveMarkerRandomDebuff();
        else if (buffId == 2) temp = new ActiveMarkerDecreaseTickTime();
        else if (buffId == 3) temp = new ActiveMarkerDamageUp();
        else if (buffId == 4) temp = new ActiveMarkerTimeUp();
        else if (buffId == 5) temp = new ActiveMarkerRangeUp();

        if (temp != null)
        {
            temp.relativeSkillName = "ActiveMarker";
            temp.id = buffId;
        }

        return temp;
    }
}
