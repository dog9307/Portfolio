using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorCloning : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sour;
    [SerializeField]
    private SpriteRenderer _dest;
    [SerializeField]
    private float _alphaModifier = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (!(_sour && _dest)) return;

        Color color = _sour.color;
        color.a *= _alphaModifier;
        _dest.color = color;
    }
}
