using System.IO;
using UnityEngine;
using Cobilas.Collections;
using Cobilas.IO.Alf.Alfbt;
using Cobilas.IO.Alf.Alfbt.Language;
using Cobilas.Unity.Management.Runtime;
using Cobilas.Unity.Management.Resources;
#if UNITY_EDITOR
using UnityEditor;
using Cobilas.Unity.Management.Build;
#endif

namespace Cobilas.Unity.Management.Translation {
    public static class TranslationManager {

        public static int LanguageSelected;

        private static LanguageManager management = new LanguageManager();
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void InitEditor() {
            CobilasBuildProcessor.EventOnPreprocessBuild += (pp, br) => {
                if (pp == CobilasEditorProcessor.PriorityProcessor.High)
                    Refresh();
            };
            CobilasEditorProcessor.playModeStateChanged += (pr, pm) => {
                if (pr == CobilasEditorProcessor.PriorityProcessor.Low &&
                    pm == PlayModeStateChange.EnteredPlayMode)
                    Refresh();                
            };
            if (!EditorApplication.isPlaying) return;
            Reset();
            Init();
        }

        [MenuItem("Tools/Translation Manager/Refresh Translation Manager")]
        //[CRIOLM_CallWhen(typeof(CobilasResources), CRIOLMType.BeforeSceneLoad)]
        [CallWhenStart(InitializePriority.High, "#ResourceManager")]
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

        //[CRIOLM_CallWhen(typeof(CobilasResources), CRIOLMType.AfterSceneLoad)]
        [CallWhenStart(InitializePriority.Low, "#ResourceManager")]
#else
        [StartBeforeSceneLoad("#TranslationManager")]
#endif
        private static void Init() {
            Application.quitting += management.Dispose;
            TranslationList[] list = ResourceManager.GetAllSpecificObjectInFolder<TranslationList>("Resources/Translation");
            for (int I = 0; I < ArrayManipulation.ArrayLength(list); I++)
                foreach (var item in list[I])
                    if (item != null)
                        using (item) {
                            using (ALFBTRead readheader = ALFBTRead.Create(item.Header)) {
                                using (ALFBTRead readvalue = ALFBTRead.Create(item.Stream))
                                    if (Load(readheader, readvalue))
                                        Debug.Log("[MemoryStream]ALFBT load");
                            }
                            item.Header.Dispose();
                            item.Stream.Dispose();
                        }
        }

        public static void Reset() {
            management.Dispose();
            management = new LanguageManager();
        }

        public static bool Load(ALFBTRead header, ALFBTRead values) 
            => management.Add(header.ReadOnly, values.ReadOnly);

        public static string GetLanguageText(string path)
            => GetLanguageText(LanguageSelected, path);

        public static string GetLanguageText(int index, string path) 
            => management.GetLanguageText(index, path);

        public static string GetLanguageText(string langTarget, string path) 
            => management.GetLanguageText(langTarget, path);

        public static string GetManifestText(string flagName) 
            => GetManifestText(LanguageSelected, flagName);

        public static string GetManifestText(int index, string flagName) 
            => management[index].GetManifestText(flagName);

        public static string GetManifestText(string langTarget, string flagName) 
            => management[langTarget].GetManifestText(flagName);

        public static LanguageInfo[] GetListOfLanguages() {
            LanguageInfo[] res = new LanguageInfo[management.Count];
            for (int I = 0; I < res.Length; I++)
                res[I] = new LanguageInfo(management[I].GetManifestText("lang_target"), management[I].GetManifestText("lang_display"));
            return res;
        }
    }
}