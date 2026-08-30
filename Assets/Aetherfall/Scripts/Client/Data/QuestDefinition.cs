using UnityEngine;

namespace Aetherfall.Client.Data;

[System.Serializable]
public struct QuestObjectiveDefinition
{
    public string objectiveId;
    public string objectiveType;
    public int requiredAmount;
}

[CreateAssetMenu(menuName = "Aetherfall/Quests/Quest")]
public sealed class QuestDefinition : AetherfallDefinition
{
    [SerializeField] private QuestObjectiveDefinition[] objectives = new QuestObjectiveDefinition[0];
    public QuestObjectiveDefinition[] Objectives => objectives;
}
