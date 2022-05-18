using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FrogHome)), RequireComponent(typeof(Collider2D))]
public class FrogHomeFlys : MonoBehaviour
{
    public static event Action OnFlyEaten;

    [SerializeField] private float minFlyInterval;
    [SerializeField] private float maxFlyInterval;
    [SerializeField] private float flyStayDuration;

    private FrogHome frogHome;
    private HomeInside homeInside;
    private bool containsFly;

    private void Awake()
    {
        containsFly = false;
        frogHome = GetComponent<FrogHome>();
        homeInside = GetComponentInChildren<HomeInside>();
    }

    private void OnEnable()
    {
        PlayerLives.OnLevelLost += DisableFlys;
        FrogHome.OnLevelWon += DisableFlys;
    }

    private void OnDisable()
    {
        PlayerLives.OnLevelLost -= DisableFlys;
        FrogHome.OnLevelWon -= DisableFlys;
    }

    private void Start()
    {
        StartCoroutine(ToggleFlyLoop());
    }

    private IEnumerator ToggleFlyLoop()
    {
        float timeToFirstFly = UnityEngine.Random.Range(0, maxFlyInterval);
        yield return new WaitForSeconds(timeToFirstFly);

        while (!frogHome.IsFilled)
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponentInParent<PlayerMovement>())
        {
            DisableFlys();
            if (containsFly) OnFlyEaten?.Invoke();
        }
    }

    private void DisableFlys()
    {
        StopAllCoroutines();
    }
}
