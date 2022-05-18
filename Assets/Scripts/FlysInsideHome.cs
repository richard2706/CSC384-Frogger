using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FrogHome))]
public class FlysInsideHome : MonoBehaviour
{
    public bool ContainsFly { get; private set; }

    [SerializeField] private float minFlyInterval;
    [SerializeField] private float maxFlyInterval;
    [SerializeField] private float flyStayDuration;

    private HomeInside homeInside;

    private void Awake()
    {
        ContainsFly = false;
        homeInside = GetComponentInChildren<HomeInside>();
    }

    private void Start()
    {
        StartCoroutine(ToggleFlyLoop());
    }

    private void OnEnable()
    {
        FrogHome.OnFrogReachedHome += DisableFlys;
        PlayerLives.OnLevelLost += DisableFlys;
        FrogHome.OnLevelWon += DisableFlys;
    }

    private void OnDisable()
    {
        FrogHome.OnFrogReachedHome -= DisableFlys;
        PlayerLives.OnLevelLost -= DisableFlys;
        FrogHome.OnLevelWon -= DisableFlys;
    }

    private IEnumerator ToggleFlyLoop()
    {
        float timeToFirstFly = UnityEngine.Random.Range(0, maxFlyInterval);
        yield return new WaitForSeconds(timeToFirstFly);

        while (true)
        {
            ContainsFly = true;
            homeInside.ShowFly();
            yield return new WaitForSeconds(flyStayDuration);

            homeInside.HideFly();
            ContainsFly = false;
            float betweenFlyInterval = UnityEngine.Random.Range(minFlyInterval, maxFlyInterval);
            yield return new WaitForSeconds(betweenFlyInterval);
        }
    }

    private void DisableFlys()
    {
        StopAllCoroutines();
        homeInside.HideFly();
        enabled = false;
    }
}
