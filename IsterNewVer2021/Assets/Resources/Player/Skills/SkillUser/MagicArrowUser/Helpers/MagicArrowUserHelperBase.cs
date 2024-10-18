using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MagicArrowUserHelperBase : IObjectCreator
{
    public GameObject effectPrefab { get; set; }

    public MagicArrowUser owner { get; set; }

    public abstract void Init();
    public abstract void Release();
    public abstract void Update();

    public abstract GameObject CreateObject();
    public abstract void UseSkill(GameObject newBullet);
}
