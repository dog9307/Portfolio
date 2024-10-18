using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveBase : SkillBase
{
    protected int _cost;
    public int cost { get { return _cost; } set { _cost = value; } }
}
