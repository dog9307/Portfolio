using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTriggerMonoBase : MonoBehaviour, ISFXTrigger
{
    private SFXPlayer _sfxPlayer;

    public virtual void TriggerSFX(string name)
    {
        if (!_sfxPlayer)
        {
            _sfxPlayer = GetComponent<SFXPlayer>();
            if (!_sfxPlayer)
            {
                _sfxPlayer = GetComponentInChildren<SFXPlayer>();
                if (!_sfxPlayer)
                    return;
            }
        }

        _sfxPlayer.PlaySFX(name);
    }
}
