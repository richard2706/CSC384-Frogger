using UnityEngine;
using UnityEngine.UI;

public class MenuButtonHandler : MonoBehaviour
{
    [SerializeField] GameObject creditsPanel;

    public void ContinueGame()
    {
        Debug.Log("Continue game");
    }

    public void RestartGame()
    {
        Debug.Log("Restart game");
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
        Debug.Log("Achievements");
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
