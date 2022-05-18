using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreUI : MonoBehaviour
{
    private Text scoreText;
    [SerializeField] private ScoreManager score;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        ScoreManager.OnScoreChange += UpdateScoreUI;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChange -= UpdateScoreUI;
    }

    public void UpdateScoreUI(ScoreManager score)
    {
        if (this.score == score) scoreText.text = score.Score.ToString();
    }
}
