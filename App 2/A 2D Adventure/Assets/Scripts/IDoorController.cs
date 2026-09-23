using TMPro;
using UnityEngine;

public abstract class DoorController : MonoBehaviour
{
	public int CollectiblesToOpen { get; set;}
	[SerializeField] protected TMP_Text errorText;
	protected const string LockedMessage = "Door is locked! Collect all items to open.";
}
