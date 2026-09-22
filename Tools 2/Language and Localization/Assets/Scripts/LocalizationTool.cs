using System.Collections.Generic;
using System.Linq;
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
		var objectField = new ObjectField("Database File") { objectType = typeof(LocalizationDatabase) };
		rootVisualElement.Add(objectField);

		var tableContainer = new VisualElement { style = { flexGrow = 1, marginTop = 10 } };
		rootVisualElement.Add(tableContainer);

		objectField.RegisterValueChangedCallback(evt =>
		{
			_database = evt.newValue as LocalizationDatabase;
			tableContainer.Clear();
			if (_database != null) BuildTable(tableContainer);
		});

		rootVisualElement.Add(new IMGUIContainer(DrawBottomButtons));
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
		EditorGUI.BeginDisabledGroup(_database == null || _database.languages.Count == 0);
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
		if (_database == null) return;

		Undo.RecordObject(_database, "Add Localization Key Row");
		foreach (var language in _database.languages)
			language.translations.Add(new LocalizationRow { key = "NEW_KEY", value = string.Empty });

		EditorUtility.SetDirty(_database);
		_tableView.Rebuild();
	}

	private void RemoveSelectedKeyRows()
	{
		if (_database == null) return;

		var selectedIndices = _tableView.selectedIndices as List<int> ?? new List<int>();
		if (selectedIndices.Count == 0) return;

		Undo.RecordObject(_database, "Remove Localization Key Row");
		foreach (var language in _database.languages)
			for (var i = selectedIndices.Count - 1; i >= 0; i--)
			{
				var index = selectedIndices[i];
				if (index >= 0 && index < language.translations.Count)
					language.translations.RemoveAt(index);
			}

		EditorUtility.SetDirty(_database);
		_tableView.Rebuild();
	}

	private void BuildTable(VisualElement container)
	{
		var rowSource = _database.languages.Count > 0
			? _database.languages[0].translations
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
			makeCell = () => new TextField(),
			bindCell = (element, rowIndex) =>
			{
				if (element is not TextField textField) return;

				var rowData = _database.languages[0].translations[rowIndex];
				textField.value = rowData.key;

				EventCallback<ChangeEvent<string>> callback = evt =>
				{
					Undo.RecordObject(_database, "Modify Localization Key");
					foreach (var lang in _database.languages.Where(lang => rowIndex < lang.translations.Count))
						lang.translations[rowIndex].key = evt.newValue;
					EditorUtility.SetDirty(_database);
				};

				textField.userData = callback;
				textField.RegisterValueChangedCallback(callback);
			},
			unbindCell = (element, _) =>
			{
				if (element is TextField { userData: EventCallback<ChangeEvent<string>> cb } textField)
					textField.UnregisterValueChangedCallback(cb);
			}
		};
		_tableView.columns.Add(keyColumn);

		for (var i = 0; i < _database.languages.Count; i++)
		{
			var langIndex = i;
			var langColumn = new Column
			{
				title = _database.languages[langIndex].name,
				width = 200,
				makeCell = () => new TextField(),
				bindCell = (element, rowIndex) =>
				{
					if (element is not TextField textField) return;

					var languageData = _database.languages[langIndex];
					textField.value = languageData.translations[rowIndex].value;

					EventCallback<ChangeEvent<string>> callback = evt =>
					{
						Undo.RecordObject(_database, "Modify Localization Value");
						languageData.translations[rowIndex].value = evt.newValue;
						EditorUtility.SetDirty(_database);
					};

					textField.userData = callback;
					textField.RegisterValueChangedCallback(callback);
				},
				unbindCell = (element, _) =>
				{
					if (element is TextField { userData: EventCallback<ChangeEvent<string>> cb } textField)
						textField.UnregisterValueChangedCallback(cb);
				}
			};
			_tableView.columns.Add(langColumn);
		}

		container.Add(_tableView);
	}

	private void Save()
	{
		if (_database == null) return;

		EditorUtility.SetDirty(_database);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		Settings.Instance.Languages =
			(from lang in _database.languages where !string.IsNullOrEmpty(lang.name) select lang.name).ToArray();
		DynamicLanguageGenerator.RegenerateMenu(Settings.Instance.Languages);

		Debug.Log("Changes saved successfully!");
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
