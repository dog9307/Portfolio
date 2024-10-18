using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskRangeChanger : MonoBehaviour
{
    [SerializeField]
    private List<SpriteMask> _maskList;

    struct SpriteMaskRangeInfo
    {
        public int frontID;
        public int backID;
    }

    private Dictionary<SpriteMask, SpriteMaskRangeInfo> _maskDic = new Dictionary<SpriteMask, SpriteMaskRangeInfo>();

    void Start()
    {
        foreach (var m in _maskList)
        {
            SpriteMaskRangeInfo range = new SpriteMaskRangeInfo();

            range.frontID = m.frontSortingLayerID;
            range.backID = m.backSortingLayerID;

            _maskDic.Add(m, range);
        }
    }

    public void ResetRange()
    {
        foreach (var m in _maskDic)
        {
            m.Key.frontSortingLayerID = m.Value.frontID;
            m.Key.backSortingLayerID = m.Value.backID;
        }
    }

    public void ChangeRange(string layerName)
    {
        if (_maskList == null) return;

        foreach (var m in _maskList)
        {
            m.frontSortingLayerID = SortingLayer.NameToID(layerName);
            m.backSortingLayerID = SortingLayer.NameToID(layerName);
        }
    }

    public void ChangeRangeFront(string layerName)
    {
        if (_maskList == null) return;

        foreach (var m in _maskList)
            m.frontSortingLayerID = SortingLayer.NameToID(layerName);
    }

    public void ChangeRangeBack(string layerName)
    {
        if (_maskList == null) return;

        foreach (var m in _maskList)
            m.backSortingLayerID = SortingLayer.NameToID(layerName);
    }
}
