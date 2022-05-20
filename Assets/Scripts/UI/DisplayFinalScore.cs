using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class DisplayFinalScore : MonoBehaviour
{
    private const string scoreTextFormat = "Your score: {0}";
    [SerializeField] private ScoreManager score;

    private Text finalScoreText;

    private void Awake()
    {
        finalScoreText = GetComponent<Text>();
    }

    private void Start()
    {
        StartCoroutine(ShowFinalScore());
    }

    private IEnumerator ShowFinalScore()
    {
        yield return new WaitForFixedUpdate();
        finalScoreText.text = string.Format(scoreTextFormat, score.Score.ToString());
    }
}
