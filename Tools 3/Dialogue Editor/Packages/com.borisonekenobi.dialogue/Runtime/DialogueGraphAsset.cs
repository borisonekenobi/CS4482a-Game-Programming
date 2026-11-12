using System.Collections.Generic;
using UnityEngine;

namespace Borisonekenobi.Dialogue
{
	[CreateAssetMenu(fileName = "NewDialogueGraph", menuName = "Dialogue/Graph Asset")]
	public class DialogueGraphAsset : ScriptableObject
	{
		public StartNode startNode;
		public List<ChoiceNode> nodes = new();
		public EndNode endNode;
	}
}
