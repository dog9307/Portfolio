using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicOrderInLayer : MonoBehaviour
{
    private Renderer _renderer;
    private Movable _move;
    private Collider2D _collider;

    private SortingLayerInfo _info;

    public bool isAffectedScale { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _move = GetComponent<Movable>();
        if (!_move)
            _collider = GetComponent<Collider2D>();

        _info = GetComponent<SortingLayerInfo>();
        _renderer = _info.render;
    }

    // Update is called once per frame
    void Update()
    {
        if (_renderer.sortingLayerName != "Dynamic") return;

        int sorting = 0;
        float posY = transform.position.y;
        sorting = (int)(posY * (-100.0f));
        if (_info)
            sorting += (isAffectedScale ? (int)(_info.addtiveOrder * transform.lossyScale.y) : _info.addtiveOrder);

        _renderer.sortingOrder = sorting;
    }
}
