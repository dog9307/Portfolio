using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowSync : MonoBehaviour
{
    [SerializeField]
    private GlowableObject _sour;
    public GlowableObject sour { get { return _sour; } set { _sour = value; } }
    [SerializeField]
    private GlowableObject _dest;

    void Start()
    {
        if (!_dest)
            _dest = GetComponent<GlowableObject>();
    }

    void Update()
    {
        if (!_sour) return;
        if (!_dest) return;

        _dest.GlowSync(_sour);
    }
}
