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
        GameStateManager.OnLevelStart += PauseTimer;
        FrogHome.OnFrogReachedHome += PauseTimer;
        PlayerLives.OnPlayerLoseLife += PauseTimer;
        PlayerManager.OnPlayerReady += RestartTimer;
        GameStateManager.OnLevelStart += RestartTimer;
    }

    private void OnDisable()
    {
        GameStateManager.OnLevelStart -= PauseTimer;
        FrogHome.OnFrogReachedHome -= PauseTimer;
        PlayerLives.OnPlayerLoseLife -= PauseTimer;
        PlayerManager.OnPlayerReady -= RestartTimer;
        GameStateManager.OnLevelStart -= RestartTimer;
    }

    private void PauseTimer(PlayerLives playerLives)
    {
        ExecutePauseTimer();
    }

    private void PauseTimer()
    {
        ExecutePauseTimer();
    }

    private void ExecutePauseTimer()
    {
        StopAllCoroutines();
    }

    private void RestartTimer()
    {
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
