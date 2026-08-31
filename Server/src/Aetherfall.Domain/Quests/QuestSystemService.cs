using System;
using System.Collections.Generic;
using System.Linq;

namespace Aetherfall.Domain.Quests
{
    public enum QuestState
    {
        Available,
        Accepted,
        Completed,
        Failed
    }

    public enum ObjectiveType
    {
        Kill,
        Collect,
        Explore,
        Deliver
    }

    public record ObjectiveProgress(int CurrentCount, int RequiredCount);

    public static class QuestSystemService
    {
        public static bool AcceptQuest(string questId, int playerLevel, List<string> completedQuestIds)
        {
            var requiredLevel = GetRequiredLevel(questId);
            var requiredPrevQuests = GetRequiredPreviousQuests(questId);

            if (playerLevel < requiredLevel) return false;

            foreach (var prevQuest in requiredPrevQuests)
            {
                if (!completedQuestIds.Contains(prevQuest)) return false;
            }

            return true;
        }

        public static Dictionary<string, ObjectiveProgress> UpdateObjectiveProgress(
            Dictionary<string, ObjectiveProgress> objectives,
            string objectiveId,
            int increment = 1)
        {
            if (!objectives.ContainsKey(objectiveId))
                throw new ArgumentException($"Objective '{objectiveId}' not found in quest.");

            var current = objectives[objectiveId];
            var newCount = Math.Max(0, Math.Min(current.CurrentCount + increment, current.RequiredCount));
            objectives[objectiveId] = new ObjectiveProgress(newCount, current.RequiredCount);
            return objectives;
        }

        public static bool IsQuestComplete(Dictionary<string, ObjectiveProgress> objectives)
        {
            return objectives.Values.All(obj => obj.CurrentCount >= obj.RequiredCount);
        }

        public static QuestState TransitionState(QuestState currentState, string triggerEvent)
        {
            switch (currentState)
            {
                case QuestState.Available:
                    return triggerEvent == "Accepted" ? QuestState.Accepted : currentState;
                case QuestState.Accepted:
                    if (triggerEvent == "Completed") return QuestState.Completed;
                    break;
                case QuestState.Completed:
                    return currentState;
                default:
                    return currentState;
            }
            return currentState;
        }

        public static double CalculateCompletionBonus(double baseReward, Dictionary<string, ObjectiveProgress> objectives)
        {
            double multiplier = 1.0;
            foreach (var obj in objectives.Values)
            {
                if (obj.RequiredCount > 5) multiplier += (obj.RequiredCount - 5) * 0.02;
            }
            return baseReward * Math.Max(1.0, multiplier);
        }

        private static int GetRequiredLevel(string questId) => 10;
        private static List<string> GetRequiredPreviousQuests(string questId) => new List<string>();
    }
}
