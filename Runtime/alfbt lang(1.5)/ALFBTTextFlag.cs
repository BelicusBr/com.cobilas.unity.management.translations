using UnityEngine;
using Cobilas.Collections;

namespace Cobilas.Unity.Management.Translation {
    [CreateAssetMenu(fileName = "new ALFBTTextFlag", menuName = "Translation Manager/ALFBT TextFlag")]
    public class ALFBTTextFlag : ScriptableObject {
        [SerializeField] private ALFBTLangBase language;
        [SerializeField] private TextField[] textFields;

        public TextField[] TextFields => textFields;
        public int Count => ArrayManipulation.ArrayLength(textFields);
        public ALFBTLangBase Language { get => language; internal set => language = value; }
    }
}