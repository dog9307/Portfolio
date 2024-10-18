﻿using UnityEngine;
using UnityEditor;

namespace Yarn.Unity.Editor
{
    [CustomEditor(typeof(CustomLineView))]
    public class CustomLineViewEditor : UnityEditor.Editor
    {
        private SerializedProperty canvasGroupProperty;
        private SerializedProperty runnerProperty;
        private SerializedProperty useFadeEffectProperty;
        private SerializedProperty fadeInTimeProperty;
        private SerializedProperty fadeOutTimeProperty;
        private SerializedProperty lineTextProperty;
        private SerializedProperty showCharacterNamePropertyInLineViewProperty;
        private SerializedProperty characterNameTextProperty;
        private SerializedProperty useTypewriterEffectProperty;
        private SerializedProperty onCharacterTypedProperty;
        private SerializedProperty typewriterEffectSpeedProperty;

        private SerializedProperty continueButtonProperty;

        private SerializedProperty autoAdvanceDialogueProperty;
        private SerializedProperty holdDelayProperty;

        private SerializedProperty sfxProperty;

        public void OnEnable()
        {
            canvasGroupProperty = serializedObject.FindProperty(nameof(CustomLineView.canvasGroup));
            runnerProperty = serializedObject.FindProperty(nameof(CustomLineView._runner));
            useFadeEffectProperty = serializedObject.FindProperty(nameof(CustomLineView.useFadeEffect));
            fadeInTimeProperty = serializedObject.FindProperty(nameof(CustomLineView.fadeInTime));
            fadeOutTimeProperty = serializedObject.FindProperty(nameof(CustomLineView.fadeOutTime));
            lineTextProperty = serializedObject.FindProperty(nameof(CustomLineView.lineText));
            showCharacterNamePropertyInLineViewProperty = serializedObject.FindProperty(nameof(CustomLineView.showCharacterNameInLineView));
            characterNameTextProperty = serializedObject.FindProperty(nameof(CustomLineView.characterNameText));
            useTypewriterEffectProperty = serializedObject.FindProperty(nameof(CustomLineView.useTypewriterEffect));
            onCharacterTypedProperty = serializedObject.FindProperty(nameof(CustomLineView.onCharacterTyped));
            typewriterEffectSpeedProperty = serializedObject.FindProperty(nameof(CustomLineView.typewriterEffectSpeed));
            continueButtonProperty = serializedObject.FindProperty(nameof(CustomLineView.continueButton));
            autoAdvanceDialogueProperty = serializedObject.FindProperty(nameof(CustomLineView.autoAdvance));
            holdDelayProperty = serializedObject.FindProperty(nameof(CustomLineView.holdTime));
            //sfxProperty = serializedObject.FindProperty(nameof(CustomLineView._sfx));
        }

        public override void OnInspectorGUI()
        {
            var baseProperties = new[] {
                canvasGroupProperty,

                runnerProperty,

                lineTextProperty,

                autoAdvanceDialogueProperty,
            };
            foreach (var prop in baseProperties)
            {
                EditorGUILayout.PropertyField(prop);
            }

            if (autoAdvanceDialogueProperty.boolValue)
            {
                EditorGUI.indentLevel += 1;
                EditorGUILayout.PropertyField(holdDelayProperty);
                EditorGUI.indentLevel -= 1;
            }

            EditorGUILayout.PropertyField(useFadeEffectProperty);

            if (useFadeEffectProperty.boolValue)
            {
                EditorGUI.indentLevel += 1;
                EditorGUILayout.PropertyField(fadeInTimeProperty);
                EditorGUILayout.PropertyField(fadeOutTimeProperty);
                EditorGUI.indentLevel -= 1;
            }


            EditorGUILayout.PropertyField(useTypewriterEffectProperty);

            if (useTypewriterEffectProperty.boolValue)
            {
                EditorGUI.indentLevel += 1;
                EditorGUILayout.PropertyField(onCharacterTypedProperty);
                EditorGUILayout.PropertyField(typewriterEffectSpeedProperty);
                EditorGUI.indentLevel -= 1;
            }

            EditorGUILayout.PropertyField(characterNameTextProperty);

            if (characterNameTextProperty.objectReferenceValue == null)
            {
                EditorGUI.indentLevel += 1;
                EditorGUILayout.PropertyField(showCharacterNamePropertyInLineViewProperty);
                EditorGUI.indentLevel -= 1;
            }

            EditorGUILayout.PropertyField(continueButtonProperty);

            //EditorGUILayout.PropertyField(sfxProperty);

            serializedObject.ApplyModifiedProperties();

        }
    }

}
