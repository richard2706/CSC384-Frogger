using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    private const float lifeSpriteWidth = 37f;

    private RectTransform livesTransform;
    private int displayedNumLives;
    private Vector2 initialAnchoredPosition;

    private void Awake()
    {
        livesTransform = GetComponent<RectTransform>();
        initialAnchoredPosition = livesTransform.anchoredPosition;
        //lifeSpriteWidth = livesTransform.rect.width;
    }

    private void OnEnable()
    {
        PlayerManager.OnPlayerLoseLife += RemoveLife;
    }

    private void Start()
    {
        // set number of lives from lives script
        displayedNumLives = 7;

        float width = displayedNumLives * lifeSpriteWidth;
        float height = livesTransform.sizeDelta.y;
        livesTransform.sizeDelta = new Vector2(width, height);

        livesTransform.anchoredPosition = new Vector2(initialAnchoredPosition.x - width / 2, initialAnchoredPosition.y);

        //Debug.Log(livesTransform.anchoredPosition);

        //livesTransform.anchoredPosition 
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerLoseLife -= RemoveLife;
    }

    private void AddLife()
    {
        displayedNumLives++;
        float width = livesTransform.sizeDelta.x + lifeSpriteWidth;
        float height = livesTransform.sizeDelta.y;
        livesTransform.sizeDelta = new Vector2(width, height);
    }

    private void RemoveLife()
    {
        if (displayedNumLives <= 0) return;
        displayedNumLives--;
        float width = livesTransform.sizeDelta.x - lifeSpriteWidth;
        float height = livesTransform.sizeDelta.y;
        livesTransform.sizeDelta = new Vector2(width, height);
    }

    //private void DisplayLives(int numLives)
    //{

    //}
}
