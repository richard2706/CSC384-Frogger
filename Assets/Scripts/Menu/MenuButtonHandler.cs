using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject achievementsPanel;

    public void ContinueGame()
    {
        Debug.Log("Continue game");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowChangeProfilePanel()
    {
        Debug.Log("Change profile");
    }

    public void StartTwoPlayerGame()
    {
        Debug.Log("Two player game");
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
