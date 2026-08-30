using System.Collections.Generic;
using UnityEngine;

namespace Aetherfall.Client.Controllers;

public sealed class QuestLogController : MonoBehaviour
{
    private readonly List<string> _questEntries = new();

    public IReadOnlyCollection<string> QuestEntries => _questEntries;

    public void AddQuest(string questName)
    {
        if (!string.IsNullOrWhiteSpace(questName))
        {
            _questEntries.Add(questName);
        }
    }
}
