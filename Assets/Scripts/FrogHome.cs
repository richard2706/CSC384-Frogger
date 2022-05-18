using System;
using System.Collections;
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

    [SerializeField] private float minFlyInterval;
    [SerializeField] private float maxFlyInterval;
    [SerializeField] private float flyStayDuration;

    private Transform homeTransform;
    private Collider2D homeCollider;
    private HomeInside homeInside;
    private bool containsFly;

    private void Awake()
    {
        IsFilled = false;
        homeTransform = transform;
        homeCollider = GetComponent<Collider2D>();
        homeInside = GetComponentInChildren<HomeInside>();
        containsFly = false;
    }

    private void OnEnable()
    {
        allHomes.Add(this);
    }

    private void OnDisable()
    {
        allHomes.Remove(this);
    }

    private void Start()
    {
        StartCoroutine(ToggleFlyLoop());
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

    private IEnumerator ToggleFlyLoop()
    {
        float timeToFirstFly = UnityEngine.Random.Range(0, maxFlyInterval);
        yield return new WaitForSeconds(timeToFirstFly);

        while (true)
        {
            containsFly = true;
            homeInside.ShowFly();
            yield return new WaitForSeconds(flyStayDuration);

            homeInside.HideFly();
            containsFly = false;
            float betweenFlyInterval = UnityEngine.Random.Range(minFlyInterval, maxFlyInterval);
            yield return new WaitForSeconds(betweenFlyInterval);
        }
    }
}
