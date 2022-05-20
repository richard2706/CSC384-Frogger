using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static event Action OnLevelStart;

    [SerializeField] private GameObject startLevelPanel;
    [SerializeField] private GameObject winLevelPanel;
    [SerializeField] private GameObject loseLevelPanel;

    private PlayerManager[] players;
    private Spawner[] spawners;
    private FrogHomeFlys[] frogHomeFlys;
    private LivesUI[] livesIndicators;

    private void Awake()
    {
        players = FindObjectsOfType<PlayerManager>(true);
        spawners = FindObjectsOfType<Spawner>(true);
        frogHomeFlys = FindObjectsOfType<FrogHomeFlys>(true);
        livesIndicators = FindObjectsOfType<LivesUI>(true);
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
        Timer timer = FindObjectOfType<Timer>();
        timer.StopAllCoroutines();
        timer.gameObject.SetActive(false);
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
        Debug.Log("Multiplayer: " + GameManager.Multiplayer);
        if (GameManager.Multiplayer) EnableAll(livesIndicators);

        DisableAll(players);
        DisableAll(spawners);
        DisableAllBehaviours(frogHomeFlys);

        StartCoroutine(WaitForStart());
    }

    private IEnumerator WaitForStart()
    {
        bool keyPressed = false;
        while (!keyPressed)
        {
            if (Input.anyKeyDown)
            {
                keyPressed = true;
                StartLevel();
                OnLevelStart?.Invoke();
            }
            yield return null;
        }
    }

    private void StartLevel()
    {
        startLevelPanel.SetActive(false);

        if (GameManager.Multiplayer)
        {
            Debug.Log("Start Multiplayer game");
            EnableAll(players);
        }
        else
        {
            Debug.Log("Start single player game" + players.Length);
            foreach (PlayerManager player in players)
            {
                if (player.IsPlayerOne())
                {
                    Debug.Log("Player 1 active");
                    player.gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log("Other player inactive");
                    player.gameObject.SetActive(false);
                }
            }
        }

        EnableAll(spawners);
        EnableAllBehaviours(frogHomeFlys);
    }

    private IEnumerator WaitForRestart()
    {
        bool keyPressed = false;
        while (!keyPressed)
        {
            if (Input.anyKeyDown)
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

    private void EnableAllBehaviours(MonoBehaviour[] behaviours)
    {
        foreach (MonoBehaviour behaviour in behaviours)
        {
            behaviour.enabled = true;
        }
    }

    private void DisableAllBehaviours(MonoBehaviour[] behaviours)
    {
        foreach (MonoBehaviour behaviour in behaviours)
        {
            behaviour.enabled = false;
        }
    }
}
