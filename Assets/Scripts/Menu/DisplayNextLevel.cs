using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayNextLevel : MonoBehaviour
{
    private const string nextLevelTextFormat = "Level {0}";

    private Text levelText;

    public void UpdateNextLevelText()
    {
        int nextLevel = Math.Min(GameManager.SelectedProfile.LevelsCompleted + 1, GameManager.NumLevels);
        levelText.text = string.Format(nextLevelTextFormat, nextLevel);
    }

    private void Awake()
    {
        levelText = GetComponent<Text>();
    }

    private void Start()
    {
        UpdateNextLevelText();
    }
}
