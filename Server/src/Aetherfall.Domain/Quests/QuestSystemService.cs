using System;
using System.Collections.Generic;
using System.Linq;

namespace Aetherfall.Domain.Quests
{
    /// <summary>
    /// Represents the lifecycle states of a quest.
    /// </summary>
    public enum QuestState
    {
        Available,
        Accepted,
        Completed,
        Failed
    }

    /// <summary>
    /// Defines the types of objectives supported by the quest system.
    /// </summary>
    public enum ObjectiveType
    {
        Kill,
        Collect,
        Explore,
        Deliver
    }

    /// <summary>
    /// Tracks progress for a specific objective within a quest.
    /// </summary>
    public record ObjectiveProgress(int CurrentCount, int RequiredCount);

    /// <summary>
    /// Handles all quest logic including acceptance, objective tracking, state transitions, and completion validation.
    /// Implements the exact mechanics from the 10-quest-system-design document.
    /// </summary>
    public static class QuestSystemService
    {
        /// <summary>
        /// Accepts a quest after validating prerequisites (level, previous quests).
        /// Returns true if accepted, false if prerequisites fail.
        /// </summary>
        public static bool AcceptQuest(string questId, int playerLevel, List<string> completedQuestIds)
        {
            // Example prerequisite validation logic
            var requiredLevel = GetRequiredLevel(questId);
            var requiredPrevQuests = GetRequiredPreviousQuests(questId);

            if (playerLevel < requiredLevel) return false;

            foreach (var prevQuest in requiredPrevQuests)
            {
                if (!completedQuestIds.Contains(prevQuest)) return false;
            }

            // Transition state logic handled by aggregate or service layer
            return true;
        }

        /// <summary>
        /// Updates objective progress for a quest. Validates increments and caps values.
        /// </summary>
        public static Dictionary<string, ObjectiveProgress> UpdateObjectiveProgress(
            Dictionary<string, ObjectiveProgress> objectives,
            string objectiveId,
            int increment = 1)
        {
            if (!objectives.ContainsKey(objectiveId))
                throw new ArgumentException($"Objective '{objectiveId}' not found in quest.");

            var current = objectives[objectiveId];

            // Prevent negative progress and cap at required amount
            var newCount = Math.Max(0, Math.Min(current.CurrentCount + increment, current.RequiredCount));

            objectives[objectiveId] = new ObjectiveProgress(newCount, current.RequiredCount);
            return objectives;
        }

        /// <summary>
        /// Checks if all objectives for a quest are fully completed.
        /// </summary>
        public static bool IsQuestComplete(Dictionary<string, ObjectiveProgress> objectives)
        {
            return objectives.Values.All(obj => obj.CurrentCount >= obj.RequiredCount);
        }

        /// <summary>
        /// Validates state transitions based on triggers and current state.
        /// Ensures deterministic progression through quest lifecycle.
        /// </summary>
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
        }

        /// <summary>
        /// Calculates quest completion bonus multiplier based on objective types and difficulty scaling.
        /// </summary>
        public static double CalculateCompletionBonus(double baseReward, Dictionary<string, ObjectiveProgress> objectives)
        {
            double multiplier = 1.0;
            foreach (var obj in objectives.Values)
            {
                // Bonus scales slightly with higher required counts to reward grinding
                if (obj.RequiredCount > 5) multiplier += (obj.RequiredCount - 5) * 0.02;
            }
            return baseReward * Math.Max(1.0, multiplier);
        }

        #region Mock Data Retrieval (In production, these query a repository or config)

        private static int GetRequiredLevel(string questId) => 10; // Placeholder

        private static List<string> GetRequiredPreviousQuests(string questId) => new List<string>(); // Placeholder

        #endregion
    }
}
