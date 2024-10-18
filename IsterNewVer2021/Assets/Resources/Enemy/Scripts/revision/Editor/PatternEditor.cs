using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PatternFactory))]
public class PatternEditor : Editor
{   
    PatternFactory _factory;

    public List<REnemyAttackPatternBase> attackpattern
    {
        get
        {
            if (!_factory)
                return null;

            return _factory.attackpattern;
        }
    }
    public List<REnemyMovePatternBase> movepattern
    {
        get
        {
            if (!_factory)
                return null;

            return _factory.movepattern;
        }
    }

    private void OnEnable()
    {
        _factory = (PatternFactory)target;
        _factory.Init();
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.LabelField("======Attack Pattern======");
        for (int i = 0; i < attackpattern.Count; ++i)
        {
            if (attackpattern[i] == null)
            {
                attackpattern.RemoveAt(i);
                i--;
                continue;
            }

            REnemyAttackPatternBase apbase = attackpattern[i];
            
            AP prev = apbase.attackType;
            apbase.attackType = (AP)EditorGUILayout.EnumPopup("AttackPattern", apbase.attackType);

            if (prev != apbase.attackType)
            {
                ChangeInstance(apbase, i, apbase.attackType);
                continue;
            }

            if (GUILayout.Button("Remove Pattern"))
            {
                _factory.Release(apbase);
                i--;
                continue;
                // shapes.Add(ShapeFactory.GetShape(SHAPE.Triangle));
            }
            EditorGUILayout.LabelField("-------------------------");
        }

        //새로운 패턴 등록하기.
        if (GUILayout.Button("Create New Pattern"))
        {
            REnemyAttackPatternBase newBase = ReFactory.GetPattern(AP.rangebasic, _factory.gameObject);
            newBase._owner = _factory.GetComponent< REnemyController>();
            _factory.attackpattern.Add(newBase);
           // shapes.Add(ShapeFactory.GetShape(SHAPE.Triangle));
        }
        EditorGUILayout.LabelField("=====Attack Pattern End=====");

        EditorGUILayout.LabelField("======= Move Pattern ======");

        for (int i = 0; i < movepattern.Count; ++i)
        {
            if (movepattern[i] == null)
            {
                movepattern.RemoveAt(i);
                i--;
                continue;
            }

            REnemyMovePatternBase mpbase = movepattern[i];

            MP prev = mpbase.moveType;
            mpbase.moveType = (MP)EditorGUILayout.EnumPopup("MovePattern", mpbase.moveType);

            if (prev != mpbase.moveType)
            {
                ChangeInstance(mpbase, i, mpbase.moveType);
                continue;
            }

            if (GUILayout.Button("Remove Pattern"))
            {
                _factory.Release(mpbase);
                i--;
                continue;
                // shapes.Add(ShapeFactory.GetShape(SHAPE.Triangle));
            }
            EditorGUILayout.LabelField("-------------------------");
        }

        //새로운 패턴 등록하기.
        if (GUILayout.Button("Create New Move Pattern"))
        {
            REnemyMovePatternBase newBase = ReFactory.GetMovePattern(MP.tracking, _factory.gameObject);
            newBase._owner = _factory.GetComponent<REnemyController>();
            _factory.movepattern.Add(newBase);
            // shapes.Add(ShapeFactory.GetShape(SHAPE.Triangle));
        }
        EditorGUILayout.LabelField("===== Move Pattern End =====");

        if (EditorGUI.EndChangeCheck())
        {
            // 컨트롤 z에 등록
            Undo.RecordObject(_factory, "Factory Change");

            // 프리팹에서 저장할 때 필요한듯??
            EditorUtility.SetDirty(_factory);
        }

    }
    
    void ChangeInstance(REnemyAttackPatternBase current, int index, AP newType)
    {
        REnemyAttackPatternBase newShape = ReFactory.GetPattern(newType, current.gameObject);
        attackpattern.Insert(index, newShape);
        _factory.Release(current);
    }
    void ChangeInstance(REnemyMovePatternBase current, int index, MP newType)
    {
        REnemyMovePatternBase newShape = ReFactory.GetMovePattern(newType, current.gameObject);
        movepattern.Insert(index, newShape);
        _factory.Release(current);
    }
}
