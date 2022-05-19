using System;
using System.Collections.Generic;

[Serializable]
public class Profile
{
    public int ProfileID { get; private set; }
    public int CurrentLevel { get; private set; }
    public List<Achievement> Achievements { get; private set; }

    public Profile(int profileID)
    {
        ProfileID = profileID;
        CurrentLevel = 1;
        Achievements = new List<Achievement>();
    }

    public void IncrementCurrentLevel()
    {
        CurrentLevel++;
    }

    public void AddAchievement(Achievement achievement)
    {
        Achievements.Add(achievement);
    }
}
