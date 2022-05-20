using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayNextLevel : MonoBehaviour
{
    private const string nextLevelTextFormat = "Level {0}";

    private Text levelText;

    private void Awake()
    {
        levelText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        UpdateNextLevelText(GameManager.SelectedProfile);
        GameManager.OnProfileChanged += UpdateNextLevelText;
    }

    private void OnDisable()
    {
        GameManager.OnProfileChanged -= UpdateNextLevelText;
    }

    private void UpdateNextLevelText(Profile selectedProfile)
    {
        int nextLevel = selectedProfile.GetNextLevel();
        levelText.text = string.Format(nextLevelTextFormat, nextLevel);
    }
}
