using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(GlowableObject))]
public class GlowEffectBase : MonoBehaviour
{
    protected GlowableObject _glowObject;
    public Material glowMat
    {
        get
        {
            if (!_glowObject)
                _glowObject = GetComponent<GlowableObject>();

            return _glowObject.glowMat;
        }
    }

    public Color color
    {
        get
        {
            if (!_glowObject)
                _glowObject = GetComponent<GlowableObject>();

            return _glowObject.color;
        }

        set
        {
            if (!_glowObject)
                _glowObject = GetComponent<GlowableObject>();

            _glowObject.color = value;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _glowObject = GetComponent<GlowableObject>();
    }
}
