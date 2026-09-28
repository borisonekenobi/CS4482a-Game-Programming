using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
	public class DialogueEditorWindow : EditorWindow
	{
		private ObjectField _assetPickerField;
		private DialogueGraphAsset _currentAsset;
		private DialogueGraphView _graphView;

		private void CreateGUI()
		{
			_graphView = new DialogueGraphView();
			_graphView.StretchToParentSize();
			rootVisualElement.Add(_graphView);

			var toolbar = new VisualElement
			{
				style =
				{
					flexDirection = FlexDirection.Row, backgroundColor = new StyleColor(new Color(0.2f, 0.2f, 0.2f))
				}
			};

			_assetPickerField = new ObjectField("Graph Asset") { objectType = typeof(DialogueGraphAsset) };
			_assetPickerField.RegisterValueChangedCallback(evt =>
			{
				_currentAsset = evt.newValue as DialogueGraphAsset;
				LoadGraphFromAsset();
			});
			toolbar.Add(_assetPickerField);

			var createNodeBtn = new Button(() =>
			{
				var node = _graphView.CreateStandardNode(new Vector2(250, 150), Guid.NewGuid().ToString());
				node.AddChoiceOutputPort("NEW_CHOICE_KEY");
			}) { text = "Create Dialogue Node" };
			toolbar.Add(createNodeBtn);

			var saveBtn = new Button(SaveGraphToAsset) { text = "Save Graph Data" };
			toolbar.Add(saveBtn);

			rootVisualElement.Add(toolbar);

			rootVisualElement.RegisterCallback<KeyDownEvent>(evt =>
			{
				if ((!evt.ctrlKey && !evt.commandKey) || evt.keyCode != KeyCode.S) return;
				SaveGraphToAsset();
				evt.StopPropagation();
			});

			if (_currentAsset != null && _assetPickerField != null)
				_assetPickerField.SetValueWithoutNotify(_currentAsset);
		}

		[MenuItem("Window/Dialogue Editor")]
		public static void OpenWindow()
		{
			GetWindow<DialogueEditorWindow>("Dialogue System");
		}

		[OnOpenAsset]
		public static bool OnOpenAssetHandler(EntityId entityId, int line)
		{
			if (EditorUtility.EntityIdToObject(entityId) is not DialogueGraphAsset asset) return false;

			var window = GetWindow<DialogueEditorWindow>("Dialogue System");
			window.Show();

			window.LoadTargetAssetDirectly(asset);
			return true;
		}

		private void LoadTargetAssetDirectly(DialogueGraphAsset asset)
		{
			_currentAsset = asset;

			if (_assetPickerField != null) _assetPickerField.value = asset;
			else LoadGraphFromAsset();
		}

		private void SaveGraphToAsset()
		{
			if (_currentAsset == null)
			{
				EditorUtility.DisplayDialog("Save Warning",
					"Please assign a valid Dialogue Graph Asset container file before saving changes.", "OK");
				return;
			}

			Undo.RecordObject(_currentAsset, "Save Dialogue Graph Changes");
			_currentAsset.startNode = null;
			_currentAsset.nodes.Clear();
			_currentAsset.endNode = null;

			var allVisualNodes = _graphView.nodes.ToList();
			foreach (var node in allVisualNodes)
				switch (node.title)
				{
					case "Start Node" when _currentAsset.startNode != null:
						Debug.LogWarning("Multiple start nodes found in the graph. Only the first one will be used.");
						continue;
					case "Start Node":
						_currentAsset.startNode = new StartNode
						{
							guid = node.viewDataKey,
							position = node.GetPosition().position,
							targetNodeGuid = node.NextGuid()
						};
						break;
					case "End Node" when _currentAsset.endNode != null:
						Debug.LogWarning("Multiple end nodes found in the graph. Only the first one will be used.");
						continue;
					case "End Node":
						_currentAsset.endNode = new EndNode
						{
							guid = node.viewDataKey,
							position = node.GetPosition().position
						};
						break;
					default:
					{
						var choiceNode = new ChoiceNode
						{
							guid = node.viewDataKey,
							position = node.GetPosition().position
						};

						var speakerField = node.extensionContainer.Q<TextField>();
						choiceNode.speakerKey = speakerField != null ? speakerField.value : "";

						var ports = node.outputContainer.Children().OfType<Port>().ToList();
						foreach (var port in ports)
						{
							var textInput = port.Q<TextField>();
							var choice = new DialogueChoiceData
							{
								choiceKey = textInput != null ? textInput.value : "CHOICE_KEY",
								targetNodeGuid = ""
							};

							if (port.connections.Any())
							{
								var edge = port.connections.First();
								choice.targetNodeGuid = edge.input.node.viewDataKey;
							}

							choiceNode.choices.Add(choice);
						}

						_currentAsset.nodes.Add(choiceNode);
						break;
					}
				}

			EditorUtility.SetDirty(_currentAsset);
			AssetDatabase.SaveAssets();
			Debug.Log("Dialogue graph changes saved successfully!");
		}

		private void LoadGraphFromAsset()
		{
			var elements = _graphView.graphElements.ToList();
			foreach (var element in elements) _graphView.RemoveElement(element);

			if (_currentAsset == null || _currentAsset.nodes.Count == 0)
			{
				_graphView.GenerateDefaultNodes();
				return;
			}

			var spawnMap = new Dictionary<string, Node>();

			Node startNode = null;
			if (_currentAsset.startNode != null)
			{
				startNode = _graphView.CreateStartNode(_currentAsset.startNode.position, _currentAsset.startNode.guid);
				spawnMap[_currentAsset.startNode.guid] = startNode;
			}

			if (_currentAsset.endNode != null)
			{
				var endNode = _graphView.CreateEndNode(_currentAsset.endNode.position, _currentAsset.endNode.guid);
				spawnMap[_currentAsset.endNode.guid] = endNode;
			}

			foreach (var nodeData in _currentAsset.nodes)
			{
				var visualNode = _graphView.CreateStandardNode(nodeData.position, nodeData.guid, nodeData.speakerKey);
				foreach (var choice in nodeData.choices)
					visualNode.AddChoiceOutputPort(choice.choiceKey);

				if (visualNode != null) spawnMap[nodeData.guid] = visualNode;
			}

			var firstNodeGuid = _currentAsset.startNode?.targetNodeGuid;
			if (startNode != null && !string.IsNullOrEmpty(firstNodeGuid) && spawnMap.ContainsKey(firstNodeGuid))
				LinkPorts(startNode.outputContainer.Q<Port>(), spawnMap[firstNodeGuid].inputContainer.Q<Port>());

			foreach (var nodeData in _currentAsset.nodes)
			{
				if (!spawnMap.TryGetValue(nodeData.guid, out var sourceVisualNode)) continue;

				var sourcePorts = sourceVisualNode.outputContainer.Children().OfType<Port>().ToList();
				for (var i = 0; i < nodeData.choices.Count; i++)
				{
					if (i >= sourcePorts.Count) break;
					var targetGuid = nodeData.choices[i].targetNodeGuid;
					if (string.IsNullOrEmpty(targetGuid) || !spawnMap.ContainsKey(targetGuid)) continue;

					LinkPorts(sourcePorts[i], spawnMap[targetGuid].inputContainer.Q<Port>());
				}
			}
		}

		private void LinkPorts(Port output, Port input)
		{
			if (output == null || input == null) return;

			var edge = output.ConnectTo(input);
			_graphView.AddElement(edge);
		}
	}
}
