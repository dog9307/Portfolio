using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SORTING
{
    NONE = -1,
    Background,
    Shadow,
    Dynamic,
    Bullet,
    Effect,
    Foreground,
    Depth,
    END
}

public class SortingLayerInfo : MonoBehaviour
{
    [SerializeField]
    private SORTING _sorting;
    public SORTING sorting { get { return _sorting; } set { _sorting = value; } }

    [SerializeField]
    private int _orderInLayer;
    public int orderInLayer { get { return _orderInLayer; } set { _orderInLayer = value; } }

    [SerializeField]
    private int _additiveOrder = 0;
    public int addtiveOrder { get { return _additiveOrder; } set { _additiveOrder = value; } }

    [SerializeField]
    [HideInInspector]
    private Renderer _renderer;
    public Renderer render { get { return _renderer; } }

    [SerializeField]
    private bool _isCanSeeThrough = false;

    [SerializeField]
    private bool _isAffectedScale = false;

    // Start is called before the first frame update
    void Start()
    {
        ApplySortingLayer();

        if (_sorting == SORTING.Dynamic)
        {
            DynamicOrderInLayer order = GetComponent<DynamicOrderInLayer>();
            if (!order)
            {
                order = gameObject.AddComponent<DynamicOrderInLayer>();
                order.isAffectedScale = _isAffectedScale;
            }
        }

        if (_isCanSeeThrough)
        {
            switch (_sorting)
            {
                case SORTING.Dynamic:
                case SORTING.Foreground:
                    CharacterDepthHelper depth = GetComponent<CharacterDepthHelper>();
                    if (!depth)
                        depth = gameObject.AddComponent<CharacterDepthHelper>();

                    depth.info = this;
                    break;
            }
        }
    }

    public void ApplySortingLayer()
    {
        if (!_renderer)
        {
            _renderer = GetComponent<SpriteRenderer>();
            if (!_renderer)
            {
                ParticleSystem particle = GetComponent<ParticleSystem>();
                if (particle)
                    _renderer = particle.GetComponent<ParticleSystemRenderer>();
                else
                {
                    _renderer = GetComponent<Renderer>();

                    if (!_renderer)
                    return;
                }
            }
        }

        _renderer.sortingLayerName = _sorting.ToString();
        _renderer.sortingOrder = _orderInLayer;
    }

    public void ChangeSortingLayer(string newSorting)
    {
        _renderer.sortingLayerName = newSorting;
        _renderer.sortingOrder = _orderInLayer;
    }

    public void ReturnSorting()
    {
        _renderer.sortingLayerName = _sorting.ToString();
        _renderer.sortingOrder = _orderInLayer;
    }
}
