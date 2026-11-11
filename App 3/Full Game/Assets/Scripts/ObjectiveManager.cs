using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour, IJournalPage
{
    [Header("References")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private ObjectiveDefinition[] allObjectives;

    private readonly HashSet<ObjectiveDefinition> _completedObjectives = new();
    private readonly HashSet<ObjectiveDefinition> _unlockedObjectives = new();

    private void Start()
    {
        foreach (var objective in allObjectives)
            if (objective.unlockedAtStart)
                _unlockedObjectives.Add(objective);

        EvaluateObjectives();
    }

    private void OnEnable()
    {
        if (inventory != null) inventory.ItemAdded += OnItemAdded;
    }

    private void OnDisable()
    {
        if (inventory != null) inventory.ItemAdded -= OnItemAdded;
    }

    public string Title => "Objectives";

    public string Summary
    {
        get
        {
            var summary = new StringBuilder();
            foreach (var objective in _unlockedObjectives)
            {
                summary.AppendLine($"{objective.title}: {GetProgress(objective)}/{objective.requiredAmount}");
                summary.AppendLine();
            }

            return summary.ToString();
        }
    }

    private void OnItemAdded(Item item, int newCount)
    {
        EvaluateObjectives();
    }

    private void EvaluateObjectives()
    {
        bool changed;

        do
        {
            changed = false;

            foreach (var objective in allObjectives)
            {
                if (_unlockedObjectives.Contains(objective)) continue;
                if (!CanUnlock(objective)) continue;

                _unlockedObjectives.Add(objective);
                changed = true;
            }

            foreach (var objective in _unlockedObjectives.Where(objective => !_completedObjectives.Contains(objective))
                         .Where(IsComplete))
            {
                _completedObjectives.Add(objective);
                changed = true;
            }
        } while (changed);
    }

    private bool CanUnlock(ObjectiveDefinition objective)
    {
        if (objective.unlockOnAnyCollection && !HasCollectedAnything()) return false;
        if (objective.unlockOnFirstCollectionOf is not Item.None &&
            inventory.GetItemCount(objective.unlockOnFirstCollectionOf) == 0) return false;

        return objective.prerequisites == null ||
               objective.prerequisites.All(prerequisite =>
                   prerequisite != null && _completedObjectives.Contains(prerequisite));
    }

    private bool HasCollectedAnything()
    {
        return inventory.GetTotalItemCount() > 0;
    }

    private bool IsComplete(ObjectiveDefinition objective)
    {
        if (objective.completedByAnyCollection && HasCollectedAnything()) return true;
        if (objective.requiredItem is Item.None) return false;

        return inventory.GetItemCount(objective.requiredItem) >= objective.requiredAmount;
    }

    private int GetProgress(ObjectiveDefinition objective)
    {
        if (objective.completedByAnyCollection) return HasCollectedAnything() ? objective.requiredAmount : 0;
        return objective.requiredItem is Item.None ? 0 : inventory.GetItemCount(objective.requiredItem);
    }
}
