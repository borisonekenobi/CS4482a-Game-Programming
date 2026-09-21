using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationDatabase", menuName = "Localization/Database")]
public class LocalizationData : ScriptableObject
{
	// List of active languages (Columns)
	public List<string> languages = new List<string> { "English", "Spanish", "French" };

	// List of translation entries (Rows)
	public List<LocalizationRow> rows = new List<LocalizationRow>();
}

[System.Serializable]
public class LocalizationRow
{
	public string key;
	// Index matches the order of the 'languages' list above
	public List<string> translations = new List<string>();
}
