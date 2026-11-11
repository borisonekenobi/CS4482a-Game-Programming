using UnityEngine;

[CreateAssetMenu(fileName = "New Objective", menuName = "Objectives/Objective")]
public class ObjectiveDefinition : ScriptableObject
{
    [Header("Information")]
    public string id;
    public string title;
    public int points;

    [Header("Unlock Requirements")]
    public bool unlockedAtStart;
    public bool unlockOnAnyCollection;
    public Item unlockOnFirstCollectionOf;
    public ObjectiveDefinition[] prerequisites;

    [Header("Completion Requirements")]
    public bool completedByAnyCollection;
    public Item requiredItem;
    public int requiredAmount;
}
