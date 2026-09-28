using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueGraph", menuName = "Dialogue/Graph Asset")]
public class DialogueGraphAsset : ScriptableObject
{
	public StartNode startNode;
	public List<ChoiceNode> nodes = new();
	public EndNode endNode;
}
