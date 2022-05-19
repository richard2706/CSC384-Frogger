using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayNextLevel : MonoBehaviour
{
    private const string nextLevelTextFormat = "Level {0}";

    private Text levelText;

    public void UpdateNextLevelText(Profile selectedProfile)
    {
        int nextLevel = Math.Min(selectedProfile.LevelsCompleted + 1, GameManager.NumLevels);
        levelText.text = string.Format(nextLevelTextFormat, nextLevel);
    }

    private void Awake()
    {
        levelText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        GameManager.OnProfileChanged += UpdateNextLevelText;
    }

    private void OnDisable()
    {
        GameManager.OnProfileChanged -= UpdateNextLevelText;
    }

    private void Start()
    {
        UpdateNextLevelText(GameManager.SelectedProfile);
    }
}
