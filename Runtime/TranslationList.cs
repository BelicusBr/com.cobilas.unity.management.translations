using UnityEngine;
using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace Cobilas.Unity.Management.Translation {
    [CreateAssetMenu(fileName = "new TranslationList", menuName = "Translation Manager/Translation List")]
    public class TranslationList : ScriptableObject, IEnumerable<ALFBTWriteTemp> {
        [SerializeField] private ALFBTLangBase[] languages;

        public ALFBTLangBase[] Languages => languages;

#if UNITY_EDITOR
        internal static void Refresh() {
            ALFBTLangBase.Refresh();
            string[] guis = AssetDatabase.FindAssets($"t:{nameof(TranslationList)}", new string[] { "Assets/Resources/Translation" });
            for (int I = 0; I < ArrayManipulation.ArrayLength(guis); I++) {
                TranslationList language = AssetDatabase.LoadAssetAtPath<TranslationList>(AssetDatabase.GUIDToAssetPath(guis[I]));
                string[] s_guis = AssetDatabase.FindAssets($"t:{nameof(ALFBTHeader)}", new string[] { Path.GetDirectoryName(AssetDatabase.GetAssetPath(language)).Replace('\\', '/') });
                language.languages = new ALFBTLangBase[ArrayManipulation.ArrayLength(s_guis)];
                for (int J = 0; J < language.languages.Length; J++)
                    language.languages[J] = AssetDatabase.LoadAssetAtPath<ALFBTLangBase>(AssetDatabase.GUIDToAssetPath(s_guis[J]));
                EditorUtility.SetDirty(language);
            }
        }
#endif

        public IEnumerator<ALFBTWriteTemp> GetEnumerator() {
            for (int I = 0; I < ArrayManipulation.ArrayLength(languages); I++)
                yield return new ALFBTWriteTemp(languages[I]);
        }

        IEnumerator IEnumerable.GetEnumerator()
            => (this as IEnumerable<ALFBTWriteTemp>).GetEnumerator();
    }
}