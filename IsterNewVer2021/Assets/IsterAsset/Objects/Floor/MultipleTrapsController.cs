using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTrapsController : MonoBehaviour
{
    [SerializeField]
    private FloorTrap[] _traps;

    [SerializeField]
    private SFXPlayer _sfx;

    private static bool _isSfxPlayed = false;

    public void TrapsOn()
    {
        if (!_isSfxPlayed)
        {
            _sfx.PlaySFX("trap");
            _isSfxPlayed = true;
        }

        foreach (var trap in _traps)
            trap.TrapOn();
    }
}
