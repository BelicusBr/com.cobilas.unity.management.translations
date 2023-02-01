using UnityEditor;
using UnityEngine;
using Cobilas.Unity.Management.Translation;

namespace Cobilas.Unity.Editor.Management.Translation {
    [CustomEditor(typeof(ALFBTTextFlag))]
    public class ALFBTTextFlagInspector : UnityEditor.Editor {

        private GUIContent bt_open;
        private SerializedProperty textlist;
        //textFields
        private void OnEnable() {
            textlist = serializedObject.FindProperty("textFields");
            bt_open = new GUIContent("Open text flag editor", Resources.Load<Texture2D>("Google-Translate-icon"));
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("language"));
            EditorGUI.EndDisabledGroup();

            if (GUILayout.Button(bt_open, GUILayout.Height(25f)))
                Win_ALFBTTextFlag.DoIt(target as ALFBTTextFlag);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Text list", EditorStyles.boldLabel);
            EditorGUILayout.EndVertical();
            if (textlist.arraySize != 0) {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                for (int I = 0; I < textlist.arraySize; I++)
                    EditorGUILayout.LabelField(textlist.GetArrayElementAtIndex(I).FindPropertyRelative("name").stringValue, EditorStyles.helpBox);
                EditorGUILayout.EndVertical();
            }

            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }
    }
}