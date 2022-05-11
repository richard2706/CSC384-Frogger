using System;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    private static List<HomeManager> allHomes = new List<HomeManager>();

    public static HomeManager[] AllHomes => allHomes.ToArray();
    private static bool AllHomesFilled => allHomes.TrueForAll(HomeIsTaken);
    private static bool HomeIsTaken(HomeManager home) => home.IsTaken;

    public bool IsTaken { get; private set; }
    public Vector2 Position => homeTransform.position;

    private Transform homeTransform;

    private void Awake()
    {
        homeTransform = transform;
        IsTaken = false;
    }

    private void OnEnable()
    {
        allHomes.Add(this);
    }

    private void OnDisable()
    {
        allHomes.Remove(this);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!IsTaken && collider.GetComponentInParent<PlayerMovement>())
        {
            ScoreManager.IncreaseScore(50);
            FillHome();
        }
    }

    private void FillHome()
    {
        homeTransform.GetChild(0).gameObject.SetActive(true);

        IsTaken = true;
        if (AllHomesFilled)
        {
            Debug.Log("All homes filled");
        }
    }
}
