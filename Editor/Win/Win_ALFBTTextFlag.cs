using UnityEditor;
using UnityEngine;
using Cobilas.Unity.Management.Translation;

namespace Cobilas.Unity.Editor.Management.Translation {
    public class Win_ALFBTTextFlag : EditorWindow {
        public static void DoIt(ALFBTTextFlag objtemp) {
            Win_ALFBTTextFlag win = GetWindow<Win_ALFBTTextFlag>();
            win.objtemp = objtemp;
            win.titleContent = new GUIContent("Text flag editor", Resources.Load<Texture2D>("Google-Translate-icon"));
            win.OnEnable();
            win.Show();
        }

        private SerializedProperty textlist;
        private SerializedObject serializedObject;
        [SerializeField] private ALFBTTextFlag objtemp;

        private void OnEnable() {
            if (objtemp == null) return;
            serializedObject = new SerializedObject(objtemp);
            textlist = serializedObject.FindProperty("textFields");
        }

        private void OnGUI() {
            serializedObject.Update();
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            EditorGUILayout.LabelField(objtemp.name, EditorStyles.boldLabel);
            if (Button("Clear", 50f))
                textlist.arraySize = 0;
            if (Button("Add", 50f))
                AddList();
            EditorGUILayout.EndHorizontal();
            for (int I = 0; I < textlist.arraySize; I++)
                DrawTextField(textlist.GetArrayElementAtIndex(I), I);
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(objtemp);
        }

        private void DrawTextField(SerializedProperty prop, int index) {
            SerializedProperty prop_name = prop.FindPropertyRelative("name");
            SerializedProperty prop_text = prop.FindPropertyRelative("text");
            SerializedProperty prop_foldout = prop.FindPropertyRelative("flags_collaps");
            
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal();
            prop_foldout.boolValue = EditorGUILayout.Foldout(prop_foldout.boolValue, prop_name.stringValue);
            if (Button("Remove", 55f)) {
                textlist.DeleteArrayElementAtIndex(index);
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
            textlist.arraySize += 1;
            SerializedProperty prop = textlist.GetArrayElementAtIndex(textlist.arraySize - 1);
            prop.FindPropertyRelative("name").stringValue = string.Empty;
            prop.FindPropertyRelative("text").stringValue = string.Empty;
            prop.FindPropertyRelative("flags_collaps").boolValue = false;
        }

        private bool Button(string text, float width)
            => GUILayout.Button(text, GUILayout.Width(width));
    }
}