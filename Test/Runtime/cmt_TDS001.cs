using UnityEngine;
using Cobilas.Unity.Management.Translation;

public class cmt_TDS001 : MonoBehaviour {
    // Start is called before the first frame update
    public string gcName;
    public string gcValue;
    public string cfName;
    public string cfValue;
    public string stName;
    public string stValue;
    void Start() {
        gcName = TranslationManager.GetMarkingFlag("PT_BR.menu/graphic.Graphic").Name;
        gcValue = TranslationManager.GetMarkingFlag("PT_BR.menu/graphic.Graphic").Value;
        cfName = TranslationManager.GetMarkingFlag("PT_BR.menu/config.Config").Name;
        cfValue = TranslationManager.GetMarkingFlag("PT_BR.menu/config.Config").Value;
        stName = TranslationManager.GetTextFlag("PT_BR.menu/status.Status").Name;
        stValue = TranslationManager.GetTextFlag("PT_BR.menu/status.Status").Value;
    }

    private void OnGUI()
    {
        Rect rect = new Rect(0f, 75f, 330f, 25f);
        GUI.Label(rect, string.Format("[mf|Name:{0}] Value:{1}", gcName, gcValue));
        rect.y += 25f;
        GUI.Label(rect, string.Format("[mf|Name:{0}] Value:{1}", cfName, cfValue));
        rect.y += 25f;
        GUI.Label(rect, string.Format("[tf|Name:{0}] Value:{1}", stName, stValue));
    }
}
