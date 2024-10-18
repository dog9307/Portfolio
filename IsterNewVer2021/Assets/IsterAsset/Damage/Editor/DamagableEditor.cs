using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Damagable))]
[CanEditMultipleObjects]
public class DamagableEditor : Editor
{
    private Damagable _info;

    void Update()
    {
        if (!Application.isPlaying)
            _info.currentHP = _info.totalHP;
    }

    void Awake()
    {
        _info = (Damagable)target;

        Update();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Update();
    }
}
