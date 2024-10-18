using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyInfo))]
[CanEditMultipleObjects]
public class EnemyInfoEditor : Editor
{
    class GUISet
    {
        public GUIContent content;
        public SerializedProperty property;
    }

    private GUISet _isCanCounterAttacked = new GUISet();
    private GUISet _isAffectedGravity = new GUISet();
    private GUISet _isAffectedTimeSlow = new GUISet();
    private GUISet _isBoss = new GUISet();

    private GUISet _isCanSlow = new GUISet();
    private GUISet _isCanElectric = new GUISet();
    private GUISet _isCanPoison = new GUISet();
    private GUISet _isCanRage = new GUISet();
    private GUISet _isCanWeakness = new GUISet();

    private GUISet _isCanParryingSturn = new GUISet();

    private void Awake()
    {
        GUIContent content;

        content = new GUIContent("Counter Attack");
        _isCanCounterAttacked.content = content;

        content = new GUIContent("Gravity");
        _isAffectedGravity.content = content;

        content = new GUIContent("Time Slow");
        _isAffectedTimeSlow.content = content;

        content = new GUIContent("Is Boss");
        _isBoss.content = content;


        content = new GUIContent("Slow");
        _isCanSlow.content = content;
        content = new GUIContent("Electric");
        _isCanElectric.content = content;
        content = new GUIContent("Poison");
        _isCanPoison.content = content;
        content = new GUIContent("Rage");
        _isCanRage.content = content;
        content = new GUIContent("Weakness");
        _isCanWeakness.content = content;


        content = new GUIContent("Parrying Sturn");
        _isCanParryingSturn.content = content;
    }

    private void OnEnable()
    {
        _isCanCounterAttacked.property = serializedObject.FindProperty("_isCanCounterAttacked");
        _isAffectedGravity.property = serializedObject.FindProperty("_isAffectedGravity");
        _isAffectedTimeSlow.property = serializedObject.FindProperty("_isAffectedTimeSlow");
        _isBoss.property = serializedObject.FindProperty("_isBoss");

        _isCanSlow.property = serializedObject.FindProperty("_isCanSlow");
        _isCanElectric.property = serializedObject.FindProperty("_isCanElectric");
        _isCanPoison.property = serializedObject.FindProperty("_isCanPoison");
        _isCanRage.property = serializedObject.FindProperty("_isCanRage");
        _isCanWeakness.property = serializedObject.FindProperty("_isCanWeakness");

        _isCanParryingSturn.property = serializedObject.FindProperty("_isCanParryingSturn");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_isCanCounterAttacked.property, _isCanCounterAttacked.content);
        EditorGUILayout.PropertyField(_isAffectedGravity.property, _isAffectedGravity.content);
        EditorGUILayout.PropertyField(_isAffectedTimeSlow.property, _isAffectedTimeSlow.content);
        EditorGUILayout.PropertyField(_isBoss.property, _isBoss.content);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Debuff");
        EditorGUILayout.PropertyField(_isCanSlow.property, _isCanSlow.content);
        EditorGUILayout.PropertyField(_isCanElectric.property, _isCanElectric.content);
        EditorGUILayout.PropertyField(_isCanPoison.property, _isCanPoison.content);
        EditorGUILayout.PropertyField(_isCanRage.property, _isCanRage.content);
        EditorGUILayout.PropertyField(_isCanWeakness.property, _isCanWeakness.content);

        EditorGUILayout.Space();

        //EditorGUILayout.PropertyField(_isCanParryingSturn.property, _isCanParryingSturn.content);

        serializedObject.ApplyModifiedProperties();
    }
}
