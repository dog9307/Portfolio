using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapSetter : MonoBehaviour
{
    MinimapController _minimap;

    public MinimapController currentMinimap { get { return _minimap; } set { _minimap = value; } }
    // Start is called before the first frame update
    public void MinimapButtonEvent()
    {
        if (!_minimap)
            _minimap = FindObjectOfType<MinimapController>();

        _minimap.LockOff();
    }
}
