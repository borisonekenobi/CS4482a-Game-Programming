namespace Borisonekenobi.Localization.Editor
{
    public static class LanguageMenu
    {
        public static void HandleSelection(string itemName)
        {
            Settings.Instance.Language = itemName;
        }
    }
}
