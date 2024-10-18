using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteMask))]
public class ShadowedCharacter : MonoBehaviour
{
    private SpriteMask _mask;

    [SerializeField]
    private SpriteRenderer _sprite;

    // Start is called before the first frame update
    void Start()
    {
        _mask = GetComponent<SpriteMask>();
        if (!_mask)
            _mask = gameObject.AddComponent<SpriteMask>();

        if (!_sprite)
            _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_sprite || !_mask)
            return;

        _mask.sprite = _sprite.sprite;
    }
}
