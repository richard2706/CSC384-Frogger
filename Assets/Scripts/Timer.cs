using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static event Action<Timer> OnTimerUpdate;

    public float TimeRemaining { get; private set; }

    [SerializeField] private int duration;

    private PlayerLives[] allPlayersLives;

    private void Awake()
    {
        TimeRemaining = duration;
        allPlayersLives = FindObjectsOfType<PlayerLives>();
    }

    private void OnEnable()
    {
        GameStateManager.OnLevelStart += RestartTimer;
        FrogHome.OnFrogReachedHome += RestartTimer;
        PlayerLives.OnPlayerLoseLife += RestartTimer;
    }

    private void OnDisable()
    {
        GameStateManager.OnLevelStart -= RestartTimer;
        FrogHome.OnFrogReachedHome -= RestartTimer;
        PlayerLives.OnPlayerLoseLife += RestartTimer;
    }

    private void RestartTimer(PlayerLives playerLives)
    {
        ExecuteRestartTimer();
    }

    private void RestartTimer()
    {
        ExecuteRestartTimer();
    }

    private void ExecuteRestartTimer()
    {
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer() // fix issue with this being run once for each event
    {
        TimeRemaining = duration;

        while (TimeRemaining > 0)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            TimeRemaining -= 0.5f;
            OnTimerUpdate?.Invoke(this);
        }
    }
}
