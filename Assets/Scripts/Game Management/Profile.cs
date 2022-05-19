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

    public void IncrementLevelsCompleted()
    {
        LevelsCompleted++;
    }

    public void AddAchievement(Achievement achievement)
    {
        Achievements.Add(achievement);
    }
}
