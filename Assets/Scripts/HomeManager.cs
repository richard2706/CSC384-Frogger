using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    private static int filledHomes = 0; // refactor to remove this
    private static List<Vector2> allHomePositions = new List<Vector2>();

    private bool homeFilled = false;
    private Transform homeTransform;

    public static Vector2[] AllHomePositions => allHomePositions.ToArray();

    private void Awake()
    {
        homeTransform = transform;
    }

    private void OnEnable()
    {
        allHomePositions.Add(homeTransform.position);
    }

    private void OnDisable()
    {
        allHomePositions.Remove(homeTransform.position);
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
        if (filledHomes == allHomePositions.Count)
        {
            Debug.Log("All homes filled");
        }
    }
}
