using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorHSVController : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private Tilemap _tilemap;

    private Color _startColor;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        if (!_sprite)
            _tilemap = GetComponent<Tilemap>();

        if (!_sprite && !_tilemap)
            Destroy(this);

        if (_sprite)
            _startColor = _sprite.color;
        else if (_tilemap)
            _startColor = _tilemap.color;
    }

    public void MultiplyColorV(float multi)
    {
        float h = 0.0f;
        float s = 0.0f;
        float v = 0.0f;

        Color.RGBToHSV(_startColor, out h, out s, out v);

        v = v * multi;

        Color newColor = Color.HSVToRGB(h, s, v);
        newColor.a = _startColor.a;
        if (_sprite)
            _sprite.color = newColor;
        else if (_tilemap)
            _tilemap.color = newColor;
    }
}
