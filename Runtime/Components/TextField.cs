using System;
using UnityEngine;

namespace Cobilas.Unity.Management.Translation {
    [Serializable]
    public sealed class TextField {
#if UNITY_EDITOR
        public bool flags_collaps;
#endif
        [SerializeField] private string name;
        [SerializeField] private string text;

        public string Name => name;
        public string Text => text;
    }
}
