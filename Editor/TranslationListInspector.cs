using UnityEditor;
using Cobilas.Unity.Management.Translation;

namespace Cobilas.Unity.Editor.Management.Translation {
    [CustomEditor(typeof(TranslationList))]
    public class TranslationListInspector : UnityEditor.Editor {

        private SerializedProperty p_languages;

        private void OnEnable() {
            p_languages = serializedObject.FindProperty("languages");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField(EditorGUIUtility.TrTempContent("List"), EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUI.BeginDisabledGroup(true);
            for (int I = 0; I < p_languages.arraySize; I++) {
                SerializedProperty p_item = p_languages.GetArrayElementAtIndex(I);
                EditorGUILayout.PropertyField(p_item, EditorGUIUtility.TrTempContent(p_item.objectReferenceValue.name));
            }
            EditorGUI.EndDisabledGroup();
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }
    }
}