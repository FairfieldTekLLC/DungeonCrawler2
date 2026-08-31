using System;
using System.Collections.Generic;
using Aetherfall.Domain.Quests;
using Xunit;

namespace Aetherfall.Domain.Tests.Quests
{
    public class QuestSystemServiceTests
    {
        #region AcceptQuest Tests

        [Fact]
        public void AcceptQuest_ShouldReturnTrue_WhenPrerequisitesMet()
        {
            // Arrange
            var questId = "quest_1";
            var playerLevel = 10;
            var completedQuests = new List<string> { "prev_quest" };

            // Act
            bool result = QuestSystemService.AcceptQuest(questId, playerLevel, completedQuests);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AcceptQuest_ShouldReturnFalse_WhenLevelInsufficient()
        {
            // Arrange
            var questId = "quest_1";
            var playerLevel = 5; // Below required level (mocked as 10)
            var completedQuests = new List<string>();

            // Act
            bool result = QuestSystemService.AcceptQuest(questId, playerLevel, completedQuests);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AcceptQuest_ShouldReturnFalse_WhenPrerequisiteQuestMissing()
        {
            // Arrange
            var questId = "quest_1";
            var playerLevel = 10;
            var completedQuests = new List<string>(); // Missing prerequisite

            // Act
            bool result = QuestSystemService.AcceptQuest(questId, playerLevel, completedQuests);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region UpdateObjectiveProgress Tests

        [Fact]
        public void UpdateObjectiveProgress_ShouldIncrementCorrectly()
        {
            // Arrange
            var objectives = new Dictionary<string, ObjectiveProgress>
            {
                { "obj_1", new ObjectiveProgress(0, 5) }
            };

            // Act
            QuestSystemService.UpdateObjectiveProgress(objectives, "obj_1", increment: 1);

            // Assert
            Assert.Equal(1, objectives["obj_1"].CurrentCount);
        }

        [Fact]
        public void UpdateObjectiveProgress_ShouldCapAtRequiredCount()
        {
            // Arrange
            var objectives = new Dictionary<string, ObjectiveProgress>
            {
                { "obj_1", new ObjectiveProgress(4, 5) }
            };

            // Act
            QuestSystemService.UpdateObjectiveProgress(objectives, "obj_1", increment: 2);

            // Assert
            Assert.Equal(5, objectives["obj_1"].CurrentCount); // Should cap at 5
        }

        [Fact]
        public void UpdateObjectiveProgress_ShouldThrowIfObjectiveNotFound()
        {
            // Arrange
            var objectives = new Dictionary<string, ObjectiveProgress>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => 
                QuestSystemService.UpdateObjectiveProgress(objectives, "missing_obj", 1));
        }

        #endregion

        #region IsQuestComplete Tests

        [Fact]
        public void IsQuestComplete_ShouldReturnTrue_WhenAllObjectivesMet()
        {
            // Arrange
            var objectives = new Dictionary<string, ObjectiveProgress>
            {
                { "obj_1", new ObjectiveProgress(5, 5) },
                { "obj_2", new ObjectiveProgress(10, 10) }
            };

            // Act
            bool result = QuestSystemService.IsQuestComplete(objectives);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsQuestComplete_ShouldReturnFalse_WhenObjectivesIncomplete()
        {
            // Arrange
            var objectives = new Dictionary<string, ObjectiveProgress>
            {
                { "obj_1", new ObjectiveProgress(4, 5) } // Incomplete
            };

            // Act
            bool result = QuestSystemService.IsQuestComplete(objectives);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region TransitionState Tests

        [Fact]
        public void TransitionState_ShouldTransitionFromAvailableToAccepted()
        {
            // Arrange
            var currentState = QuestState.Available;
            string trigger = "Accepted";

            // Act
            var result = QuestSystemService.TransitionState(currentState, trigger);

            // Assert
            Assert.Equal(QuestState.Accepted, result);
        }

        [Fact]
        public void TransitionState_ShouldTransitionFromAcceptedToCompleted()
        {
            // Arrange
            var currentState = QuestState.Accepted;
            string trigger = "Completed";

            // Act
            var result = QuestSystemService.TransitionState(currentState, trigger);

            // Assert
            Assert.Equal(QuestState.Completed, result);
        }

        [Fact]
        public void TransitionState_ShouldRemainCompletedIfTriggerMismatch()
        {
            // Arrange
            var currentState = QuestState.Completed;
            string trigger = "Accepted"; // Cannot go back to accepted

            // Act
            var result = QuestSystemService.TransitionState(currentState, trigger);

            // Assert
            Assert.Equal(QuestState.Completed, result);
        }

        #endregion

        #region CalculateCompletionBonus Tests

        [Fact]
        public void CalculateCompletionBonus_ShouldScaleWithGrindDifficulty()
        {
            // Arrange
            var objectives = new Dictionary<string, ObjectiveProgress>
            {
                { "obj_1", new ObjectiveProgress(0, 20) } // High grind count
            };
            double baseReward = 100.0;

            // Act
            double result = QuestSystemService.CalculateCompletionBonus(baseReward, objectives);

            // Assert
            // Expected: 100 * (1 + (20 - 5) * 0.02) = 100 * 1.3 = 130.0
            Assert.Equal(130.0, result, 2);
        }

        [Fact]
        public void CalculateCompletionBonus_ShouldReturnBaseWhenGrindLow()
        {
            // Arrange
            var objectives = new Dictionary<string, ObjectiveProgress>
            {
                { "obj_1", new ObjectiveProgress(0, 3) } // Low grind count (< 5)
            };
            double baseReward = 100.0;

            // Act
            double result = QuestSystemService.CalculateCompletionBonus(baseReward, objectives);

            // Assert
            Assert.Equal(100.0, result);
        }

        #endregion
    }
}
