using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    public static event Action OnGameOver;
    public static event Action<PlayerLives> OnPlayerLoseLife;

    private static List<PlayerLives> allPlayersLives = new List<PlayerLives>();

    [SerializeField] private int maxLives;
    public int Lives { get; private set; }

    private void Awake()
    {
        Lives = maxLives;
    }

    private void OnEnable()
    {
        allPlayersLives.Add(this);
    }

    private void OnDisable()
    {
        allPlayersLives.Remove(this);
    }

    public void LoseLife()
    {
        Lives--;
        if (Lives < 0) Lives = 0;
        else OnPlayerLoseLife?.Invoke(this);

        if (Lives == 0)
        {
            Debug.Log("ALl lives lost. Game over.");
            CheckGameOver();
        }
    }

    private void CheckGameOver()
    {
        bool playerWithLivesExists = false;
        foreach (PlayerLives player in allPlayersLives)
        {
            if (player.Lives != 0) playerWithLivesExists = true;
        }
        if (!playerWithLivesExists) OnGameOver?.Invoke();
    }
}
