using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject achievementsPanel;
    [SerializeField] private GameObject profilePanel;

    public void ContinueGame()
    {
        Debug.Log("Continue game");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowProfilePanel()
    {
        profilePanel.SetActive(true);
    }
    public void HideProfilePanel()
    {
        profilePanel.SetActive(false);
    }

    public void StartTwoPlayerGame()
    {
        GameManager.Multiplayer = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowAchievementsPanel()
    {
        achievementsPanel.SetActive(true);
    }

    public void HideAchievementsPanel()
    {
        achievementsPanel.SetActive(false);
    }

    public void ShowCreditsPanel()
    {   
        creditsPanel.SetActive(true);
    }

    public void HideCreditsPanel()
    {
        creditsPanel.SetActive(false);
    }
}
