using System.IO;
using UnityEditor;
using UnityEngine;
using Cobilas.Collections;
using UnityEditorInternal;
using Cobilas.Unity.Management.Translation;

namespace Cobilas.Unity.Editor.Management.Translation {
    [CustomEditor(typeof(ALFBTMarkingFlag))]
    public class ALFBTMarkingFlagInspector : UnityEditor.Editor {

        private SerializedProperty p_myLang;
        private SerializedProperty p_gUITarget;
        private ReorderableList r_markingFields;
        private string[] guiTargets;
        private int guitarget_index;

        private void OnEnable() {
            p_myLang = serializedObject.FindProperty("myLang");
            p_gUITarget = serializedObject.FindProperty("gUITarget");
            guiTargets = new string[] { "gui_global" };
            if (p_myLang.objectReferenceValue != null)
                ArrayManipulation.Add((p_myLang.objectReferenceValue as ALFBTLanguage).GUITargets, ref guiTargets);
            for (int I = 0; I < ArrayManipulation.ArrayLength(guiTargets); I++)
                if (p_gUITarget.stringValue == guiTargets[I]) {
                    guitarget_index = I;
                    break;
                }
            r_markingFields = new ReorderableList(serializedObject, serializedObject.FindProperty("markingFields"));

            r_markingFields.elementHeight = (EditorGUIUtility.singleLineHeight + 2f) * 2f;
            r_markingFields.drawHeaderCallback = DrawHeaderCallback;
            r_markingFields.drawElementCallback = DrawElementCallback;
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField(EditorGUIUtility.TrTempContent("Info"), EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUI.BeginChangeCheck();
            guitarget_index = EditorGUILayout.Popup(EditorGUIUtility.TrTempContent("GUI Target"), guitarget_index, guiTargets);
            if (EditorGUI.EndChangeCheck())
                p_gUITarget.stringValue = guiTargets[guitarget_index];
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(p_myLang, EditorGUIUtility.TrTempContent("Language"));
            EditorGUI.EndDisabledGroup();
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();

            r_markingFields.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }

        private void DrawHeaderCallback(Rect rect)
            => EditorGUI.LabelField(rect, "Marking Flags", EditorStyles.boldLabel);

        private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused) {
            rect.height = EditorGUIUtility.singleLineHeight;
            SerializedProperty p_item = r_markingFields.serializedProperty.GetArrayElementAtIndex(index);
            SerializedProperty p_name = p_item.FindPropertyRelative("name");
            SerializedProperty p_text = p_item.FindPropertyRelative("text");
            EditorGUI.PropertyField(rect, p_name, EditorGUIUtility.TrTempContent("Name"));
            rect.y += rect.height + 2f;
            EditorGUI.PropertyField(rect, p_text, EditorGUIUtility.TrTempContent("Text"));
        }
    }
}