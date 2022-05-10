using UnityEngine;

public class HomeManager : MonoBehaviour
{
    private static int totalHomes = 0;
    private static int filledHomes = 0;

    private bool homeFilled = false;

    private void OnEnable()
    {
        totalHomes++;
    }

    private void OnDisable()
    {
        totalHomes--;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!homeFilled && collider.GetComponentInParent<PlayerMovement>())
        {
            Debug.Log("Player reached a home");
            ScoreManager.IncreaseScore(50);
            homeFilled = true;
            filledHomes++;
            if (filledHomes == totalHomes)
            {
                Debug.Log("All homes filled");
            }
        }
    }
}
