using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const int homeFilledPoints = 50;
    private const int forwardStepPoints = 10;
    private const int unusedHalfSecondPoints = 10;
    private const int eatFlyPoints = 200;
    private const int allHomesFilledPoints = 1000;

    private const float unusedTimeUnit = 0.5f; // Unused time points applied for each of these lengths

    public static event Action<ScoreManager> OnScoreChange;

    public int Score { get; private set; }

    [SerializeField] private Timer timer;

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
        FrogHomeFlys.OnFlyEaten += UpdateScoreOnFlyEaten;
        FrogHome.OnLevelWon += IncreaseScoreOnAllHomesFilled;
    }

    private void OnDisable()
    {
        FrogHome.OnFrogReachedHome -= UpdateScoreOnFrogReachedHome;
        PlayerMovement.OnIncreaseMaxForwardStep -= UpdateScoreOnForwardStep;
        FrogHomeFlys.OnFlyEaten -= UpdateScoreOnFlyEaten;
        FrogHome.OnLevelWon -= IncreaseScoreOnAllHomesFilled;
    }

    private void UpdateScoreOnFrogReachedHome()
    {
        int unusedTimeUnits = (int)(Math.Floor(timer.TimeRemaining / unusedTimeUnit) * unusedTimeUnit / unusedTimeUnit);
        int points = homeFilledPoints + unusedHalfSecondPoints * unusedTimeUnits;
        IncreaseScore(points);
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

    private void UpdateScoreOnFlyEaten()
    {
        IncreaseScore(eatFlyPoints);
        OnScoreChange?.Invoke(this);
    }
}
