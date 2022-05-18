using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        PlayerLives.OnLevelLost += HandleLoseLevel;
        FrogHome.OnLevelWon += HandleWinLevel;
    }

    private void OnDisable()
    {
        PlayerLives.OnLevelLost -= HandleLoseLevel;
        FrogHome.OnLevelWon -= HandleWinLevel;
    }

    private void HandleLoseLevel()
    {
        loseLevelPanel.SetActive(true);
        StartCoroutine(WaitForRestart());
    }

    private void HandleWinLevel()
    {
        winLevelPanel.SetActive(true);
        // load next level
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
        bool keyPressed = false;
        while (!keyPressed)
        {
            if (Input.anyKey)
            {
                keyPressed = true;
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

    private IEnumerator WaitForRestart()
    {
        bool keyPressed = false;
        while (!keyPressed)
        {
            if (Input.anyKey)
            {
                keyPressed = true;
                RestartLevel();
            }
            yield return null;
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
