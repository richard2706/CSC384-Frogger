using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private int duration;

    private float timeRemaining;
    private PlayerLives[] allPlayersLives;

    private void Awake()
    {
        timeRemaining = duration;
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
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        timeRemaining = duration;

        while (timeRemaining > 0)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            timeRemaining -= 0.5f;
        }
    }
}
