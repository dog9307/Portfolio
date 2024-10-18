using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthRoofManager : MonoBehaviour
{
    [SerializeField]
    private DepthRoofController[] _roofs;

    [SerializeField]
    private DepthRoofController _startDeactiveRoof;

    void Start()
    {
        foreach (var r in _roofs)
            r.manager = this;

        if (_startDeactiveRoof)
            _startDeactiveRoof.DeactiveRoof();
        else
        {
            foreach (var r in _roofs)
                r.ActiveRoof();
        }
    }

    public void DeactiveRoofChange(DepthRoofController deactive)
    {
        foreach (var r in _roofs)
        {
            if (deactive == r) continue;

            r.ActiveRoof();
        }
    }
}
