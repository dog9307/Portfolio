using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAdditiveHelper : MonoBehaviour
{
    private SortingLayerInfo _info;
    [SerializeField]
    private Transform _pos;
    public Transform pos { get { return _pos; } set { _pos = value; } }

    [SerializeField]
    private int _additive = 0;

    // Start is called before the first frame update
    void Start()
    {
        _info = GetComponent<SortingLayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_pos) return;
        if (!_info) return;

        _info.addtiveOrder = (int)((transform.position.y - _pos.position.y) * 100.0f) + _additive;
    }
}
