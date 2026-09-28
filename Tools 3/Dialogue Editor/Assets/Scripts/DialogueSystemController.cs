using UnityEngine;
using UnityEngine.UI;

public class DialogueSystemController : MonoBehaviour
{
	[SerializeField] private DialogueGraphAsset dialogueGraphAsset;
	[SerializeField] private Canvas canvas;
	[SerializeField] private Font font;

	private int _numChoices;

	private void Start()
	{
		GenerateDialogueUI(dialogueGraphAsset.startNode.targetNodeGuid);
	}

	private void GenerateDialogueUI(string nodeGuid)
	{
		_numChoices = 0;
		for (var i = 0; i < canvas.transform.childCount; i++)
		{
			var child = canvas.transform.GetChild(i);
			Destroy(child.gameObject);
		}

		if (dialogueGraphAsset.endNode.guid == nodeGuid) return;

		var node = dialogueGraphAsset.nodes.Find(x => x.guid == nodeGuid);
		var dialogueText = new GameObject("DialogueText");
		dialogueText.transform.SetParent(canvas.transform);
		var dialogueTextText = dialogueText.AddComponent<Text>();
		dialogueTextText.text = Localization.Get(node.key);
		dialogueTextText.font = font;
		dialogueTextText.color = Color.black;
		var dialogueTextRectTransform = dialogueText.GetComponent<RectTransform>();
		dialogueTextRectTransform.anchoredPosition = new Vector2(0, 0);
		dialogueTextRectTransform.sizeDelta = new Vector2(200, 50);

		foreach (var choice in node.choices)
		{
			var button = new GameObject("ChoiceButton");
			button.transform.SetParent(canvas.transform);
			var buttonRectTransform = button.AddComponent<RectTransform>();
			buttonRectTransform.anchoredPosition = new Vector2(0, -55 * (_numChoices + 1));
			buttonRectTransform.sizeDelta = new Vector2(200, 50);
			var buttonText = button.AddComponent<Text>();
			buttonText.text = Localization.Get(choice.key);
			buttonText.font = font;
			buttonText.color = Color.black;
			var buttonButton = button.AddComponent<Button>();
			buttonButton.onClick.AddListener(() => GenerateDialogueUI(choice.targetNodeGuid));
			_numChoices++;
		}
	}
}
