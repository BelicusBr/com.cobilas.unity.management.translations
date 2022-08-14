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
using Cobilas.Unity.Management.Build;
#endif

namespace Cobilas.Unity.Management.Translation {
    public static class TranslationManager {

        private static TranslationManagement management = new TranslationManagement();
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void InitEditor() {
            CobilasBuildProcessor.EventOnPreprocessBuild += (pp, br) => {
                if (pp == CobilasEditorProcessor.PriorityProcessor.High)
                    Refresh();
            };
            CobilasEditorProcessor.projectChanged += (pp) => {
                if (pp == CobilasEditorProcessor.PriorityProcessor.Low)
                    Refresh();
            };
            if (!EditorApplication.isPlaying) return;
            Reset();
            Init();
        }

        [MenuItem("Tools/Translation Manager/Refresh Translation Manager")]
        [CRIOLM_CallWhen(typeof(CobilasResources), CRIOLMType.BeforeSceneLoad)]
        private static void Refresh() {
            Debug.Log(string.Format("[TranslationManager]Refresh [{0}]", System.DateTime.Now));
            TranslationList.Refresh();
        }

        [MenuItem("Tools/Translation Manager/Create Translation Folder")]
        private static void CreateTranslationFolder() {
            string path = Path.Combine(Application.dataPath, "Resources/Translation");
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
                AssetDatabase.Refresh();
            }
        }

        [CRIOLM_CallWhen(typeof(CobilasResources), CRIOLMType.AfterSceneLoad)]
#else
        [CRIOLM_BeforeSceneLoad]
#endif
        private static void Init() {
            Application.quitting += management.Dispose;
            TranslationList[] list = CobilasResources.GetAllSpecificObjectInFolder<TranslationList>("Resources/Translation");
            for (int I = 0; I < ArrayManipulation.ArrayLength(list); I++)
                foreach (var item in list[I])
                    if (item != null)
                        using (item.Stream) {
                            item.Stream.Seek(0, SeekOrigin.Begin);
                            using (ALFBTRead read = ALFBTRead.Create(item.Stream))
                                if (Load(read))
                                    Debug.Log("[MemoryStream]ALFBT load");
                        }
        }

        public static void Reset() {
            management.Dispose();
            management = new TranslationManagement();
        }

        public static bool Load(ALFBTRead read) => management.LoadTranslation(read);

        public static TextFlag GetTextFlag(string path) => management.GetTextFlag(path);

        public static LanguageInfo[] GetListOfLanguages() => management.GetListOfLanguages();

        public static MarkingFlag GetMarkingFlag(string path) => management.GetMarkingFlag(path);

        public static TranslationCollection GetTranslation(string lang) => management.GetTranslation(lang);
    }
}