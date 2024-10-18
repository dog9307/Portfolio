using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBuffBase : IBuff
{
    public SkillUser owner { get; set; }
    public string skillBuffName { get { return GetType().Name; } }

    public string relativeSkillName { get; set; }
    public int id { get; set; }

    public abstract void BuffOn();
    public abstract void BuffOff();
}
