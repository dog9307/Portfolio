using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthRoofController : MonoBehaviour
{
    public DepthRoofManager manager { get; set; }

    [HideInInspector]
    [SerializeField]
    private SpriteRenderer _renderer;

    public void ActiveRoof()
    {
        if (!manager) return;

        Color color = _renderer.color;
        color.a = 1.0f;
        _renderer.color = color;
    }

    public void DeactiveRoof()
    {
        if (!manager) return;

        manager.DeactiveRoofChange(this);

        Color color = _renderer.color;
        color.a = 0.0f;
        _renderer.color = color;
    }
}
