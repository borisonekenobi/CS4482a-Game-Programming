using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class DialogueNode
{
	public string guid;
	public Vector2 position;
}

[Serializable]
public class StartNode : DialogueNode
{
	public string targetNodeGuid;
}

[Serializable]
public class ChoiceNode : DialogueNode
{
	public string key;
	public List<DialogueChoiceData> choices = new();
}

[Serializable]
public class DialogueChoiceData
{
	public string key;
	public string targetNodeGuid;
}

[Serializable]
public class EndNode : DialogueNode
{
}
