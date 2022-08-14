using UnityEngine;

namespace Cobilas.Unity.Management.Translation {
    public class ALFBTFlagBase : ScriptableObject {
        [SerializeField] private string gUITarget;
        [SerializeField] private ALFBTLanguage myLang;

        public string GUITarget => gUITarget;
        public ALFBTLanguage Language { get => myLang; internal set => myLang = value; }

        [System.Serializable]
        public sealed class TextField {
            [SerializeField] private string name;
            [SerializeField] private string text;

            public string Name => name;
            public string Text => text;
        }
    }
}