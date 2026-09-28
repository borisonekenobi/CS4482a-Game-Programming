using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
	public static class NodeExtensions
	{
		public static string NextGuid(this Node node)
		{
			return node.outputContainer.Q<Port>()?.connections.FirstOrDefault()?.input.node.viewDataKey ?? "";
		}

		public static void AddChoiceOutputPort(this Node node, string choiceKey)
		{
			var choicePort =
				Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
			var choiceCount = node.outputContainer.childCount;
			choicePort.portName = $"Option {choiceCount}";

			var keyField = new TextField { value = choiceKey, style = { width = 100 } };
			choicePort.Add(keyField);

			var deleteButton = new Button(() => { node.RemoveChoiceOutputPort(choicePort); }) { text = "✕" };
			choicePort.Add(deleteButton);

			node.outputContainer.Add(choicePort);
			node.RefreshPorts();
		}

		private static void RemoveChoiceOutputPort(this Node node, Port port)
		{
			if (node.outputContainer.childCount == 1)
			{
				Debug.LogWarning("Dialogue node must have at least one choice");
				return;
			}

			node.outputContainer.Remove(port);
			node.RefreshPorts();
		}
	}
}
