using UnityEngine;

public static class Localization
{
	private static readonly LocalizationDatabase Database = Resources.Load<LocalizationDatabase>("LocalizationDatabase");

	public static string Get(string key)
	{
		if (Database == null) return key;

		var language = Settings.Instance.Language;
		var langData = Database.languages.Find(l => l.name == language);
		if (langData == null) return key;

		var row = langData.translations.Find(r => r.key == key);
		return row?.value ?? key;
	}
}
