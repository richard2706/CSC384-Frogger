using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Profile
{
    public int ProfileID { get; private set; }
    public int LevelsCompleted { get; private set; }
    public Dictionary<Achievement, bool> Achievements { get; private set; }

    public Profile(int profileID)
    {
        ProfileID = profileID;
        LevelsCompleted = 0;

        Achievements = new Dictionary<Achievement, bool>();
        foreach (Achievement achievement in Achievement.allAchievements.Values)
        {
            Achievements.Add(achievement, false);
        }
    }

    public void UpdateLevelsCompleted(int level)
    {
        if (level > LevelsCompleted) LevelsCompleted = level;
        SaveManager.SaveProfile(this);
    }

    public int GetNextLevel()
    {
        return Math.Min(LevelsCompleted + 1, GameManager.NumLevels);
    }

    public void AddAchievement(int achievementID)
    {
        Achievement achievement = Achievement.GetAchievementByID(achievementID);
        if (achievement == null) return;

        Achievements.Remove(achievement);
        Achievements.Add(achievement, true);
        SaveManager.SaveProfile(this);
    }

    public bool HasAchievement(int achievementID)
    {
        Achievement achievement = Achievement.GetAchievementByID(achievementID);
        if (achievement == null) return false;

        bool success = Achievements.TryGetValue(achievement, out bool hasAchivement);
        return success && hasAchivement;
    }
}
