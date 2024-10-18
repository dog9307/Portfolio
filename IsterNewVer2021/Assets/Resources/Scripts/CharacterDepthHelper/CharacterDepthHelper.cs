using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDepthHelper : MonoBehaviour
{
    static GameObject _prefab;

    private SpriteRenderer _parent;
    private SpriteMask _mask;

    public SortingLayerInfo info { get; set; }

    static int MAX = 32767;
    static int MIN = -32768;

    // Start is called before the first frame update
    void Start()
    {
        if (!_prefab)
            _prefab = Resources.Load<GameObject>("Scripts/CharacterDepthHelper/CharacterDepthHelper");

        GameObject helper = Instantiate(_prefab, transform);
        helper.transform.localPosition = Vector3.zero;

        _parent = GetComponent<SpriteRenderer>();
        _mask = helper.GetComponent<SpriteMask>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_mask || !_parent)
        {
            Destroy(this);
            return;
        }

        _mask.sprite = _parent.sprite;

        if (info.sorting == SORTING.Dynamic)
            _mask.frontSortingOrder = _parent.sortingOrder - 100;
    }
}
