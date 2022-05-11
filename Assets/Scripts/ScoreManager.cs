using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreManager : MonoBehaviour
{
    private static int score = 0;
    private static Text scoreText;

    public static int IncreaseScore(int points)
    {
        score += points;
        UpdateScoreUI();
        return score;
    }

    private void Awake()
    {
        scoreText = GetComponent<Text>();
        UpdateScoreUI();
        scoreText.text = score.ToString();
    }

    private void OnEnable()
    {
        
    }

    private static void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }
}
