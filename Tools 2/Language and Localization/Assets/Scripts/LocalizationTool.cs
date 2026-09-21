using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class LocalizationTool : EditorWindow
{
	private static LocalizationTool _window;

	private string[] _languages;
	private string _language;

	private LocalizationData _database;
	private MultiColumnListView _tableView;

	private void OnEnable()
	{
		_languages = Settings.Instance.Languages;
		_language = Settings.Instance.Language;

		Debug.Log("Starting with language: " + _language);
	}

	private void CreateGUI()
	{
		var objectField = new ObjectField("Database File")
		{
			objectType = typeof(LocalizationData)
		};
		rootVisualElement.Add(objectField);

		var tableContainer = new VisualElement { style = { flexGrow = 1, marginTop = 10 } };
		rootVisualElement.Add(tableContainer);
		objectField.RegisterValueChangedCallback(evt =>
		{
			_database = evt.newValue as LocalizationData;
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
		if (GUILayout.Button("Add Key Row", GUILayout.ExpandWidth(false))) AddKeyRow();
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Save", GUILayout.ExpandWidth(false))) Save();
		if (GUILayout.Button("Save and Close", GUILayout.ExpandWidth(false))) SaveAndClose();
		if (GUILayout.Button("Cancel", GUILayout.ExpandWidth(false))) Cancel();
		EditorGUILayout.EndHorizontal();
	}

	private void AddKeyRow()
	{
		_database.rows.Add(new LocalizationRow
			{ key = "NEW_KEY", translations = new List<string>(new string[_database.languages.Count]) });
		EditorUtility.SetDirty(_database);
		_tableView.Rebuild();
	}

	private void BuildTable(VisualElement container)
	{
		_tableView = new MultiColumnListView
		{
			itemsSource = _database.rows,
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

				var rowData = _database.rows[rowIndex];
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
			var langName = _database.languages[langIndex];

			var langColumn = new Column
			{
				title = langName,
				width = 200,
				bindCell = (element, rowIndex) =>
				{
					if (element is not TextField textField) return;

					var rowData = _database.rows[rowIndex];
					while (rowData.translations.Count <= langIndex) rowData.translations.Add("");

					textField.value = rowData.translations[langIndex];
					textField.RegisterValueChangedCallback(evt =>
					{
						rowData.translations[langIndex] = evt.newValue;
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
		Debug.Log("Number of Languages: " + _tableView.columns.Count);
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
