using System;
using System.Collections.Generic;

[Serializable]
public class Profile
{
    public int ProfileID { get; private set; }
    public int LevelsCompleted { get; private set; }
    public List<Achievement> Achievements { get; private set; }

    public Profile(int profileID)
    {
        ProfileID = profileID;
        LevelsCompleted = 0;
        Achievements = new List<Achievement>();
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

    public void AddAchievement(Achievement achievement)
    {
        Achievements.Add(achievement);
        SaveManager.SaveProfile(this);
    }
}
