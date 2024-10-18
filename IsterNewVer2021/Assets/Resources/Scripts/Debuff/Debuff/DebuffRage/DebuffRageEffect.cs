using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffRageEffect : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private SpriteRenderer _owner;

    // Start is called before the first frame update
    void Start()
    {
        _owner = transform.parent.GetComponent<SpriteRenderer>();
        _renderer = GetComponent<SpriteRenderer>();

        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        _renderer.sprite = _owner.sprite;
    }
}
