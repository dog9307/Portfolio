using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FloorActive : ActiveBase
{
    public virtual float scale { get; }
    public virtual float damage { get; }
}
