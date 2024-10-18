using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase
{
    public virtual int id { get; set; }

    public abstract void Init();
    public abstract void Release();
}
