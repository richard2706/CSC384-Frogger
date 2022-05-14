using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    private const float lifeSpriteWidth = 37f;

    [SerializeField] private PlayerLives playerLives;

    private RectTransform livesTransform;
    private Vector2 initialAnchoredPosition;

    private void Awake()
    {
        livesTransform = GetComponent<RectTransform>();
        initialAnchoredPosition = livesTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        PlayerManager.OnPlayerLoseLife += DisplayLives;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerLoseLife -= DisplayLives;
    }

    private void Start()
    {
        DisplayLives(playerLives);
    }

    private void DisplayLives(PlayerLives lives)
    {
        if (lives.Lives < 0 || lives != playerLives) return;
        float width = lives.Lives * lifeSpriteWidth;
        float height = livesTransform.sizeDelta.y;
        livesTransform.sizeDelta = new Vector2(width, height);
        livesTransform.anchoredPosition = new Vector2(initialAnchoredPosition.x - width / 2, initialAnchoredPosition.y);
    }
}
