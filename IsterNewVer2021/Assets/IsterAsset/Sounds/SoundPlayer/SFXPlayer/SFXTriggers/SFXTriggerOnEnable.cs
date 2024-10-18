using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTriggerOnEnable : SFXTriggerMonoBase
{
    [SerializeField]
    private string _sfxName;

    private void OnEnable()
    {
        base.TriggerSFX(_sfxName);
    }
}
