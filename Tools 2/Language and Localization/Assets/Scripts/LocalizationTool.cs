using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class LocalizationTool : EditorWindow
{
	private static LocalizationTool _window;

	private LocalizationDatabase _database;
	private MultiColumnListView _tableView;

	private void CreateGUI()
	{
		var objectField = new ObjectField("Database File")
		{
			objectType = typeof(LocalizationDatabase)
		};
		rootVisualElement.Add(objectField);

		var tableContainer = new VisualElement { style = { flexGrow = 1, marginTop = 10 } };
		rootVisualElement.Add(tableContainer);
		objectField.RegisterValueChangedCallback(evt =>
		{
			_database = evt.newValue as LocalizationDatabase;
			tableContainer.Clear();
			if (_database != null) BuildTable(tableContainer);
		});

		var bottomButtonsContainer = new IMGUIContainer(DrawBottomButtons);
		rootVisualElement.Add(bottomButtonsContainer);
	}

	[MenuItem("Window/Localization...")]
	public static void OpenWindow()
	{
		_window = GetWindow<LocalizationTool>("Translations");
		_window.Show();
	}

	private void DrawBottomButtons()
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUI.BeginDisabledGroup(_database == null);
		if (GUILayout.Button("Add Key Row", GUILayout.ExpandWidth(false))) AddKeyRow();
		if (GUILayout.Button("Remove Selected Key Row", GUILayout.ExpandWidth(false))) RemoveSelectedKeyRows();
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Save", GUILayout.ExpandWidth(false))) Save();
		if (GUILayout.Button("Save and Close", GUILayout.ExpandWidth(false))) SaveAndClose();
		EditorGUI.EndDisabledGroup();
		if (GUILayout.Button("Cancel", GUILayout.ExpandWidth(false))) Cancel();
		EditorGUILayout.EndHorizontal();
	}

	private void AddKeyRow()
	{
		foreach (var language in _database.languages)
			language.Translations.Add(new LocalizationRow { key = "NEW_KEY", value = string.Empty });

		EditorUtility.SetDirty(_database);
		_tableView.Rebuild();
	}

	private void RemoveSelectedKeyRows()
	{
		var selectedIndices = _tableView.selectedIndices as List<int> ?? new List<int>();
		if (selectedIndices.Count == 0) return;
		
		foreach (var language in _database.languages)
		{
			for (var i = selectedIndices.Count - 1; i >= 0; i--)
			{
				var index = selectedIndices[i];
				if (index >= 0 && index < language.Translations.Count)
					language.Translations.RemoveAt(index);
			}
		}
		
		EditorUtility.SetDirty(_database);
		_tableView.Rebuild();
	}

	private void BuildTable(VisualElement container)
	{
		var rowSource = _database.languages.Count > 0
			? _database.languages[0].Translations
			: new List<LocalizationRow>();

		_tableView = new MultiColumnListView
		{
			itemsSource = rowSource,
			showAlternatingRowBackgrounds = AlternatingRowBackground.All,
			virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
			showBorder = true,
			style = { flexGrow = 1 }
		};

		var keyColumn = new Column
		{
			title = "Localization Key",
			width = 150,
			bindCell = (element, rowIndex) =>
			{
				if (element is not TextField textField) return;

				var rowData = _database.languages[0].Translations[rowIndex];
				textField.value = rowData.key;
				textField.RegisterValueChangedCallback(evt =>
				{
					rowData.key = evt.newValue;
					EditorUtility.SetDirty(_database);
				});
			},
			makeCell = () => new TextField()
		};
		_tableView.columns.Add(keyColumn);

		for (var i = 0; i < _database.languages.Count; i++)
		{
			var langIndex = i;
			var langName = _database.languages[langIndex].name;

			var langColumn = new Column
			{
				title = langName,
				width = 200,
				bindCell = (element, rowIndex) =>
				{
					if (element is not TextField textField) return;

					var languageData = _database.languages[langIndex];
					while (languageData.Translations.Count <= rowIndex)
						languageData.Translations.Add(new LocalizationRow { key = string.Empty, value = string.Empty });

					textField.value = languageData.Translations[rowIndex].value;
					textField.RegisterValueChangedCallback(evt =>
					{
						languageData.Translations[rowIndex].value = evt.newValue;
						EditorUtility.SetDirty(_database);
					});
				},
				makeCell = () => new TextField()
			};

			_tableView.columns.Add(langColumn);
		}

		container.Add(_tableView);
	}

	private void Save()
	{
		Debug.Log("Number of Languages: " + (_tableView.columns.Count - 1));
		Debug.Log("Number of Keys: " + _tableView.itemsSource.Count);

		// TODO: Save the data to the LocalizationData asset

		Settings.Instance.Languages = new[]
		{
			"Bulgarian",
			"Chinese (Simplified)",
			"Chinese (Traditional)",
			"English",
			"French",
			"German",
			"Japanese",
			"Korean",
			"Polish",
			"Russian"
		};
		DynamicLanguageGenerator.RegenerateMenu(Settings.Instance.Languages);
	}

	private void SaveAndClose()
	{
		Save();
		if (_window != null) _window.Close();
	}

	private static void Cancel()
	{
		if (_window != null) _window.Close();
	}
}
