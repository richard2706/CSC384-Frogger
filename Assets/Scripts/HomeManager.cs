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
            ScoreManager.IncreaseScore(50);
            FillHome();
        }
    }

    private void FillHome()
    {
        transform.GetChild(0).gameObject.SetActive(true);

        homeFilled = true;
        filledHomes++;
        if (filledHomes == totalHomes)
        {
            Debug.Log("All homes filled");
        }
    }
}
