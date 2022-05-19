using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static event Action<Timer> OnTimerUpdate;

    public float TimeRemaining { get; private set; }

    [SerializeField] private int duration;

    private PlayerManager[] allPlayers;

    private void Awake()
    {
        TimeRemaining = duration;
        allPlayers = FindObjectsOfType<PlayerManager>();
    }

    private void OnEnable()
    {
        GameStateManager.OnLevelStart += RestartTimer;

        if (GameManager.Multiplayer)
        {
            FrogHome.OnFrogReachedHome += RestartTimer;
        }
        else
        {
            PlayerManager.OnPlayerReady += RestartTimer;

            FrogHome.OnFrogReachedHome += StopTimer;
            PlayerLives.OnPlayerLoseLife += StopTimer;
        }

        //PlayerLives.OnLevelLost += ExecuteStopTimer;
    }

    private void OnDisable()
    {
        GameStateManager.OnLevelStart -= RestartTimer;

        if (GameManager.Multiplayer)
        {
            FrogHome.OnFrogReachedHome -= RestartTimer;
        }
        else
        {
            PlayerManager.OnPlayerReady -= RestartTimer;

            FrogHome.OnFrogReachedHome -= StopTimer;
            PlayerLives.OnPlayerLoseLife -= StopTimer;
        }

        //PlayerLives.OnLevelLost -= ExecuteStopTimer;
    }

    private void StopTimer(PlayerLives playerLives)
    {
        ExecuteStopTimer();
    }

    private void StopTimer()
    {
        ExecuteStopTimer();
    }

    private void ExecuteStopTimer()
    {
        StopAllCoroutines();
    }

    private void RestartTimer()
    {
        ExecuteStopTimer();
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        TimeRemaining = duration;
        OnTimerUpdate?.Invoke(this);

        while (TimeRemaining > 0)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            TimeRemaining -= 0.5f;
            OnTimerUpdate?.Invoke(this);
        }

        foreach (PlayerManager player in allPlayers)
        {
            player.PlayerLoseLife();
        }
    }
}
