using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayFinalScore : MonoBehaviour
{
    private const string scoreTextFormat = "Your score: {0}";
    [SerializeField] private ScoreManager score;

    private void Start()
    {
        GetComponent<Text>().text = string.Format(scoreTextFormat, score.Score.ToString());
    }
}
