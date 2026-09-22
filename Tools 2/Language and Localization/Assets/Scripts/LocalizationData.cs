using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationDatabase", menuName = "Localization/Database")]
public class LocalizationDatabase : ScriptableObject
{
	public List<LocalizationLanguage> languages = new();

	public void AddLanguage()
	{
		var keys = languages.FirstOrDefault()?.translations.Select(r => r.key) ?? Enumerable.Empty<string>();
		languages.Add(new LocalizationLanguage());
		foreach (var key in keys)
			languages[^1].translations.Add(new LocalizationRow { key = key, value = string.Empty });
	}
}

[CustomEditor(typeof(LocalizationDatabase))]
public class LocalizationDataEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		var localizationData = (LocalizationDatabase)target;
		if (!GUILayout.Button("Add Language")) return;
		localizationData.AddLanguage();
		EditorUtility.SetDirty(localizationData);
	}
}

[Serializable]
public class LocalizationLanguage
{
	public string name;
	public List<LocalizationRow> translations = new();
}

[Serializable]
public class LocalizationRow
{
	public string key;
	public string value;
}
