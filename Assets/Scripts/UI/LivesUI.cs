using UnityEngine;

/// <summary>
/// Displays the specified player's number of lives.
/// </summary>
public class LivesUI : MonoBehaviour
{
    private const float lifeSpriteWidth = 37f;

    [SerializeField] private PlayerLives playerLives;

    private RectTransform livesTransform;

    private void Awake()
    {
        livesTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        GameStateManager.OnLevelStart += DisplayLives;
        PlayerLives.OnPlayerLoseLife += DisplayLives;
    }

    private void OnDisable()
    {
        GameStateManager.OnLevelStart -= DisplayLives;
        PlayerLives.OnPlayerLoseLife -= DisplayLives;
    }

    private void DisplayLives()
    {
        DisplayLives(playerLives);
    }

    private void DisplayLives(PlayerLives lives)
    {
        if (lives.Lives < 0 || lives != playerLives) return;

        float width = lives.Lives * lifeSpriteWidth;
        float height = livesTransform.sizeDelta.y;
        livesTransform.sizeDelta = new Vector2(width, height);
    }
}
