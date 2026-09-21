using UnityEngine;

public static class LanguageMenu
{
	public static void HandleSelection(string itemName)
	{
		Settings.Instance.Language = itemName;
		Debug.Log(Settings.Instance.Language);
	}
}
