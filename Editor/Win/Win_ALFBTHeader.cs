using UnityEngine;
using UnityEditor;
using Cobilas.Unity.Management.Translation;

namespace Cobilas.Unity.Editor.Management.Translation {
    public class Win_ALFBTHeader : EditorWindow {
        public static void Init(ALFBTHeader header) {
            Win_ALFBTHeader win = GetWindow<Win_ALFBTHeader>();
            win.header = header;
            win.titleContent = new GUIContent("Header editor", Resources.Load<Texture2D>("Google-Translate-icon"));
            win.OnEnable();
            win.Show();
        }

        private ALFBTHeader header;
        private SerializedObject serializedObject;
        private SerializedProperty prop_otherFields;

        private void OnEnable() {
            if (header == null) return;
            prop_otherFields = (serializedObject = new SerializedObject(header)).FindProperty("otherFields");
        }

        private void OnGUI() {
            serializedObject.Update();
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            EditorGUILayout.LabelField(header.name, EditorStyles.boldLabel);
            if (Button("Clear", 50f))
                prop_otherFields.arraySize = 0;
            if (Button("Add", 50f))
                AddList();
            EditorGUILayout.EndHorizontal();
            for (int I = 0; I < prop_otherFields.arraySize; I++)
                DrawTextField(prop_otherFields.GetArrayElementAtIndex(I), I);
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(header);
        }

        private void DrawTextField(SerializedProperty prop, int index) {
            SerializedProperty prop_name = prop.FindPropertyRelative("name");
            SerializedProperty prop_text = prop.FindPropertyRelative("text");
            SerializedProperty prop_foldout = prop.FindPropertyRelative("flags_collaps");

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal();
            prop_foldout.boolValue = EditorGUILayout.Foldout(prop_foldout.boolValue, prop_name.stringValue);
            if (Button("Remove", 55f)) {
                prop_otherFields.DeleteArrayElementAtIndex(index);
                return;
            }
            EditorGUILayout.EndHorizontal();
            if (prop_foldout.boolValue) {
                ++EditorGUI.indentLevel;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Path:", EditorStyles.boldLabel, GUILayout.Width(50f));
                prop_name.stringValue = EditorGUILayout.TextField(prop_name.stringValue);
                EditorGUILayout.EndHorizontal();
                prop_text.stringValue = EditorGUILayout.TextArea(prop_text.stringValue, GUILayout.Height(130f));
                --EditorGUI.indentLevel;
            }
            EditorGUILayout.EndVertical();
        }

        private void AddList() {
            prop_otherFields.arraySize += 1;
            SerializedProperty prop = prop_otherFields.GetArrayElementAtIndex(prop_otherFields.arraySize - 1);
            prop.FindPropertyRelative("name").stringValue = string.Empty;
            prop.FindPropertyRelative("text").stringValue = string.Empty;
            prop.FindPropertyRelative("flags_collaps").boolValue = false;
        }

        private bool Button(string text, float width)
            => GUILayout.Button(text, GUILayout.Width(width));
    }
}
