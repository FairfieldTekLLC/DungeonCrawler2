using Aetherfall.Domain.Abstractions;
using Aetherfall.Domain.Common;

namespace Aetherfall.Domain.Quests;

public sealed record QuestObjective
{
    public QuestObjective(string objectiveId, ObjectiveType objectiveType, int requiredAmount)
    {
        if (string.IsNullOrWhiteSpace(objectiveId)) throw new ArgumentException("Objective id required.", nameof(objectiveId));
        if (requiredAmount <= 0) throw new ArgumentOutOfRangeException(nameof(requiredAmount));

        ObjectiveId = objectiveId;
        ObjectiveType = objectiveType;
        RequiredAmount = requiredAmount;
    }

    public string ObjectiveId { get; }
    public ObjectiveType ObjectiveType { get; }
    public int RequiredAmount { get; }
}

public sealed class QuestProgress
{
    public QuestProgress(QuestObjective objective)
    {
        Objective = objective;
    }

    public QuestObjective Objective { get; }
    public int CurrentAmount { get; private set; }
    public bool Completed => CurrentAmount >= Objective.RequiredAmount;

    public void Advance(int amount)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        CurrentAmount = Math.Min(Objective.RequiredAmount, CurrentAmount + amount);
    }
}

public sealed class QuestAggregate : Entity
{
    private readonly List<QuestProgress> _progressEntries;

    public QuestAggregate(Guid id, string definitionId, IEnumerable<QuestObjective> objectives) : base(id)
    {
        DefinitionId = Guard.AgainstNullOrWhiteSpace(definitionId, nameof(definitionId));
        _progressEntries = objectives.Select(x => new QuestProgress(x)).ToList();
        if (_progressEntries.Count == 0) throw new ArgumentException("Quest requires objectives.", nameof(objectives));
        Status = QuestStatus.NotStarted;
    }

    public string DefinitionId { get; }
    public QuestStatus Status { get; private set; }
    public IReadOnlyCollection<QuestProgress> ProgressEntries => _progressEntries.AsReadOnly();

    public void Start()
    {
        if (Status != QuestStatus.NotStarted) throw new InvalidOperationException("Quest already started.");
        Status = QuestStatus.InProgress;
    }

    public void AdvanceObjective(string objectiveId, int amount)
    {
        if (Status != QuestStatus.InProgress) throw new InvalidOperationException("Quest is not active.");
        var objective = _progressEntries.FirstOrDefault(x => x.Objective.ObjectiveId == objectiveId) ?? throw new InvalidOperationException("Objective not found.");
        objective.Advance(amount);
        if (_progressEntries.All(x => x.Completed))
        {
            Status = QuestStatus.Completed;
        }
    }
}
