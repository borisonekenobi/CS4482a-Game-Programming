using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationDatabase", menuName = "Localization/Database")]
public class LocalizationDatabase : ScriptableObject
{
	public List<LocalizationLanguage> languages = new();

	private void OnEnable()
	{
		if (languages != null && languages.Count != 0) return;
		languages = new List<LocalizationLanguage>
		{
			new() { name = "English" }
		};
            
		EditorUtility.SetDirty(this);
	}
}

[Serializable]
public class LocalizationLanguage
{
	public string name;
	[HideInInspector]
	public List<LocalizationRow> translations = new();
}

[Serializable]
public class LocalizationRow
{
	public string key;
	public string value;
}
