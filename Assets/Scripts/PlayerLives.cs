using System.Collections;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    [SerializeField] private int maxLives;
    public int Lives { get; private set; }

    private void Awake()
    {
        Lives = maxLives;
    }

    public void LoseLife()
    {
        Lives--;
        if (Lives < 0) Lives = 0;
        if (Lives == 0)
        {
            Debug.Log("ALl lives lost. Game over.");
        }
    }
}
