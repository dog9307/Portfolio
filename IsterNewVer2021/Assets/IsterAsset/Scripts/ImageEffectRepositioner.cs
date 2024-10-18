using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageEffectRepositioner : MonoBehaviour
{
    void Start()
    {
        if (!transform.parent) return;

        SpriteRenderer parent = GetComponentInParent<SpriteRenderer>();
        if (!parent) return;
        
        float offsetY = parent.bounds.size.y / 10.0f + 1.0f;
        Vector2 pos = new Vector2(0.0f, offsetY);

        transform.localPosition = pos;
    }
}
