using System.Collections;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    [SerializeField] private int maxLives;
    private int lives;

    private void Awake()
    {
        lives = maxLives;
    }

    public void LoseLife()
    {
        lives--;
        if (lives < 0) lives = 0;
        if (lives == 0)
        {
            Debug.Log("ALl lives lost. Game over.");
        }
    }
}
