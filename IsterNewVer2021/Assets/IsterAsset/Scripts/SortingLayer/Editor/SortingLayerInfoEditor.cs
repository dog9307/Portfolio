using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SortingLayerInfo))]
[CanEditMultipleObjects]
public class SortingLayerInfoEditor : Editor
{
    private SortingLayerInfo _info;

    void Update()
    {
        if (!Application.isPlaying)
            _info.ApplySortingLayer();
    }

    void Awake()
    {
        _info = (SortingLayerInfo)target;

        Update();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Update();
    }
}

