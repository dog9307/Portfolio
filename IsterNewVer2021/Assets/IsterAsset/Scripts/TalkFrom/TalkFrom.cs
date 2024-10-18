using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TalkFrom : MonoBehaviour
{
    [SerializeField]
    protected SFXPlayer _sfx;
    [SerializeField]
    protected string _sfxName;

    //public abstract void Talk(PlayerInfo currentPlayer);
    public abstract void Talk();

    public void PlaySFX()
    {
        if (_sfx)
            _sfx.PlaySFX(_sfxName);
    }
}
