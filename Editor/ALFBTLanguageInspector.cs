using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using Cobilas.Unity.Management.Translation;

namespace Cobilas.Unity.Editor.Management.Translation {
    [CustomEditor(typeof(ALFBTLanguage))]
    public class ALFBTLanguageInspector : UnityEditor.Editor {

        private SerializedProperty p_lang;
        private SerializedProperty p_flags;
        private ReorderableList r_gUITargets;
        private SerializedProperty p_displayName;
        private SerializedProperty p_flags_collaps;

        private void OnEnable() {
            p_lang = serializedObject.FindProperty("lang");
            p_flags = serializedObject.FindProperty("flags");
            p_displayName = serializedObject.FindProperty("displayName");
            p_flags_collaps = serializedObject.FindProperty("flags_collaps");
            r_gUITargets = new ReorderableList(serializedObject, serializedObject.FindProperty("gUITargets"));
            
            r_gUITargets.elementHeight = EditorGUIUtility.singleLineHeight + 2f;
            r_gUITargets.drawHeaderCallback = DrawHeaderCallback;
            r_gUITargets.drawElementCallback = DrawElementCallback;
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField(EditorGUIUtility.TrTempContent("Info"), EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(p_lang, EditorGUIUtility.TrTempContent("Language"));
            EditorGUILayout.PropertyField(p_displayName, EditorGUIUtility.TrTempContent("Display name"));
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUI.indentLevel++;
            if (p_flags_collaps.boolValue = EditorGUILayout.Foldout(p_flags_collaps.boolValue, EditorGUIUtility.TrTempContent("Flags"))) {
                EditorGUI.indentLevel++;
                EditorGUI.BeginDisabledGroup(true);
                for (int I = 0; I < p_flags.arraySize; I++) {
                    SerializedProperty p_prop = p_flags.GetArrayElementAtIndex(I);
                    EditorGUILayout.PropertyField(p_prop, EditorGUIUtility.TrTempContent(p_prop.objectReferenceValue.name));
                }
                EditorGUI.EndDisabledGroup();
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();

            r_gUITargets.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }

        private void DrawHeaderCallback(Rect rect)
            => EditorGUI.LabelField(rect, "GUI targets", EditorStyles.boldLabel);

        private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused) {
            rect.height = EditorGUIUtility.singleLineHeight;
            SerializedProperty p_item = r_gUITargets.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.BeginChangeCheck();
            string txt = EditorGUI.TextField(rect, p_item.stringValue);
            if (EditorGUI.EndChangeCheck())
                p_item.stringValue = txt;
        }
    }
}