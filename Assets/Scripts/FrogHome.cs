using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FrogHome : MonoBehaviour
{
    public static event Action OnFrogReachedHome;

    private static List<FrogHome> allHomes = new List<FrogHome>();

    public static FrogHome[] AllHomes => allHomes.ToArray();
    private static bool AllHomesFilled => allHomes.TrueForAll(HomeIsTaken);
    private static bool HomeIsTaken(FrogHome home) => home.IsTaken;

    public bool IsTaken { get; private set; }
    public Vector2 Position => homeTransform.position;
    public Vector2 ColliderSize => homeCollider.bounds.size;

    private Transform homeTransform;
    private Collider2D homeCollider;

    private void Awake()
    {
        homeTransform = transform;
        homeCollider = GetComponent<Collider2D>();
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
            OnFrogReachedHome?.Invoke();
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
