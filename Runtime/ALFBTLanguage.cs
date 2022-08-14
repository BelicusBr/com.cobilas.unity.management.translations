using System.IO;
using UnityEngine;
using Cobilas.Collections;
using Cobilas.IO.Alf.Alfbt;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Cobilas.Unity.Management.Translation {
    [CreateAssetMenu(fileName = "new ALFBTLanguage", menuName = "Translation Manager/ALFBT Language")]
    public class ALFBTLanguage : ScriptableObject {
        [SerializeField] private string lang;
        [SerializeField] private string displayName;
        [SerializeField] private string[] gUITargets;
        [SerializeField] private ALFBTFlagBase[] flags;
#if UNITY_EDITOR
        public bool flags_collaps;
#endif

        public string Language => lang;
        public ALFBTFlagBase[] Flags => flags;
        public string DisplayName => displayName;
        public string[] GUITargets => gUITargets;

#if UNITY_EDITOR
        internal static void Refresh() {
            string[] guis = AssetDatabase.FindAssets("t:ALFBTLanguage", new string[] { "Assets/Resources/Translation" });
            for (int I = 0; I < ArrayManipulation.ArrayLength(guis); I++) {
                ALFBTLanguage language = AssetDatabase.LoadAssetAtPath<ALFBTLanguage>(AssetDatabase.GUIDToAssetPath(guis[I]));
                string[] s_guis = AssetDatabase.FindAssets("t:ALFBTFlagBase", new string[] { Path.GetDirectoryName(AssetDatabase.GetAssetPath(language)).Replace('\\', '/') });
                language.flags = new ALFBTFlagBase[ArrayManipulation.ArrayLength(s_guis)];
                for (int J = 0; J < language.flags.Length; J++) {
                    language.flags[J] = AssetDatabase.LoadAssetAtPath<ALFBTFlagBase>(AssetDatabase.GUIDToAssetPath(s_guis[J]));
                    language.flags[J].Language = language;
                }
                EditorUtility.SetDirty(language);
            }
        }
#endif

        public static IEnumerator<ALFBTWriteTemp> CreateALFBTFile(ALFBTLanguage language) {
            Dictionary<string, ALFBTWriteTemp> pairs = new Dictionary<string, ALFBTWriteTemp>();

            if (ArrayManipulation.EmpytArray(language.flags))
                yield return null;

            for (int I = 0; I < ArrayManipulation.ArrayLength(language.gUITargets); I++)
                pairs.Add(language.gUITargets[I], new ALFBTWriteTemp(language, language.gUITargets[I]));

            for (int I = 0; I < ArrayManipulation.ArrayLength(language.flags); I++) {
                ALFBTFlagBase temp = language.flags[I];
                if (temp is ALFBTMarkingFlag mf) {
                    if (!pairs.ContainsKey(mf.GUITarget))
                        pairs.Add(language.gUITargets[I], new ALFBTWriteTemp(language, mf.GUITarget));
                    for (int J = 0; J < mf.Count; J++)
                        pairs[mf.GUITarget].Write.WriteMarkingFlag(mf.MarkingFields[J].Name, mf.MarkingFields[J].Text);
                } else if (temp is ALFBTTextFlag tf) {
                    if (!pairs.ContainsKey(tf.GUITarget))
                        pairs.Add(language.gUITargets[I], new ALFBTWriteTemp(language, tf.GUITarget));
                    for (int J = 0; J < tf.Count; J++)
                        pairs[tf.GUITarget].Write.WriteTextFlag(tf.TextFields[J].Name, tf.TextFields[J].Text);
                }
            }

            foreach (var item in pairs) {
                item.Value.Write.Dispose();
                yield return item.Value;
            }
        }

        public sealed class ALFBTWriteTemp : System.IDisposable {
            private MemoryStream stream;
            private ALFBTWrite write;

            public ALFBTWrite Write => write;
            public MemoryStream Stream => stream;

            public ALFBTWriteTemp(ALFBTLanguage language, string guiTarget) {
                write = ALFBTWrite.Create(stream = new MemoryStream());
                write.WriteHeaderFlag();

                write.WriteMarkingFlag("language", language.lang);
                write.WriteMarkingFlag("gui_target", guiTarget);
                if (!string.IsNullOrEmpty(language.displayName))
                    write.WriteTextFlag("display_name", language.displayName);
            }

            public void Dispose() {
                stream.Dispose();

                write = (ALFBTWrite)null;
                stream = (MemoryStream)null;
            }
        }
    }
}