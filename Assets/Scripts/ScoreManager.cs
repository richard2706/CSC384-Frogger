using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const int homeFilledPoints = 50;
    private const int forwardStepPoints = 10;
    private const int unusedHalfSecondPoints = 10;
    private const int eatFlyPoints = 200;
    private const int allHomesFilledPoints = 1000;

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
        PlayerMovement.OnIncreaseMaxForwardStep += UpdateScoreOnForwardStep;
        FrogHome.OnLevelWon += IncreaseScoreOnAllHomesFilled;
    }

    private void OnDisable()
    {
        FrogHome.OnFrogReachedHome -= UpdateScoreOnFrogReachedHome;
        PlayerMovement.OnIncreaseMaxForwardStep -= UpdateScoreOnForwardStep;
        FrogHome.OnLevelWon -= IncreaseScoreOnAllHomesFilled;
    }

    private void UpdateScoreOnFrogReachedHome()
    {
        IncreaseScore(homeFilledPoints);
        OnScoreChange?.Invoke(this);
    }

    private void IncreaseScoreOnAllHomesFilled()
    {
        IncreaseScore(allHomesFilledPoints);
        OnScoreChange?.Invoke(this);
    }

    private void UpdateScoreOnForwardStep(int forwardSteps)
    {
        int points = forwardStepPoints * forwardSteps;
        IncreaseScore(points);
        OnScoreChange?.Invoke(this);
    }
}
