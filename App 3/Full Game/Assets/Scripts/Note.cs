using UnityEngine;

public class Note : MonoBehaviour, ICollectible
{
    [SerializeField] public string noteTitle;
    [SerializeField] [TextArea] public string noteContent;
    public string Name => "Note";
}
