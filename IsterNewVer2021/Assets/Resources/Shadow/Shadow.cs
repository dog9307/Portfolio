using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Shadow : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _relativeSprite;

    [SerializeField]
    private bool _isShadowScaling = true;

    [SerializeField]
    private float _startWidth;

    void Start()
    {
        _startWidth = transform.localScale.x;
    }

    public void ShadowBuild(float size)
    {
        Vector3 scale;
        scale.x = size;
        scale.y = size / 2.0f;
        scale.z = 1.0f;

        transform.localScale = scale;
    }

    void Update()
    {
        if (!_isShadowScaling) return;
        if (!_relativeSprite) return;

        float width = 0.0f;
        if (_relativeSprite.sprite) width = _relativeSprite.sprite.bounds.extents.x;

        float size = Mathf.Lerp(_startWidth, width, 0.3f);

        ShadowBuild(size);
    }
}
