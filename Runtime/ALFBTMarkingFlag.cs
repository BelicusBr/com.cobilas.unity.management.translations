using UnityEngine;
using Cobilas.Collections;

namespace Cobilas.Unity.Management.Translation {
    [CreateAssetMenu(fileName = "new ALFBTMarkingFlag", menuName = "Translation Manager/ALFBT MarkingFlag")]
    public class ALFBTMarkingFlag : ALFBTFlagBase {
        [SerializeField]
        private TextField[] markingFields;

        public TextField[] MarkingFields => markingFields;
        public int Count => ArrayManipulation.ArrayLength(markingFields);
    }
}