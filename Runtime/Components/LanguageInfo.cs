namespace Cobilas.Unity.Management.Translation {
    public readonly struct LanguageInfo {
        private readonly string language;
        private readonly string displayName;

        public string Language => language;
        public string DisplayName => displayName;

        public LanguageInfo(string language, string displayName) {
            this.language = language;
            this.displayName = displayName;
        }
    }
}
