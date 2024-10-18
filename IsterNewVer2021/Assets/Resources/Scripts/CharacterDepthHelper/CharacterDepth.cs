using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDepth : MonoBehaviour
{
    private SpriteRenderer _parent;
    private SpriteRenderer _child;

    // Start is called before the first frame update
    void Start()
    {
        _parent = transform.parent.GetComponent<SpriteRenderer>();
        if (!_parent)
        {
            Destroy(gameObject);
            return;
        }

        _child = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_parent && _child)
        {
            _child.sprite = _parent.sprite;
            _child.sortingOrder = _parent.sortingOrder + 1;
        }
    }
}
