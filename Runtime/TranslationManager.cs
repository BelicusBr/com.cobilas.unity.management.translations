using System.IO;
using UnityEngine;
using Cobilas.Collections;
using Cobilas.IO.Alf.Alfbt;
using Cobilas.IO.Alf.Alfbt.Flags;
using Cobilas.IO.Alf.Management.Alfbt;
using Cobilas.Unity.Management.Resources;
using Cobilas.Unity.Management.RuntimeInitialize;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Cobilas.Unity.Management.Translation {
    public static class TranslationManager {

        private static TranslationManagement management = new TranslationManagement();
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void InitEditor() {
            if (!EditorApplication.isPlaying) return;
            Reset();
            Init();
        }
#endif
        [CRIOLM_BeforeSceneLoad]
        private static void Init() {
            Application.quitting += management.Dispose;
            TextAsset[] texts = CobilasResources.GetAllSpecificObjectInFolder<TextAsset>("Resources/Translation");
            for (int I = 0; I < ArrayManipulation.ArrayLength(texts); I++) {
                Debug.Log(texts[I].name);
                using (ALFBTRead read = ALFBTRead.Create(new StringReader(texts[I].text)))
                    Load(read);
            }
        }

        public static void Reset() {
            management.Dispose();
            management = new TranslationManagement();
        }

        public static void Load(ALFBTRead read) => management.LoadTranslation(read);

        public static TextFlag GetTextFlag(string path) => management.GetTextFlag(path);

        public static LanguageInfo[] GetListOfLanguages() => management.GetListOfLanguages();

        public static MarkingFlag GetMarkingFlag(string path) => management.GetMarkingFlag(path);

        public static TranslationCollection GetTranslation(string lang) => management.GetTranslation(lang);
    }
}