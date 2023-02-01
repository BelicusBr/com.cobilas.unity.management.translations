using UnityEngine;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using Cobilas.Collections;
#endif

namespace Cobilas.Unity.Management.Translation {
    public abstract class ALFBTLangBase : ScriptableObject {
#if UNITY_EDITOR
        public bool flags_collaps;
#endif

        public abstract string Language { get; }
        public abstract string DisplayName { get; }
        public abstract TextField[] OtherFields { get; }
        public abstract ALFBTTextFlag[] Flags { get; internal set; }

#if UNITY_EDITOR
        internal static void Refresh() {
            string[] guis = AssetDatabase.FindAssets($"t:{nameof(ALFBTLangBase)}", new string[] { "Assets/Resources/Translation" });
            for (int I = 0; I < ArrayManipulation.ArrayLength(guis); I++) {
                ALFBTLangBase language = AssetDatabase.LoadAssetAtPath<ALFBTLangBase>(AssetDatabase.GUIDToAssetPath(guis[I]));
                string[] s_guis = AssetDatabase.FindAssets($"t:{nameof(ALFBTTextFlag)}", new string[] { Path.GetDirectoryName(AssetDatabase.GetAssetPath(language)).Replace('\\', '/') });
                ALFBTTextFlag[] flags = new ALFBTTextFlag[ArrayManipulation.ArrayLength(s_guis)];
                for (int J = 0; J < flags.Length; J++) {
                    flags[J] = AssetDatabase.LoadAssetAtPath<ALFBTTextFlag>(AssetDatabase.GUIDToAssetPath(s_guis[J]));
                    flags[J].Language = language;
                }
                language.Flags = flags;
                EditorUtility.SetDirty(language);
            }
        }
#endif
    }
}