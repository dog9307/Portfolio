using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffPoisonEffectPositioner : MonoBehaviour
{
    [SerializeField]
    private Transform _effect;

    // Start is called before the first frame update
    void Start()
    {
        if (!transform.parent) return;

        SpriteRenderer parent = transform.parent.GetComponent<SpriteRenderer>();
        float offsetY = parent.bounds.size.y / 3.0f;
        Vector2 pos = new Vector2(0.0f, offsetY);
        _effect.localPosition = pos;
    }
}
