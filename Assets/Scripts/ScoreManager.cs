using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const int homeFilledPoints = 50;
    private const int forwardStepPoints = 10;
    private const int unusedHalfSecondPoints = 10;
    private const int eatFlyPoints = 200;
    private const int allFrogsHomePoints = 1000;

    public static event Action<ScoreManager> OnScoreChange;

    public int Score { get; private set; }

    public int IncreaseScore(int points)
    {
        Score += points;
        return Score;
    }

    private void Awake()
    {
        Score = 0;
    }

    private void Start()
    {
        OnScoreChange?.Invoke(this);
    }

    private void OnEnable()
    {
        FrogHome.OnFrogReachedHome += UpdateScoreOnFrogReachedHome;
    }

    private void OnDisable()
    {
        FrogHome.OnFrogReachedHome -= UpdateScoreOnFrogReachedHome;
    }

    private void UpdateScoreOnFrogReachedHome()
    {
        IncreaseScore(homeFilledPoints);
        OnScoreChange?.Invoke(this);
    }
}
