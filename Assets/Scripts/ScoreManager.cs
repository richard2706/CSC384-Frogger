using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreManager : MonoBehaviour
{
    private const int FrogReachHomePoints = 50;

    private static int score = 0;
    private static Text scoreText;

    public static int IncreaseScore(int points)
    {
        score += points;
        UpdateScoreUI();
        return score;
    }

    private static void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }

    private void Awake()
    {
        scoreText = GetComponent<Text>();
        UpdateScoreUI();
        scoreText.text = score.ToString();
    }

    private void OnEnable()
    {
        FrogHome.OnFrogReachedHome += UpdateScoreOnFrogReachedHome;
    }

    private void UpdateScoreOnFrogReachedHome()
    {
        IncreaseScore(FrogReachHomePoints);
    }
}
