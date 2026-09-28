using UnityEngine;

namespace Editor
{
	public static class LanguageMenu
	{
		public static void HandleSelection(string itemName)
		{
			Settings.Instance.Language = itemName;
			Debug.Log($"Switching to {Settings.Instance.Language}");
		}
	}
}
