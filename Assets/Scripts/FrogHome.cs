using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FrogHome : MonoBehaviour
{
    public static event Action OnFrogReachedHome;
    public static event Action OnLevelWon;

    private static List<FrogHome> allHomes = new List<FrogHome>();

    public static FrogHome[] AllHomes => allHomes.ToArray();
    private static bool AllHomesFilled => allHomes.TrueForAll(HomeIsTaken);
    private static bool HomeIsTaken(FrogHome home) => home.IsFilled;

    public bool IsFilled { get; private set; }
    public Vector2 Position => homeTransform.position;
    public Vector2 ColliderSize => homeCollider.bounds.size;

    private Transform homeTransform;
    private Collider2D homeCollider;
    private HomeInside homeInside;

    private void Awake()
    {
        IsFilled = false;
        homeTransform = transform;
        homeCollider = GetComponent<Collider2D>();
        homeInside = GetComponentInChildren<HomeInside>();
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
        if (!IsFilled && collider.GetComponentInParent<PlayerMovement>())
        {
            FillHome();
        }
    }

    private void FillHome()
    {
        OnFrogReachedHome?.Invoke();
        homeInside.ShowFrog();

        IsFilled = true;
        if (AllHomesFilled)
        {
            OnLevelWon?.Invoke();
        }
    }
}
