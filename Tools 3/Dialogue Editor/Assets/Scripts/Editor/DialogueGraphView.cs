using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
	public class DialogueGraphView : GraphView
	{
		public DialogueGraphView()
		{
			SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
			this.AddManipulator(new ContentDragger());
			this.AddManipulator(new SelectionDragger());
			this.AddManipulator(new RectangleSelector());

			var grid = new GridBackground();
			Insert(0, grid);
			grid.StretchToParentSize();
		}

		public void GenerateDefaultNodes()
		{
			CreateStartNode(new Vector2(100, 200), Guid.NewGuid().ToString());
			CreateEndNode(new Vector2(600, 200), Guid.NewGuid().ToString());
		}

		public Node CreateStartNode(Vector2 pos, string guid)
		{
			var node = new Node { title = "Start Node", viewDataKey = guid };
			node.SetPosition(new Rect(pos, new Vector2(150, 100)));

			var outputPort =
				Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
			outputPort.portName = "Next";
			node.outputContainer.Add(outputPort);

			node.RefreshPorts();
			node.RefreshExpandedState();
			AddElement(node);
			return node;
		}

		public Node CreateEndNode(Vector2 pos, string guid)
		{
			var node = new Node { title = "End Node", viewDataKey = guid };
			node.SetPosition(new Rect(pos, new Vector2(150, 100)));

			var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi,
				typeof(float));
			inputPort.portName = "Exit Endpoint";
			node.inputContainer.Add(inputPort);

			node.RefreshPorts();
			node.RefreshExpandedState();
			AddElement(node);
			return node;
		}

		public Node CreateStandardNode(Vector2 pos, string guid, string speakerKey = "")
		{
			var node = new Node { viewDataKey = guid };
			node.SetPosition(new Rect(pos, new Vector2(220, 150)));

			var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi,
				typeof(float));
			inputPort.portName = "Entry";
			node.inputContainer.Add(inputPort);

			var keyField = new TextField("Speaker Localization Key") { value = speakerKey };
			node.extensionContainer.Add(keyField);

			var addChoiceButton = new Button(() => { node.AddChoiceOutputPort("NEW_CHOICE_KEY"); })
				{ text = "+ Add Choice" };
			node.titleContainer.Add(addChoiceButton);

			node.RefreshPorts();
			node.RefreshExpandedState();
			AddElement(node);
			return node;
		}

		public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
		{
			var compatiblePorts = new List<Port>();
			ports.ForEach(port =>
			{
				if (startPort != port && startPort.node != port.node && startPort.direction != port.direction)
					compatiblePorts.Add(port);
			});
			return compatiblePorts;
		}
	}
}
