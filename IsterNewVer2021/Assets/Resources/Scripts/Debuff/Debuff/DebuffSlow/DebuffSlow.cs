using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffSlow : DebuffBase
{
    private SpriteRenderer _effect;

    public override void Init()
    {
        figure = (0.5f > figure ? 0.5f : figure);
        MAX_TOTAL_TIME = 10.0f;

        base.Init();

        _effect = CreateObject().GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
        base.Update();

        Color color = _effect.color;
        color.a = (1.0f - (_currentTime / _totalTime));
        _effect.color = color;
    }

    public override void BuffOn()
    {
        owner.slowDecrease = figure;
    }

    public override void BuffOff()
    {
        owner.slowDecrease = 0.0f;

        if (_effect)
            GameObject.Destroy(_effect.gameObject);
    }

    public override GameObject CreateObject()
    {
        GameObject effect = GameObject.Instantiate(effectPrefab);
        effect.transform.parent = owner.transform;
        effect.transform.localPosition = Vector2.zero;

        return effect;
    }
}
