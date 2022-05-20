using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Text))]
public class DisplayAchievements : MonoBehaviour
{
    private const string achievementFormat = "{0}: {1}\n\n";
    private const string achivementLockedText = "Locked";
    private const string achivementUnlockedText = "Unlocked";

    private Text achievementsText;

    private void Awake()
    {
        achievementsText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        UpdateAchivements(GameManager.SelectedProfile);
        GameManager.OnProfileChanged += UpdateAchivements;
    }

    private void OnDisable()
    {
        GameManager.OnProfileChanged -= UpdateAchivements;
    }

    private void Start()
    {
        //UpdateAchivements(GameManager.SelectedProfile);
    }

    private void UpdateAchivements(Profile selectedProfile)
    {
        string achivementsString = "";
        foreach (KeyValuePair<Achievement, bool> achievementEntry in selectedProfile.Achievements)
        {
            string title = achievementEntry.Key.Title;
            string status = achievementEntry.Value ? achivementUnlockedText : achivementLockedText;
            achivementsString += String.Format(achievementFormat, title, status);
        }
        achievementsText.text = achivementsString;
    }
}
