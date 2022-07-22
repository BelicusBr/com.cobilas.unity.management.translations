using UnityEngine;
using Cobilas.IO.Alf.Alfbt;
using Cobilas.IO.Alf.Alfbt.Flags;
using Cobilas.IO.Alf.Management.Alfbt;
using Cobilas.Unity.Management.RuntimeInitialize;

namespace Cobilas.Unity.Management.Translation {
    public static class UnityTranslationManager {
        private static TranslationManagement management = new TranslationManagement();

        [CRIOLM_BeforeSceneLoad]
        private static void Init() {
            Application.quitting += management.Dispose;
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