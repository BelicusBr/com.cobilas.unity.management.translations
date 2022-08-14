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
    public class TranslationList : ScriptableObject, IEnumerable<ALFBTLanguage.ALFBTWriteTemp> {
        [SerializeField] private ALFBTLanguage[] languages;

        public ALFBTLanguage[] Languages => languages;

#if UNITY_EDITOR

        internal static void Refresh() {
            ALFBTLanguage.Refresh();
            string[] guis = AssetDatabase.FindAssets("t:TranslationList", new string[] { "Assets/Resources/Translation" });
            for (int I = 0; I < ArrayManipulation.ArrayLength(guis); I++) {
                TranslationList language = AssetDatabase.LoadAssetAtPath<TranslationList>(AssetDatabase.GUIDToAssetPath(guis[I]));
                string[] s_guis = AssetDatabase.FindAssets("t:ALFBTLanguage", new string[] { Path.GetDirectoryName(AssetDatabase.GetAssetPath(language)).Replace('\\', '/') });
                language.languages = new ALFBTLanguage[ArrayManipulation.ArrayLength(s_guis)];
                for (int J = 0; J < language.languages.Length; J++)
                    language.languages[J] = AssetDatabase.LoadAssetAtPath<ALFBTLanguage>(AssetDatabase.GUIDToAssetPath(s_guis[J]));
                EditorUtility.SetDirty(language);
            }
        }
#endif

        public IEnumerator<ALFBTLanguage.ALFBTWriteTemp> GetEnumerator()
            => IGetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => IGetEnumerator();

        private IEnumerator<ALFBTLanguage.ALFBTWriteTemp> IGetEnumerator() {
            IEnumerator<ALFBTLanguage.ALFBTWriteTemp> enumerator;
            for (int I = 0; I < ArrayManipulation.ArrayLength(languages); I++) {
                enumerator = ALFBTLanguage.CreateALFBTFile(languages[I]);
                while (enumerator.MoveNext())
                    yield return enumerator.Current;
            }
        }
    }
}