using UnityEngine;

public class Lives : MonoBehaviour
{
    [SerializeField] private int maxLives;
    private int lives;

    private void Awake()
    {
        lives = maxLives;
    }

    private void OnEnable()
    {
        Dangerous.OnPlayerDangerousCollision += LoseLife;
    }

    private void OnDisable()
    {
        Dangerous.OnPlayerDangerousCollision -= LoseLife;
    }

    private void LoseLife()
    {
        lives--;
        if (lives < 0) lives = 0;
        if (lives == 0)
        {
            Debug.Log("ALl lives lost. Game over.");
        }
    }
}
