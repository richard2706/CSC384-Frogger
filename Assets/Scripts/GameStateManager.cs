using System.Collections;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] GameObject startLevelPanel;
    [SerializeField] GameObject winLevelPanel;
    [SerializeField] GameObject loseLevelPanel;

    private PlayerManager[] players;
    private Spawner[] spawners;

    private void Awake()
    {
        players = FindObjectsOfType<PlayerManager>(true);
        spawners = FindObjectsOfType<Spawner>(true);
    }

    private void OnEnable()
    {
        PlayerLives.OnGameOver += HandleLoseGame;
    }

    private void OnDisable()
    {
        PlayerLives.OnGameOver -= HandleLoseGame;
    }

    private void HandleLoseGame()
    {
        loseLevelPanel.SetActive(true);
    }

    private void Start()
    {
        startLevelPanel.SetActive(true);
        winLevelPanel.SetActive(false);
        loseLevelPanel.SetActive(false);

        DisableAll(players);
        DisableAll(spawners);

        StartCoroutine(WaitForStart());
    }

    private IEnumerator WaitForStart()
    {
        bool playerIndicatedStart = false;
        while (!playerIndicatedStart)
        {
            if (Input.anyKey)
            {
                playerIndicatedStart = true;
                StartLevel();
            }
            yield return null;
        }
    }

    private void StartLevel()
    {
        startLevelPanel.SetActive(false);
        EnableAll(players);
        EnableAll(spawners);
    }

    private void EnableAll(MonoBehaviour[] objectBehaviours)
    {
        foreach (MonoBehaviour behaviour in objectBehaviours)
        {
            behaviour.gameObject.SetActive(true);
        }
    }

    private void DisableAll(MonoBehaviour[] objectBehaviours)
    {
        foreach (MonoBehaviour behaviour in objectBehaviours)
        {
            behaviour.gameObject.SetActive(false);
        }
    }
}
