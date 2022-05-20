using System;
using System.Collections.Generic;

[Serializable]
public class Achievement
{
    public static Dictionary<int, Achievement> allAchievements = new Dictionary<int, Achievement>();
    private static string[] achivementTitles = {
        "Cross The River",
        "Complete First Level",
        "Eat a Fly",
        "Score 3000 Points",
        "Complete All Levels"
    };
    private static int idCounter = 0;

    static Achievement()
    {
        foreach (string achivementTitle in achivementTitles)
        {
            new Achievement(achivementTitle);
        }
    }

    public static Achievement GetAchievementByID(int achivementID)
    {
        allAchievements.TryGetValue(achivementID, out Achievement achievement);
        return achievement;
    }

    public int ID { get; private set; }
    public string Title { get; private set; }

    public Achievement(string title)
    {
        ID = idCounter++;
        Title = title;
        allAchievements.Add(ID, this);
    }
}
