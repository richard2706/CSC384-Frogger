using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    public static event Action OnLevelLost;
    public static event Action<PlayerLives> OnPlayerLoseLife;

    private static List<PlayerLives> allPlayersLives = new List<PlayerLives>();

    [SerializeField] private int maxLives;
    public int Lives { get; private set; }

    private bool extraLife;
    //private PlayerPowerUpInteraction powerUpInteraction;

    public void ApplyExtraLife()
    {
        extraLife = true;
    }

    private void Awake()
    {
        Lives = maxLives;
        //powerUpInteraction = GetComponent<PlayerPowerUpInteraction>();
    }

    private void OnEnable()
    {
        allPlayersLives.Add(this);
    }

    private void OnDisable()
    {
        allPlayersLives.Remove(this);
    }

    /// <summary>
    /// Causes the player to lose a life if they do not have an extra life.
    /// </summary>
    /// <returns>True if the player lost a life, false if they had an extra life to protect them.</returns>
    public bool LoseLife()
    {
        Debug.Log(extraLife);
        if (extraLife)
        {
            Debug.Log("use extra life");
            extraLife = false;
            return false;
            //if (powerUpInteraction) powerUpInteraction.UsePowerUp();
        }
        else
        {
            Lives--;
            if (Lives < 0) Lives = 0;
            else OnPlayerLoseLife?.Invoke(this);

            if (Lives == 0)
            {
                CheckGameOver();
            }
        }
        return true;
    }

    private void CheckGameOver()
    {
        bool playerWithLivesExists = false;
        foreach (PlayerLives player in allPlayersLives)
        {
            if (player.Lives != 0) playerWithLivesExists = true;
        }
        if (!playerWithLivesExists) OnLevelLost?.Invoke();
    }
}
