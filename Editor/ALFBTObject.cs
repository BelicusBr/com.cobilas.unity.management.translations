using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using Cobilas.Collections;
using Cobilas.IO.Alf.Alfbt;
using Cobilas.Unity.Management.Build;

namespace Cobilas.Unity.Editor.Management.Translation {
    [CreateAssetMenu(fileName = "new Translation object", menuName = "Cobilas/TranslationManager/Translation object")]
    public class ALFBTObject : ScriptableObject {
        public string Lang;
        public string GUITarget;
        public string DisplayName;

        public ALFBTTextField[] TextFields;

        [InitializeOnLoadMethod]
        private static void InitBuild() {
            CobilasBuildProcessor.EventOnPreprocessBuild += (p, r) => {
                if (p == CobilasEditorProcessor.PriorityProcessor.High)
                    Refresh();
            };
            CobilasEditorProcessor.playModeStateChanged += (pp, pm) => {
                if (pp == CobilasEditorProcessor.PriorityProcessor.High &&
                    pm == PlayModeStateChange.EnteredPlayMode)
                    Refresh();
            };
        }

        private static void Refresh() {
            Debug.Log($"[Translation]Refresh resources paths[{DateTime.Now}]");
            string path = Path.Combine(Application.dataPath, "Resources/Translation").Replace('\\', '/');
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string[] GUIDobjects = AssetDatabase.FindAssets($"t:{nameof(ALFBTObject)}");
            for (int I = 0; I < ArrayManipulation.ArrayLength(GUIDobjects); I++)
                AssetDatabase.LoadAssetAtPath<ALFBTObject>(AssetDatabase.GUIDToAssetPath(GUIDobjects[I])).CreateALFBTFile(path);
            AssetDatabase.Refresh();
        }

        public void CreateALFBTFile(string folder) {
            if (!string.IsNullOrEmpty(Lang)) {
                Debug.Log($"Campo Lang de ({name}) está vazil");
                return;
            }
            using (FileStream stream = File.Create(Path.Combine(folder, $"{name}.txt"))) {
                using (ALFBTWrite write = ALFBTWrite.Create(stream)) {
                    write.WriteHeaderFlag();
                    write.WriteLineBreak();
                    write.WriteMarkingFlag("language", Lang);
                    if (!string.IsNullOrEmpty(DisplayName))
                        write.WriteMarkingFlag("display_name", DisplayName);
                    if (!string.IsNullOrEmpty(GUITarget))
                        write.WriteMarkingFlag("gui_target", GUITarget);
                    write.WriteLineBreak();
                    for (int I = 0; I < ArrayManipulation.ArrayLength(TextFields); I++)
                        write.WriteTextFlag(TextFields[I].Name, TextFields[I].Text);
                }
            }

        }
    }
}