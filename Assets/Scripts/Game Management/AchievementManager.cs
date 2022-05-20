using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementManager : MonoBehaviour
{
    private Profile currentProfile;

    private void OnDisable()
    {
        /*if (!currentProfile.HasAchievement(0))*/ FrogHome.OnFrogReachedHome -= UnlockCrossTheRiver;
        /*if (!currentProfile.HasAchievement(1))*/ FrogHome.OnLevelWon -= UnlockCompleteFirstLevel;
        /*if (!currentProfile.HasAchievement(4))*/ FrogHome.OnLevelWon -= UnlockCompleteAllLevels;
        /*if (!currentProfile.HasAchievement(2))*/ FrogHomeFlys.OnFlyEaten -= UnlockEatAFly;
        /*if (!currentProfile.HasAchievement(3))*/ ScoreManager.OnScoreChange -= UnlockScore3000;
    }

    private void Start()
    {
        currentProfile = GameManager.SelectedProfile;
        /*if (!currentProfile.HasAchievement(0))*/ FrogHome.OnFrogReachedHome += UnlockCrossTheRiver;
        /*if (!currentProfile.HasAchievement(1))*/ FrogHome.OnLevelWon += UnlockCompleteFirstLevel;
        /*if (!currentProfile.HasAchievement(4))*/ FrogHome.OnLevelWon += UnlockCompleteAllLevels;
        /*if (!currentProfile.HasAchievement(2))*/ FrogHomeFlys.OnFlyEaten += UnlockEatAFly;
        /*if (!currentProfile.HasAchievement(3))*/ ScoreManager.OnScoreChange += UnlockScore3000;
    }

    private void UnlockCrossTheRiver()
    {
        currentProfile.AddAchievement(0);
        FrogHome.OnFrogReachedHome -= UnlockCrossTheRiver;
    }

    private void UnlockCompleteFirstLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) currentProfile.AddAchievement(1);
    }

    private void UnlockEatAFly()
    {
        currentProfile.AddAchievement(2);
        FrogHomeFlys.OnFlyEaten -= UnlockEatAFly;
    }

    private void UnlockScore3000(ScoreManager score)
    {
        if (score.Score >= 3000) currentProfile.AddAchievement(3);
    }

    private void UnlockCompleteAllLevels()
    {
        if (SceneManager.GetActiveScene().buildIndex == GameManager.NumLevels) currentProfile.AddAchievement(4);
        FrogHome.OnLevelWon -= UnlockCompleteAllLevels;
    }
}
