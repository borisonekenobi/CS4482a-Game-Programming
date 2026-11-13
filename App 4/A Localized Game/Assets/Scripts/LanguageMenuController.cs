using Borisonekenobi.Localization;
using UnityEngine;

public class LanguageMenuController : MonoBehaviour
{
    public void SetLanguage(string language)
    {
        Settings.Instance.Language = language;
    }
}
