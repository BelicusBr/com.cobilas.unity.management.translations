using UnityEngine;
using Cobilas.Unity.Management.Translation;

public class cmt_TDS001 : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        TranslationManager.LanguageSelected = 0;
        for (int I = 0; I < 3; I++)
            Debug.Log(TranslationManager.GetLanguageText($"menu/flag{I + 1}"));
    }
}
