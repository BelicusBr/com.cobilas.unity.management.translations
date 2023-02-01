using UnityEngine;

namespace Cobilas.Unity.Management.Translation {
    [CreateAssetMenu(fileName = "new ALFBTHeader", menuName = "Translation Manager/ALFBT Header")]
    public class ALFBTHeader : ALFBTLangBase {
        [SerializeField] private string language;
        [SerializeField] private string displayName;
        [SerializeField] private ALFBTTextFlag[] flags;
        [SerializeField] private TextField[] otherFields;

        public override string Language => language;
        public override string DisplayName => displayName;
        public override TextField[] OtherFields => otherFields;
        public override ALFBTTextFlag[] Flags { get => flags; internal set => flags = value; }
    }
}
