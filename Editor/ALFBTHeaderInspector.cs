using UnityEditor;
using UnityEngine;
using Cobilas.Unity.Management.Translation;

namespace Cobilas.Unity.Editor.Management.Translation {
    [CustomEditor(typeof(ALFBTHeader))]
    public class ALFBTHeaderInspector : UnityEditor.Editor {

        private bool foldout_flags;
        private bool foldout_otherFields;
        private SerializedProperty prop_flags;
        private SerializedProperty prop_language;
        private SerializedProperty prop_displayName;
        private SerializedProperty prop_otherFields;

        private void OnEnable() {
            prop_flags = serializedObject.FindProperty("flags");
            prop_language = serializedObject.FindProperty("language");
            prop_displayName = serializedObject.FindProperty("displayName");
            prop_otherFields = serializedObject.FindProperty("otherFields");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Header", EditorStyles.boldLabel);
            ++EditorGUI.indentLevel;
            EditorGUILayout.PropertyField(prop_language);
            if (string.IsNullOrEmpty(prop_language.stringValue))
                EditorGUILayout.HelpBox("The field must be filled.", MessageType.Warning);
            EditorGUILayout.PropertyField(prop_displayName);
            --EditorGUI.indentLevel;
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            ++EditorGUI.indentLevel;
            EditorGUILayout.BeginHorizontal();
            foldout_otherFields = EditorGUILayout.Foldout(foldout_otherFields, "Other fields");
            if (GUILayout.Button("Editor", GUILayout.Width(50f)))
                Win_ALFBTHeader.Init(target as ALFBTHeader);
            EditorGUILayout.EndHorizontal();

            if (foldout_otherFields)
                for (int I = 0; I < prop_otherFields.arraySize; I++)
                    EditorGUILayout.LabelField(prop_otherFields.GetArrayElementAtIndex(I).FindPropertyRelative("name").stringValue, EditorStyles.helpBox);
            
            --EditorGUI.indentLevel;
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            ++EditorGUI.indentLevel;
            if (foldout_flags = EditorGUILayout.Foldout(foldout_flags, "Flags")) {
                EditorGUI.BeginDisabledGroup(true);
                ++EditorGUI.indentLevel;
                for (int I = 0; I < prop_flags.arraySize; I++)
                    EditorGUILayout.ObjectField(prop_flags.GetArrayElementAtIndex(I));
                --EditorGUI.indentLevel;
                EditorGUI.EndDisabledGroup();
            }
            --EditorGUI.indentLevel;
            EditorGUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }
    }
}